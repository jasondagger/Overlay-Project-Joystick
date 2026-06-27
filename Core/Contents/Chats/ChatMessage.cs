
using Godot;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Https;
using Overlay.Core.Services.ColorInterpolators;
using Overlay.Core.Tools;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Overlay.Core.Contents.Chats;

 internal sealed partial class ChatMessage() :
     Node2D()
 {
	 public override void _Ready()
     {
         this.RetrieveResources();
     }
	 
     public override void _Process(
         double delta
	 )
     {
         switch (this.m_generateState)
         {
             case GenerateStateType.Complete:
                 this.Generated?.Invoke(
                     obj: this
				 );
                 this.m_generateState = GenerateStateType.Active;
                 break;
             
             case GenerateStateType.Active:
	             this.HandleTextAnimation(
		             delta: (float)delta
		         );
	             break;
             
             case GenerateStateType.Inactive:
             default:
                 break;
         }
     }
	 
     internal Action              Destroyed = null;
	 internal Action<ChatMessage> Generated = null;
	 
     internal void Generate(
         string                            username,
         bool                              hasCustomBadgeColor,
         ServiceColorInterpolatorColorMode customBadgeColor,
         bool                              hasCustomNameColor,
         ServiceColorInterpolatorColorMode customNameColor,
         string                            message,
         ChatMessageEmote[]                chatMessageEmotes,
         bool                              isModerator,
         bool                              isStreamer,
         bool                              isSubscriber,
         bool                              isBot
     )
     {
	     var badgeBot        = isBot                        ? $"[img=28x28]user://dynamic_Bot_{       (hasCustomBadgeColor ? $"{customBadgeColor}" : "Default")}.res[/img]" : string.Empty;
	     var badgeModerator  = isModerator                  ? $"[img=28x28]user://dynamic_Moderator_{ (hasCustomBadgeColor ? $"{customBadgeColor}" : "Default")}.res[/img]" : string.Empty;
	     var badgeSubscriber = isSubscriber && !isModerator ? $"[img=28x28]user://dynamic_Subscriber_{(hasCustomBadgeColor ? $"{customBadgeColor}" : "Default")}.res[/img]" : string.Empty;
	     var badgeStreamer   = isStreamer                   ? $"[img=28x28]user://dynamic_Streamer_{  (hasCustomBadgeColor ? $"{customBadgeColor}" : "Default")}.res[/img]" : string.Empty;
	     var badgeText       = badgeBot + badgeModerator + badgeSubscriber + badgeStreamer;
	     
	     this.m_hasCustomNameColor  = hasCustomNameColor;
	     this.m_customNameColor     = customNameColor;
	     this.m_username            = username;

	     lock (this.m_textLock)
	     { 
			this.m_hasCustomBadgeColor = hasCustomBadgeColor;
			this.m_customBadgeColor    = customBadgeColor;
			this.m_badgeText           = badgeText;
			this.m_text                = $"{badgeText}  " +
		                                 $"{ChatMessage.c_labelFontSize}" +
		                                 $"{ChatMessage.c_labelNameFont}" +
		                                 $"[color=#{ChatMessage.c_labelInterpolatorColor}]" +
		                                 $"{username}" +
		                                 $"  " +
		                                 $"{ChatMessage.c_labelMessageFont}" +
		                                 $"{ChatMessage.c_labelMessageColor}" +
		                                 $"{message}";
	     }
         
         this.InsertImages(
	         message:           message,
	         chatMessageEmotes: chatMessageEmotes
	    );
     }

 	internal int GetLabelHeightInPixels()
	{
		return this.m_richTextLabel.GetContentHeight() + 5;
	}

	internal bool HasUsername(
		string username
	)
	{
		return this.m_username == username;
	}

	internal void Reset()
    {
        this.m_chatMessageEmotesToLoad      = 0u;
        this.m_fadeElapsed                  = 0f;
        this.m_generateState                = GenerateStateType.Inactive;
        this.m_hasAnimatedChatMessageEmotes = false;
        this.m_richTextLabel.Text           = string.Empty;
        this.m_visibleState                 = VisibleStateType.Visible;

        lock (this.m_textLock)
        {
	        this.m_badgeText = string.Empty;
	        this.m_text      = string.Empty;
        }
        
        this.m_animatedChatMessageEmoteCurrentFrameCounts.Clear();
        this.m_animatedChatMessageEmoteCurrentFrameRates.Clear();
        this.m_animatedChatMessageEmoteMaxFrameCounts.Clear();
        this.m_animatedChatMessageEmoteMaxFrameRates.Clear();
        this.m_animatedChatMessageEmotes.Clear();
    }

	internal void ShowLabel()
	{
		this.m_richTextLabel.Visible = true;
	}

	internal void UpdateBadgeColor(
		bool                              hasCustomColor,
		ServiceColorInterpolatorColorMode customColor
	)
	{
		lock (this.m_textLock)
		{
			if (this.m_badgeText != string.Empty)
			{
				var currentColor = this.m_hasCustomBadgeColor ? $"{this.m_customBadgeColor}" : "Default";
				var newColor     = hasCustomColor ? $"{customColor}" : "Default";
				
				var badgeText    = this.m_badgeText.Replace(
					oldValue: currentColor,
					newValue: newColor
				);

				this.m_text      = this.m_text.Replace(
					oldValue: this.m_badgeText,
					newValue: badgeText
				);
				this.m_badgeText = badgeText;
			}
			
			this.m_hasCustomBadgeColor = hasCustomColor;
			this.m_customBadgeColor    = customColor;
		}
	}

	internal void UpdateNameColor(
		bool                              hasCustomColor,
		ServiceColorInterpolatorColorMode customColor
	)
	{
		this.m_hasCustomNameColor = hasCustomColor;
		this.m_customNameColor    = customColor;
	}

    private enum GenerateStateType :
        uint
	{
		Complete = 0u,
		Active,
        Inactive,
    }
    
    private enum VisibleStateType :
        uint
    {
        Fade = 0u,
        Visible,
    }

    private enum StaticImageFormatType :
	    uint
    {
	    Jpg,
	    Png,
    }
    
    private const int                                           c_chatMessageEmoteWidth                      = 16;
	private const int                                           c_chatMessageEmoteHeight                     = 16;
    private const int                                           c_gifFrameRateIndex0                         = 804;
    private const int                                           c_gifFrameRateIndex1                         = 805;
    private const float                                         c_defaultEmoteFramesPerSecondInMilliseconds  = 0.04167f;
    private const string                                        c_labelInterpolatorColor                     = $"00000000";
    private const string                                        c_labelFontSize                              = $"[font_size=28]";
    private const string                                        c_labelNameFont                              = $"[font=res://Resources/Fonts/Roboto-Black.tres]";
    private const string                                        c_labelMessageFont                           = $"[font=res://Resources/Fonts/Roboto-Bold.tres]";
    private const string                                        c_chatMessageEmoteDirectoryAnimated          = $"user://Emotes/Animated";
    private const string                                        c_chatMessageEmoteDirectoryStatic            = $"user://Emotes/Static";
    private const string                                        c_labelMessageColor                          = $"[color=#F2F2F2FF]";
    private const uint                                          c_labelWidth                                 = 1100u;
	
    private static readonly Dictionary<string, string>          s_chatMessageEmoteBasics                     = new()
    {
	    { ":D", ":grin:"  },
	    { ":)", ":smile:" },
	    { ":*", ":kiss:"  },
	    { ";)", ":wink:"  },
	    { "<3", ":heart:" },
    };
    private static readonly HashSet<string>                     s_chatMessageEmotesWithFileSyntax            =
    [
	    ":D",
	    ":)",
	    ":*",
	    ";)",
	    "<3",
    ];
    private static readonly Dictionary<VisibleStateType, float> s_fadeDelays                                 = new()
    {
	    { VisibleStateType.Visible, 32f },
	    { VisibleStateType.Fade,    2f },
    };
    
    private readonly Dictionary<string, int>                    m_animatedChatMessageEmoteCurrentFrameCounts = new();
    private readonly Dictionary<string, float>                  m_animatedChatMessageEmoteCurrentFrameRates  = new();
    private readonly Dictionary<string, int>                    m_animatedChatMessageEmoteMaxFrameCounts     = new();
    private readonly Dictionary<string, float>                  m_animatedChatMessageEmoteMaxFrameRates      = new();
    private readonly HashSet<string>                            m_animatedChatMessageEmotes                  = [];
    private readonly object                                     m_textLock                                   = new();

	private uint                                                m_chatMessageEmotesToLoad                    = 0u;
	private string                                              m_badgeText                                  = string.Empty;
	private ServiceColorInterpolatorColorMode                   m_customBadgeColor                           = ServiceColorInterpolatorColorMode.White;
	private ServiceColorInterpolatorColorMode                   m_customNameColor                            = ServiceColorInterpolatorColorMode.White;
    private float                                               m_fadeElapsed                                = 0f;
	private GenerateStateType                                   m_generateState                              = GenerateStateType.Inactive;
	private bool                                                m_hasAnimatedChatMessageEmotes               = false;
	private bool                                                m_hasCustomNameColor                         = false;
	private bool                                                m_hasCustomBadgeColor                        = false;
    private RichTextLabel                                       m_richTextLabel                              = new();
    private ServiceGodotHttp                                    m_serviceGodotHttp                           = null;
    private ServiceColorInterpolatorNormal                      m_serviceColorInterpolatorNormal             = null;
	private string                                              m_text                                       = string.Empty;
	private string                                              m_username                                   = string.Empty;
	private VisibleStateType                                    m_visibleState                               = VisibleStateType.Visible;

	private void GenerateStaticImageFromStaticChatMessageEmote(
		byte[]                body,
		string                chatMessageEmoteCodeLookUp,
		string                chatMessageEmoteCode,
		string                chatMessageEmoteStaticPathGlobal,
		StaticImageFormatType staticImageFormat
	)
	{
		var userDirectoryPath = $"{ChatMessage.c_chatMessageEmoteDirectoryStatic}/{chatMessageEmoteCodeLookUp}";
		if (
			Directory.Exists(
				path: userDirectoryPath
			) is false
		)
		{
			var fullPath = ProjectSettings.GlobalizePath(
				path: userDirectoryPath
			);
			Directory.CreateDirectory(
				path: fullPath
			);
		}
		
		var image = Godot.Image.CreateEmpty(
			width:      ChatMessage.c_chatMessageEmoteWidth,
			height:     ChatMessage.c_chatMessageEmoteHeight,
			useMipmaps: false,
			format:     Godot.Image.Format.Rgba8
		);

		switch (staticImageFormat)
		{
			case StaticImageFormatType.Jpg:
				image.LoadJpgFromBuffer(
					buffer: body
				);
				break;
			
			case StaticImageFormatType.Png:
				image.LoadPngFromBuffer(
					buffer: body
				);
				break;
			
			default:
				ConsoleLogger.LogMessageError(
					messageError:
						$"{nameof(ChatMessage)}." +
						$"{nameof(ChatMessage.GenerateStaticImageFromStaticChatMessageEmote)}() - " +
						$"EXCEPTION: Missing static image format type '{staticImageFormat}'."
				);
				return;
		}
		
		var imageTexture = ImageTexture.CreateFromImage(
			image: image
		);
		var emotePath = $"{chatMessageEmoteStaticPathGlobal}\\static_0.res";
		ResourceSaver.Save(
			resource: imageTexture,
			path:     emotePath
		);
		this.ReplaceChatMessageCodeWithChatMessageImagePath(
			chatMessageEmoteCode: chatMessageEmoteCode,
			chatMessageEmotePath: emotePath
		);
		
		this.m_chatMessageEmotesToLoad--;
		if (this.m_chatMessageEmotesToLoad is 0u)
		{
			this.GenerateRichTextLabel();
		}
	}
	
	private async Task GenerateAnimatedImagesFromAnimatedChatMessageEmote(
		byte[] body,
		string chatMessageEmoteCodeLookUp,
		string chatMessageEmoteCode,
		string chatMessageEmoteAnimatedPathGlobal
	)
	{
		var userDirectoryPath = $"{ChatMessage.c_chatMessageEmoteDirectoryAnimated}/{chatMessageEmoteCodeLookUp}";
		if (
			Directory.Exists(
				path: userDirectoryPath
			) is false
		)
		{
			var fullPath = ProjectSettings.GlobalizePath(
				path: userDirectoryPath
			);
			Directory.CreateDirectory(
				path: fullPath
			);
		}

		var imageSixLabors = SixLabors.ImageSharp.Image.Load(
			buffer: body
		);
		var frames = imageSixLabors.Frames;
		for (var i = 0; i < frames.Count; i++)
		{
			var pngFilePath = $"{chatMessageEmoteAnimatedPathGlobal}/animated_{i}.png";
			var imageFrame  = frames.CloneFrame(
				index: i
			);
			await imageFrame.SaveAsPngAsync(
				path: pngFilePath
			);

			var bytes = await File.ReadAllBytesAsync(
				path: pngFilePath
			);
			
			File.Delete(
				path: pngFilePath	
			);

			var image = Godot.Image.CreateEmpty(
				width:      ChatMessage.c_chatMessageEmoteWidth,
				height:     ChatMessage.c_chatMessageEmoteHeight,
				useMipmaps: false,
				format:     Godot.Image.Format.Rgba8
			);
			image.LoadPngFromBuffer(
				buffer: bytes
			);
			var imageTexture = ImageTexture.CreateFromImage(
				image: image
			);
			var emoteFile = $"{chatMessageEmoteAnimatedPathGlobal}/animated_{i}.res";
			ResourceSaver.Save(
				resource: imageTexture,
				path:     emoteFile
			);
		}

		var frameDelay = BitConverter.ToInt16(
			value:
			[
				body[ChatMessage.c_gifFrameRateIndex0],
				body[ChatMessage.c_gifFrameRateIndex1]
			],
			startIndex: 0
		);
		var normalizedFrameDelay = 
			frameDelay > 0.1f || frameDelay < .001f ?
				ChatMessage.c_defaultEmoteFramesPerSecondInMilliseconds :
				frameDelay / 100f;
		var frameDelayText = $"{normalizedFrameDelay}";
		var frameDelayFile = $"{chatMessageEmoteAnimatedPathGlobal}/frame_rate.txt";
		await File.WriteAllTextAsync(
			path:     frameDelayFile,
			contents: frameDelayText
		);
		
		var emotePath = $"{chatMessageEmoteAnimatedPathGlobal}/animated_0.res";
		this.ReplaceChatMessageCodeWithChatMessageImagePath(
			chatMessageEmoteCode: chatMessageEmoteCode,
			chatMessageEmotePath: emotePath
		);
		this.m_animatedChatMessageEmotes.Add(
			item: chatMessageEmoteCodeLookUp
		);
		this.m_animatedChatMessageEmoteCurrentFrameCounts.Add(
			key:   chatMessageEmoteCodeLookUp,
			value: 0
		);
		this.m_animatedChatMessageEmoteMaxFrameCounts.Add(
			key:   chatMessageEmoteCodeLookUp,
			value: frames.Count - 1
		);
		this.m_animatedChatMessageEmoteCurrentFrameRates.Add(
			key:   chatMessageEmoteCodeLookUp,
			value: 0f
		);
		this.m_animatedChatMessageEmoteMaxFrameRates.Add(
			key:   chatMessageEmoteCodeLookUp,
			value: frameDelayText.ToFloat()
		);
		
		this.m_hasAnimatedChatMessageEmotes = true;
		this.m_chatMessageEmotesToLoad--;
		if (this.m_chatMessageEmotesToLoad is 0u)
		{
			this.CallDeferred(
				method: "GenerateRichTextLabel"
			);
		}
	}
	
    private void GenerateRichTextLabel()
	{
		this.m_richTextLabel.FitContent    = false;
        this.m_richTextLabel.SetSize(
			size: new Vector2(
				x: ChatMessage.c_labelWidth,
				y: 10000f
			)
		);
		this.m_richTextLabel.BbcodeEnabled = true;
		this.m_richTextLabel.Text          = this.m_text;
		this.m_richTextLabel.ForceUpdateTransform();
		
		var exactHeight = this.m_richTextLabel.GetContentHeight();
		this.m_richTextLabel.SetSize(
			size: new Vector2(
				x: ChatMessage.c_labelWidth,
				y: exactHeight
			)
		);
		this.m_richTextLabel.Visible       = false;
		this.m_generateState               = GenerateStateType.Complete;
	}

	private void HandleTextAnimation(
		float delta
	)
	{
		if (this.m_hasAnimatedChatMessageEmotes)
		{
			foreach (var animatedChatMessageEmote in this.m_animatedChatMessageEmotes)
			{
				this.m_animatedChatMessageEmoteCurrentFrameRates[key: animatedChatMessageEmote] += delta;

				if (
					this.m_animatedChatMessageEmoteCurrentFrameRates[key: animatedChatMessageEmote] <
					this.m_animatedChatMessageEmoteMaxFrameRates[key: animatedChatMessageEmote]
				)
				{
					continue;
				}
				
				var previousFrame = this.m_animatedChatMessageEmoteCurrentFrameCounts[key: animatedChatMessageEmote];
				var currentFrame  = previousFrame + 1;
				if (currentFrame > this.m_animatedChatMessageEmoteMaxFrameCounts[key: animatedChatMessageEmote])
				{
					currentFrame = 0;
				}

				lock (this.m_textLock)
				{
					this.m_text = this.m_text.Replace(
						oldValue: $"{animatedChatMessageEmote}/animated_{previousFrame}.res",
						newValue: $"{animatedChatMessageEmote}/animated_{currentFrame}.res"
					);
				}

				this.m_animatedChatMessageEmoteCurrentFrameCounts[key: animatedChatMessageEmote] = currentFrame;
				this.m_animatedChatMessageEmoteCurrentFrameRates[key: animatedChatMessageEmote]  = 0f;
			}
		}
		
		var color = this.m_hasCustomNameColor ? 
			this.m_serviceColorInterpolatorNormal.GetColorByModeAsHex(
				colorMode:      this.m_customNameColor,
				colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0    
			) : 
			this.m_serviceColorInterpolatorNormal.GetColorByCurrentModeAsHex(
				colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0    
			);

		lock (this.m_textLock)
		{
			this.m_richTextLabel.Text = this.m_text.Replace(
				oldValue: ChatMessage.c_labelInterpolatorColor,
				newValue: color
			);
		}
	}
    
	private void HandleTextFade(
		float delta
	)
	{
		this.m_fadeElapsed += delta;
		switch (this.m_visibleState)
		{
			case VisibleStateType.Visible:
				if (this.m_fadeElapsed >= ChatMessage.s_fadeDelays[key: VisibleStateType.Visible])
				{
					this.m_visibleState = VisibleStateType.Fade;
					this.m_fadeElapsed = 0f;
				}
				break;
			case VisibleStateType.Fade:
				// todo: transparency
				var height = this.m_richTextLabel.GetContentHeight();
				if (this.m_fadeElapsed >= ChatMessage.s_fadeDelays[key: VisibleStateType.Fade])
				{
					this.Destroyed?.Invoke();
					this.m_generateState = GenerateStateType.Inactive;
				}
				break;

			default:
				break;
		}
	}

	private void InsertChatMessageEmotes(
		string             message,
		ChatMessageEmote[] chatMessageEmotes
	)
	{
		foreach (var chatMessageEmote in chatMessageEmotes)
		{
			var chatMessageEmoteCode = chatMessageEmote.Code;
			if (
				ChatMessage.s_chatMessageEmotesWithFileSyntax.Contains(
					value: chatMessageEmoteCode
				) is false
			)
			{
				continue;
			}

			var chatMessageEmoteCodeRename = ChatMessage.s_chatMessageEmoteBasics[chatMessageEmoteCode];
			chatMessageEmote.Code          = chatMessageEmoteCodeRename;
			this.ReplaceChatMessageCodeWithChatMessageCodeRename(
				chatMessageEmoteCode:       chatMessageEmoteCode,
				chatMessageEmoteCodeRename: chatMessageEmoteCodeRename
			);
		}
		foreach (var chatMessageEmote in chatMessageEmotes)
		{
			var chatMessageEmoteCode       = chatMessageEmote.Code;
			var chatMessageEmoteCodeLookUp = chatMessageEmoteCode.Replace(
				oldValue: $":",
				newValue: string.Empty
			);

			var chatMessageEmoteStaticPathLocal  = $"{ChatMessage.c_chatMessageEmoteDirectoryStatic}/{chatMessageEmoteCodeLookUp}";
			var chatMessageEmoteStaticPathGlobal = ProjectSettings.GlobalizePath(
				path: chatMessageEmoteStaticPathLocal
			);
			if (
				Directory.Exists(
					path: chatMessageEmoteStaticPathGlobal
				) is true
			)
			{
				var chatMessageEmotePath = $"{chatMessageEmoteStaticPathLocal}/static_0.res";
				this.ReplaceChatMessageCodeWithChatMessageImagePath(
					chatMessageEmoteCode: chatMessageEmoteCode,
					chatMessageEmotePath: chatMessageEmotePath
				);
				continue;
			}

			var chatMessageEmoteAnimatedPathLocal  = $"{ChatMessage.c_chatMessageEmoteDirectoryAnimated}/{chatMessageEmoteCodeLookUp}";
			var chatMessageEmoteAnimatedPathGlobal = ProjectSettings.GlobalizePath(
				path: chatMessageEmoteAnimatedPathLocal
			);
			if (
				Directory.Exists(
					path: chatMessageEmoteAnimatedPathGlobal
				) is true
			)
			{
				var chatMessageEmotePath = $"{ChatMessage.c_chatMessageEmoteDirectoryAnimated}/{chatMessageEmoteCodeLookUp}/animated_0.res";
				this.ReplaceChatMessageCodeWithChatMessageImagePath(
					chatMessageEmoteCode: chatMessageEmoteCode,
					chatMessageEmotePath: chatMessageEmotePath
				);

				this.m_animatedChatMessageEmotes.Add(
					item: chatMessageEmoteCodeLookUp
				);
				this.m_animatedChatMessageEmoteCurrentFrameCounts.Add(
					key:   chatMessageEmoteCodeLookUp,
					value: 0
				);
				this.m_animatedChatMessageEmoteCurrentFrameRates.Add(
					key:   chatMessageEmoteCodeLookUp,
					value: 0f
				);

				var files = Directory.GetFiles(
					path: chatMessageEmoteAnimatedPathGlobal
				);
				var frameCount = files.Length - 2;
				this.m_animatedChatMessageEmoteMaxFrameCounts.Add(
					key:   chatMessageEmoteCodeLookUp,
					value: frameCount
				);

				var file = files.First(
					predicate: file => file.EndsWith(
						value: "frame_rate.txt"
					)
				);
				var text = File.ReadAllText(
					path: file
				);
				this.m_animatedChatMessageEmoteMaxFrameRates.Add(
					key:   chatMessageEmoteCodeLookUp,
					value: text.ToFloat()
				);

				this.m_hasAnimatedChatMessageEmotes = true;
				continue;
			}
			
			this.m_chatMessageEmotesToLoad++;
			var uri = new Uri(
				uriString: $"{chatMessageEmote.Url}"
			);
			this.m_serviceGodotHttp.SendHttpRequest(
				url:                     uri.OriginalString,
				headers:                 [],
				method:                  HttpClient.Method.Get,
				json:                    string.Empty,
				requestCompletedHandler:
				(
					long     result,
					long     responseCode,
					string[] headers,
					byte[]   body
				) =>
				{
					if (responseCode >= 300u)
					{
						this.QueueFree();
						return;
					}

					var contentTypeHeader = headers[1];

					if (
						contentTypeHeader.Contains(
							value: $"png"
						)
					)
					{
						this.GenerateStaticImageFromStaticChatMessageEmote(
							body:                             body,
							chatMessageEmoteCodeLookUp:       chatMessageEmoteCodeLookUp,
							chatMessageEmoteCode:             chatMessageEmoteCode,
							chatMessageEmoteStaticPathGlobal: chatMessageEmoteStaticPathGlobal,
							staticImageFormat:                StaticImageFormatType.Png
						);
					}
					else if (
						contentTypeHeader.Contains(
							value: $"jpg"
						) ||
						contentTypeHeader.Contains(
							value: $"jpeg"
						)
					)
					{
						this.GenerateStaticImageFromStaticChatMessageEmote(
							body:                             body,
							chatMessageEmoteCodeLookUp:       chatMessageEmoteCodeLookUp,
							chatMessageEmoteCode:             chatMessageEmoteCode,
							chatMessageEmoteStaticPathGlobal: chatMessageEmoteStaticPathGlobal,
							staticImageFormat:                StaticImageFormatType.Jpg
						);
					}
					else
					{
						Task.Run(
							function: async () =>
							{
								await this.GenerateAnimatedImagesFromAnimatedChatMessageEmote(
									body:                               body,
									chatMessageEmoteCodeLookUp:         chatMessageEmoteCodeLookUp,
									chatMessageEmoteCode:               chatMessageEmoteCode,
									chatMessageEmoteAnimatedPathGlobal: chatMessageEmoteAnimatedPathGlobal
								);
							}
						);
					}
				}
			);
		}
	}
	
	private void InsertImages(
		string             message,
		ChatMessageEmote[] chatMessageEmotes
	)
	{
		var hasEmotes = chatMessageEmotes is not null && chatMessageEmotes.Length is not 0;
		if (hasEmotes is true)
		{
			this.InsertChatMessageEmotes(
				message:            message,
				chatMessageEmotes:  chatMessageEmotes
			);
		}

		if (this.m_chatMessageEmotesToLoad is 0u)
		{
			this.GenerateRichTextLabel();
		}
	}

	private void ReplaceChatMessageCodeWithChatMessageCodeRename(
		string chatMessageEmoteCode,
		string chatMessageEmoteCodeRename
	)
	{
		lock (this.m_textLock)
		{
			this.m_text = this.m_text.Replace(
				oldValue: chatMessageEmoteCode, 
				newValue: $"{chatMessageEmoteCodeRename}"
			);
		}
	}
	
	private void ReplaceChatMessageCodeWithChatMessageImagePath(
		string chatMessageEmoteCode,
		string chatMessageEmotePath
	)
	{
		lock (this.m_textLock)
		{
			this.m_text = this.m_text.Replace(
				oldValue: chatMessageEmoteCode, 
				newValue: $"[img=32x32]{chatMessageEmotePath}[/img]"
			);
		}
	}
    
    private void RetrieveResources()
    {
	    var mainFont = GD.Load<FontFile>(
		    path: "res://Resources/Fonts/Roboto-Bold.ttf"
		);
	    var emojiFont = GD.Load<FontFile>(
		    path: "res://Resources/Fonts/NotoColorEmoji-Regular.ttf"
		);

	    mainFont.Fallbacks = [emojiFont];

	    this.m_richTextLabel.AddThemeFontOverride(
		    name: "normal_font", 
		    font: mainFont
		);
	    
	    this.m_serviceColorInterpolatorNormal = Services.Services.GetService<ServiceColorInterpolatorNormal>();
	    var serviceGodots                     = Services.Services.GetService<ServiceGodots>();
	    this.m_serviceGodotHttp               = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
	    
        this.AddChild(
			node: this.m_richTextLabel
		);
    }
}