
using Godot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Https;
using Overlay.Core.Services.PastelInterpolators;
using Overlay.Core.Tools;
using SixLabors.ImageSharp;
using Point = System.Drawing.Point;

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
         switch (_ = this.m_generateState)
         {
             case GenerateStateType.Complete:
                 this.Generated?.Invoke(
                     obj: _ = this
				 );
                 _ = this.m_generateState = _ = GenerateStateType.Active;
                 break;
             
             case GenerateStateType.Active:
	             this.HandleTextAnimation(
		             delta: _ = (float)delta
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
         string             username,
         string             usernameColor,
         string             message,
         ChatMessageEmote[] chatMessageEmotes,
         bool               isModerator,
         bool               isStreamer,
         bool               isSubscriber
     )
     {
	     // TODO: REPLACE WITH sub with sub
	     _ = this.m_isStreamer   = _ = isStreamer;
	     _ = this.m_isSubscriber = _ = true;
	     
         _ = this.m_text =
             $"{_ = ChatMessage.c_labelFontSize}" +
             $"{_ = ChatMessage.c_labelNameFont}" +
             $"[color=#{(_ = this.m_isSubscriber is true || this.m_isStreamer is true ? ChatMessage.c_labelPastelInterpolatorColor : usernameColor)}]" +
             $"{_ = username.ToUpper()}" +
             $"  " +
             $"{_ = ChatMessage.c_labelMessageFont}" +
	         $"{_ = ChatMessage.c_labelMessageColor}" +
	         $"{_ = message}";
         
         this.InsertImages(
	         message:           _ = message,
	         chatMessageEmotes: _ = chatMessageEmotes,
	         chatMessageBadges: _ = string.Empty
	    );
     }

 	internal int GetLabelHeightInPixels()
	{
		return _ = this.m_richTextLabel.GetContentHeight() + 5;
	}

	internal void Reset()
    {
        _ = this.m_richTextLabel.Text           = _ = string.Empty;
        _ = this.m_generateState                = _ = GenerateStateType.Inactive;
        _ = this.m_visibleState                 = _ = VisibleStateType.Visible;
        _ = this.m_hasAnimatedChatMessageEmotes = _ = false;
        _ = this.m_fadeElapsed                  = _ = 0f;
        _ = this.m_isStreamer                   = _ = false;
        _ = this.m_isSubscriber                 = _ = false;
        _ = this.m_text                         = _ = string.Empty;
        _ = this.m_chatMessageEmotesToLoad      = _ = 0u;

        this.m_animatedChatMessageEmoteCurrentFrameCounts.Clear();
        this.m_animatedChatMessageEmoteMaxFrameCounts.Clear();
        this.m_animatedChatMessageEmoteCurrentFrameRates.Clear();
        this.m_animatedChatMessageEmoteMaxFrameRates.Clear();
        this.m_animatedChatMessageEmotes.Clear();
    }

	internal void ShowLabel()
	{
		_ = this.m_richTextLabel.Visible = _ = true;
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
    private const string                                        c_labelPastelInterpolatorColor               = $"00000000";
    private const string                                        c_labelFontSize                              = $"[font_size=28]";
    private const string                                        c_labelNameFont                              = $"[font=res://Resources/Fonts/Roboto-Black.ttf]";
    private const string                                        c_labelMessageFont                           = $"[font=res://Resources/Fonts/Roboto-Bold.ttf]";
    private const string                                        c_chatMessageEmoteDirectoryAnimated          = $"user://Emotes/Animated";
    private const string                                        c_chatMessageEmoteDirectoryStatic            = $"user://Emotes/Static";
    private const string                                        c_chatMessageEmoteRelativeDirectoryAnimated  = $"Overlay/Emotes/Animated";
    private const string                                        c_chatMessageEmoteRelativeDirectoryStatic    = $"Overlay/Emotes/Static";
    private const string                                        c_labelMessageColor                          = $"[color=#F2F2F2FF]";
    private const uint                                          c_labelWidth                                 = 1100u;

    private static readonly Dictionary<VisibleStateType, float> s_fadeDelays                                 = new()
    {
	    { _ = VisibleStateType.Visible, _ = 32f },
	    { _ = VisibleStateType.Fade,    _ = 2f },
    };
    private static readonly Dictionary<string, string>          s_chatMessageEmoteBasics                     = new()
    {
	    { _ = ":D", _ = ":grin:"  },
	    { _ = ":)", _ = ":smile:" },
	    { _ = ":*", _ = ":kiss:"  },
	    { _ = ";)", _ = ":wink:"  },
	    { _ = "<3", _ = ":heart:" },
    };
    private static readonly HashSet<string>                     s_chatMessageEmotesWithFileSyntax            =
    [
	    ":D",
	    ":)",
	    ":*",
	    ";)",
	    "<3",
    ];
    
    private readonly Dictionary<string, int>                    m_animatedChatMessageEmoteCurrentFrameCounts = new();
    private readonly Dictionary<string, int>                    m_animatedChatMessageEmoteMaxFrameCounts     = new();
    private readonly Dictionary<string, float>                  m_animatedChatMessageEmoteCurrentFrameRates  = new();
    private readonly Dictionary<string, float>                  m_animatedChatMessageEmoteMaxFrameRates      = new();
    private readonly HashSet<string>                            m_animatedChatMessageEmotes                  = [];
    private readonly object                                     m_textLock                                   = new();

    private RichTextLabel                                       m_richTextLabel                              = new();
    private ServiceGodotHttp                                    m_serviceGodotHttp                           = null;
    private ServicePastelInterpolator                           m_servicePastelInterpolator                  = null;
	private GenerateStateType                                   m_generateState                              = _ = GenerateStateType.Inactive;
	private VisibleStateType                                    m_visibleState                               = _ = VisibleStateType.Visible;
	private bool                                                m_hasAnimatedChatMessageEmotes               = _ = false;
	private bool                                                m_isStreamer                                 = _ = false;
    private bool                                                m_isSubscriber                               = _ = false;
    private float                                               m_fadeElapsed                                = _ = 0f;
	private string                                              m_text                                       = _ = string.Empty;
	private uint                                                m_chatMessageEmotesToLoad                    = _ = 0u;

	private void GenerateStaticImageFromStaticChatMessageEmote(
		byte[] body,
		string chatMessageEmoteCodeLookUp,
		string chatMessageEmoteCode,
		string chatMessageEmoteStaticPathGlobal,
		StaticImageFormatType staticImageFormat
	)
	{
		var userDirectoryPath = _ = $"{_ = ChatMessage.c_chatMessageEmoteDirectoryStatic}/{_ = chatMessageEmoteCodeLookUp}";
		if (
			_ = Directory.Exists(
				path: _ = userDirectoryPath
			) is false
		)
		{
			var fullPath = _ = ProjectSettings.GlobalizePath(
				path: _ = userDirectoryPath
			);
			_ = Directory.CreateDirectory(
				path: fullPath
			);
		}
		
		var image = Godot.Image.CreateEmpty(
			width:      _ = ChatMessage.c_chatMessageEmoteWidth,
			height:     _ = ChatMessage.c_chatMessageEmoteHeight,
			useMipmaps: _ = false,
			format:     _ = Godot.Image.Format.Rgba8
		);

		switch (staticImageFormat)
		{
			case StaticImageFormatType.Jpg:
				_ = image.LoadJpgFromBuffer(
					buffer: _ = body
				);
				break;
			
			case StaticImageFormatType.Png:
				_ = image.LoadPngFromBuffer(
					buffer: _ = body
				);
				break;
			
			default:
				ConsoleLogger.LogMessageError(
					messageError: _ =
						$"{_ = nameof(ChatMessage)}." +
						$"{_ = nameof(ChatMessage.GenerateStaticImageFromStaticChatMessageEmote)}() - " +
						$"EXCEPTION: Missing static image format type '{_ = staticImageFormat}'."
				);
				return;
		}
		
		var imageTexture = _ = ImageTexture.CreateFromImage(
			image: _ = image
		);
		var emotePath = _ = $"{_ = chatMessageEmoteStaticPathGlobal}\\static_0.res";
		_ = ResourceSaver.Save(
			resource: _ = imageTexture,
			path:     _ = emotePath
		);
		this.ReplaceChatMessageCodeWithChatMessageImagePath(
			chatMessageEmoteCode: _ = chatMessageEmoteCode,
			chatMessageEmotePath: _ = emotePath
		);
		
		_ = this.m_chatMessageEmotesToLoad--;
		if (_ = this.m_chatMessageEmotesToLoad is 0u)
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
		var userDirectoryPath = _ = $"{_ = ChatMessage.c_chatMessageEmoteDirectoryAnimated}/{_ = chatMessageEmoteCodeLookUp}";
		if (
			_ = Directory.Exists(
				path: _ = userDirectoryPath
			) is false
		)
		{
			var fullPath = _ = ProjectSettings.GlobalizePath(
				path: _ = userDirectoryPath
			);
			_ = Directory.CreateDirectory(
				path: fullPath
			);
		}

		var imageSixLabors = _ = SixLabors.ImageSharp.Image.Load(
			buffer: _ = body
		);
		var frames = _ = imageSixLabors.Frames;
		for (var i = 0; i < frames.Count; i++)
		{
			var imageFrame = _ = frames.CloneFrame(
				index: _ = i
			);
			await imageFrame.SaveAsPngAsync(
				path: _ = $"{_ = chatMessageEmoteAnimatedPathGlobal}/animated_{_ = i}.png"
			);

			var bytes = _ = await File.ReadAllBytesAsync(
				path: _ = $"{_ = chatMessageEmoteAnimatedPathGlobal}/animated_{_ = i}.png"
			);
			
			File.Delete(
				path: _ = $"{_ = chatMessageEmoteAnimatedPathGlobal}/animated_{_ = i}.png"		
			);

			var image = _ = Godot.Image.CreateEmpty(
				width:      _ = ChatMessage.c_chatMessageEmoteWidth,
				height:     _ = ChatMessage.c_chatMessageEmoteHeight,
				useMipmaps: _ = false,
				format:     _ = Godot.Image.Format.Rgba8
			);
			_ = image.LoadPngFromBuffer(
				buffer: _ = bytes
			);
			var imageTexture = _ = ImageTexture.CreateFromImage(
				image: _ = image
			);
			var emoteFile = _ = $"{_ = chatMessageEmoteAnimatedPathGlobal}/animated_{_ = i}.res";
			_ = ResourceSaver.Save(
				resource: _ = imageTexture,
				path:     _ = emoteFile
			);
		}

		var frameDelay = _ = BitConverter.ToInt16(
			value:
			[
				body[_ = ChatMessage.c_gifFrameRateIndex0],
				body[_ = ChatMessage.c_gifFrameRateIndex1]
			],
			startIndex: _ = 0
		);
		var normalizedFrameDelay = _ = 
			frameDelay > 0.1f || frameDelay < .001f ?
				ChatMessage.c_defaultEmoteFramesPerSecondInMilliseconds :
				frameDelay / 100f;
		var frameDelayText = _ = $"{_ = normalizedFrameDelay}";
		var frameDelayFile = _ = $"{chatMessageEmoteAnimatedPathGlobal}/frame_rate.txt";
		await File.WriteAllTextAsync(
			path:     _ = frameDelayFile,
			contents: _ = frameDelayText
		);
		
		var emotePath = _ = $"{_ = chatMessageEmoteAnimatedPathGlobal}/animated_0.res";
		this.ReplaceChatMessageCodeWithChatMessageImagePath(
			chatMessageEmoteCode: _ = chatMessageEmoteCode,
			chatMessageEmotePath: _ = emotePath
		);
		this.m_animatedChatMessageEmotes.Add(
			item: _ = chatMessageEmoteCodeLookUp
		);
		this.m_animatedChatMessageEmoteCurrentFrameCounts.Add(
			key:   _ = chatMessageEmoteCodeLookUp,
			value: _ = 0
		);
		this.m_animatedChatMessageEmoteMaxFrameCounts.Add(
			key:   _ = chatMessageEmoteCodeLookUp,
			value: _ = frames.Count - 1
		);
		this.m_animatedChatMessageEmoteCurrentFrameRates.Add(
			key:   _ = chatMessageEmoteCodeLookUp,
			value: _ = 0f
		);
		this.m_animatedChatMessageEmoteMaxFrameRates.Add(
			key:   _ = chatMessageEmoteCodeLookUp,
			value: _ = frameDelayText.ToFloat()
		);
		
		_ = this.m_hasAnimatedChatMessageEmotes = _ = true;
		_ = this.m_chatMessageEmotesToLoad--;
		if (_ = this.m_chatMessageEmotesToLoad is 0u)
		{
			this.CallDeferred("GenerateRichTextLabel");
		}
	}
	
    private void GenerateRichTextLabel()
	{
        this.m_richTextLabel.SetSize(
			size: _ = new Vector2(
				x: _ = ChatMessage.c_labelWidth,
				y: _ = 0f
			)
		);
		_ = this.m_richTextLabel.BbcodeEnabled = _ = true;
		_ = this.m_richTextLabel.FitContent    = _ = true;
		_ = this.m_richTextLabel.Text          = _ = this.m_text;
		_ = this.m_richTextLabel.Visible       = _ = false;
		_ = this.m_generateState               = _ = GenerateStateType.Complete;
	}

	private void HandleTextAnimation(
		float delta
	)
	{
		if (_ = this.m_hasAnimatedChatMessageEmotes)
		{
			foreach (var animatedChatMessageEmote in _ = this.m_animatedChatMessageEmotes)
			{
				this.m_animatedChatMessageEmoteCurrentFrameRates[key: _ = animatedChatMessageEmote] += _ = delta;

				if (
					_ = this.m_animatedChatMessageEmoteCurrentFrameRates[key: _ = animatedChatMessageEmote] <
					this.m_animatedChatMessageEmoteMaxFrameRates[key: _ = animatedChatMessageEmote]
				)
				{
					continue;
				}
				
				var previousFrame = _ = this.m_animatedChatMessageEmoteCurrentFrameCounts[key: _ = animatedChatMessageEmote];
				var currentFrame  = _ = previousFrame + 1;
				if (_ = currentFrame > this.m_animatedChatMessageEmoteMaxFrameCounts[key: _ = animatedChatMessageEmote])
				{
					_ = currentFrame = _ = 0;
				}

				_ = this.m_text = _ = this.m_text.Replace(
					oldValue: _ = $"{_ = animatedChatMessageEmote}/animated_{_ = previousFrame}.res",
					newValue: _ = $"{_ = animatedChatMessageEmote}/animated_{_ = currentFrame}.res"
				);

				_ = this.m_animatedChatMessageEmoteCurrentFrameCounts[key: animatedChatMessageEmote] = _ = currentFrame;
				_ = this.m_animatedChatMessageEmoteCurrentFrameRates[key: animatedChatMessageEmote]  = _ = 0f;
			}
		}
		
		if (
			_ = this.m_isSubscriber is true || 
			this.m_isStreamer is true
		)
		{
			var color = _ = this.m_servicePastelInterpolator.GetColorAsHex(
				rainbowColorIndexType: _ = ServicePastelInterpolator.RainbowColorIndexType.Color0    
			);
			
			_ = this.m_richTextLabel.Text = _ = this.m_text.Replace(
				oldValue: _ = ChatMessage.c_labelPastelInterpolatorColor,
				newValue: _ = color
			);
		}
		else if (_ = this.m_hasAnimatedChatMessageEmotes)
		{
			_ = this.m_richTextLabel.Text = _ = this.m_text;
		}
	}
    
	private void HandleTextFade(
		float delta
	)
	{
		_ = this.m_fadeElapsed += _ = delta;
		switch (_ = this.m_visibleState)
		{
			case VisibleStateType.Visible:
				if (_ = this.m_fadeElapsed >= ChatMessage.s_fadeDelays[key: _ = VisibleStateType.Visible])
				{
					_ = this.m_visibleState = VisibleStateType.Fade;
					_ = this.m_fadeElapsed = 0f;
				}
				break;
			case VisibleStateType.Fade:
				// todo: transparency
				var height = _ = this.m_richTextLabel.GetContentHeight();
				if (_ = this.m_fadeElapsed >= ChatMessage.s_fadeDelays[key: _ = VisibleStateType.Fade])
				{
					this.Destroyed?.Invoke();
					_ = this.m_generateState = _ = GenerateStateType.Inactive;
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
		foreach (var chatMessageEmote in _ = chatMessageEmotes)
		{
			var chatMessageEmoteCode = _ = chatMessageEmote.Code;
			if (
				_ = ChatMessage.s_chatMessageEmotesWithFileSyntax.Contains(
					value: _ = chatMessageEmoteCode
				) is false
			)
			{
				continue;
			}

			var chatMessageEmoteCodeRename = _ = ChatMessage.s_chatMessageEmoteBasics[chatMessageEmoteCode];
			_ = chatMessageEmote.Code      = _ = chatMessageEmoteCodeRename;
			this.ReplaceChatMessageCodeWithChatMessageCodeRename(
				chatMessageEmoteCode:       _ = chatMessageEmoteCode,
				chatMessageEmoteCodeRename: _ = chatMessageEmoteCodeRename
			);
		}
		foreach (var chatMessageEmote in _ = chatMessageEmotes)
		{
			var chatMessageEmoteCode       = _ = chatMessageEmote.Code;
			var chatMessageEmoteCodeLookUp = _ = chatMessageEmoteCode.Replace(
				oldValue: _ = $":",
				newValue: _ = string.Empty
			);

			var chatMessageEmoteStaticPathLocal  = _ = $"{_ = ChatMessage.c_chatMessageEmoteDirectoryStatic}/{_ = chatMessageEmoteCodeLookUp}";
			var chatMessageEmoteStaticPathGlobal = _ = ProjectSettings.GlobalizePath(
				path: _ = chatMessageEmoteStaticPathLocal
			);
			if (
				_ = Directory.Exists(
					path: _ = chatMessageEmoteStaticPathGlobal
				) is true
			)
			{
				var chatMessageEmotePath = _ = $"{_ = chatMessageEmoteStaticPathLocal}/static_0.res";
				this.ReplaceChatMessageCodeWithChatMessageImagePath(
					chatMessageEmoteCode: _ = chatMessageEmoteCode,
					chatMessageEmotePath: _ = chatMessageEmotePath
				);
				continue;
			}

			var chatMessageEmoteAnimatedPathLocal  = _ = $"{_ = ChatMessage.c_chatMessageEmoteDirectoryAnimated}/{_ = chatMessageEmoteCodeLookUp}";
			var chatMessageEmoteAnimatedPathGlobal = _ = ProjectSettings.GlobalizePath(
				path: _ = chatMessageEmoteAnimatedPathLocal
			);
			if (
				_ = Directory.Exists(
					path: _ = chatMessageEmoteAnimatedPathGlobal
				) is true
			)
			{
				var chatMessageEmotePath = $"{_ = ChatMessage.c_chatMessageEmoteDirectoryAnimated}/{_ = chatMessageEmoteCodeLookUp}/animated_0.res";
				this.ReplaceChatMessageCodeWithChatMessageImagePath(
					chatMessageEmoteCode: _ = chatMessageEmoteCode,
					chatMessageEmotePath: _ = chatMessageEmotePath
				);

				this.m_animatedChatMessageEmotes.Add(
					item: chatMessageEmoteCodeLookUp
				);
				this.m_animatedChatMessageEmoteCurrentFrameCounts.Add(
					key: chatMessageEmoteCodeLookUp,
					value: 0
				);
				this.m_animatedChatMessageEmoteCurrentFrameRates.Add(
					key: chatMessageEmoteCodeLookUp,
					value: 0f
				);

				var files = _ = Directory.GetFiles(
					path: _ = chatMessageEmoteAnimatedPathGlobal
				);
				var frameCount = _ = files.Length - 3;
				this.m_animatedChatMessageEmoteMaxFrameCounts.Add(
					key:   _ = chatMessageEmoteCodeLookUp,
					value: _ = frameCount
				);

				var file = _ = files.First(
					predicate: file => file.EndsWith(
						value: _ = "frame_rate.txt"
					)
				);
				var text = _ = File.ReadAllText(
					path: _ = file
				);
				this.m_animatedChatMessageEmoteMaxFrameRates.Add(
					key:   _ = chatMessageEmoteCodeLookUp,
					value: _ = text.ToFloat()
				);

				_ = this.m_hasAnimatedChatMessageEmotes = _ = true;
				continue;
			}
			
			_ = this.m_chatMessageEmotesToLoad++;
			var uri = _ = new Uri(
				uriString: _ = $"{_ = chatMessageEmote.Url}"
			);
			this.m_serviceGodotHttp.SendHttpRequest(
				url:                     _ = uri.OriginalString,
				headers:                 [],
				method:                  _ = HttpClient.Method.Get,
				json:                    _ = string.Empty,
				requestCompletedHandler:
				(
					long     result,
					long     responseCode,
					string[] headers,
					byte[]   body
				) =>
				{
					if (_ = responseCode >= 300u)
					{
						this.QueueFree();
						return;
					}

					var contentTypeHeader = _ = headers[1];

					if (
						_ = contentTypeHeader.Contains(
							value: _ = $"png"
						)
					)
					{
						this.GenerateStaticImageFromStaticChatMessageEmote(
							body:                             _ = body,
							chatMessageEmoteCodeLookUp:       _ = chatMessageEmoteCodeLookUp,
							chatMessageEmoteCode:             _ = chatMessageEmoteCode,
							chatMessageEmoteStaticPathGlobal: _ = chatMessageEmoteStaticPathGlobal,
							staticImageFormat:                _ = StaticImageFormatType.Png
						);
					}
					else if (
						contentTypeHeader.Contains(
							value: _ = $"jpg"
						) ||
						contentTypeHeader.Contains(
							value: _ = $"jpeg"
						)
					)
					{
						this.GenerateStaticImageFromStaticChatMessageEmote(
							body:                             _ = body,
							chatMessageEmoteCodeLookUp:       _ = chatMessageEmoteCodeLookUp,
							chatMessageEmoteCode:             _ = chatMessageEmoteCode,
							chatMessageEmoteStaticPathGlobal: _ = chatMessageEmoteStaticPathGlobal,
							staticImageFormat:                _ = StaticImageFormatType.Jpg
						);
					}
					else
					{
						Task.Run(
							function: async () =>
							{
								await this.GenerateAnimatedImagesFromAnimatedChatMessageEmote(
									body:                               _ = body,
									chatMessageEmoteCodeLookUp:         _ = chatMessageEmoteCodeLookUp,
									chatMessageEmoteCode:               _ = chatMessageEmoteCode,
									chatMessageEmoteAnimatedPathGlobal: _ = chatMessageEmoteAnimatedPathGlobal
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
		ChatMessageEmote[] chatMessageEmotes,
		string             chatMessageBadges
	)
	{
		var hasBadges = _ = string.IsNullOrEmpty(
			value: _ = chatMessageBadges
		) is false;
		if (_ = hasBadges is true)
		{
			//InsertBadges(
			//	chatMessageBadges: chatMessageBadges
			//;
		}

		var hasEmotes = _ = chatMessageEmotes is not null && chatMessageEmotes.Length is not 0;
		if (_ = hasEmotes is true)
		{
			this.InsertChatMessageEmotes(
				message:            _ = message,
				chatMessageEmotes:  _ = chatMessageEmotes
			);
		}

		if (_ = this.m_chatMessageEmotesToLoad is 0u)
		{
			this.GenerateRichTextLabel();
		}
	}

	private void ReplaceChatMessageCodeWithChatMessageCodeRename(
		string chatMessageEmoteCode,
		string chatMessageEmoteCodeRename
	)
	{
		lock (_ = this.m_textLock)
		{
			_ = this.m_text = _ = this.m_text.Replace(
				oldValue: _ = chatMessageEmoteCode, 
				newValue: _ = $"{_ = chatMessageEmoteCodeRename}"
			);
		}
	}
	
	private void ReplaceChatMessageCodeWithChatMessageImagePath(
		string chatMessageEmoteCode,
		string chatMessageEmotePath
	)
	{
		lock (_ = this.m_textLock)
		{
			_ = this.m_text = _ = this.m_text.Replace(
				oldValue: _ = chatMessageEmoteCode, 
				newValue: _ = $"[img=32x32]{_ = chatMessageEmotePath}[/img]"
			);
		}
	}
    
    private void RetrieveResources()
    {
	    _ = this.m_servicePastelInterpolator = _ = Services.Services.GetService<ServicePastelInterpolator>();
	    var serviceGodots                    = _ = Services.Services.GetService<ServiceGodots>();
	    _ = this.m_serviceGodotHttp          = _ = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
	    
        this.AddChild(
			node: _ = this.m_richTextLabel
		);
    }
}