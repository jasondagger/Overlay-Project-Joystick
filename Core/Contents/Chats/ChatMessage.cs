
using Godot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Https;
using Overlay.Core.Services.PastelInterpolators;

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

	private void GeneratePngFromStaticEmote(
		byte[] body,
		string lookUpEmoteName,
		string originalEmoteName,
		string emoteDirectory
	)
	{
		// todo:
		// ApplicationManager.CreateStaticEmoteDirectory(
		//     emoteName: lookUpChatMessageEmoteName
		// );
		var image = Godot.Image.CreateEmpty(
			width:      _ = ChatMessage.c_chatMessageEmoteWidth,
			height:     _ = ChatMessage.c_chatMessageEmoteHeight,
			useMipmaps: _ = false,
			format:     _ = Godot.Image.Format.Rgba8
		);
		
		_ = image.LoadPngFromBuffer(
			buffer: _ = body
		);
		var imageTexture = _ = ImageTexture.CreateFromImage(
			image: _ = image
		);
		var emotePath = $"{_ = emoteDirectory}\\static_0.res";
		_ = ResourceSaver.Save(
			resource: _ = imageTexture,
			path:     _ = emotePath
		);
		this.ReplaceTextWithChatMessageEmotes(
			chatMessageEmoteName: _ = originalEmoteName,
			chatMessageEmotePath: _ = emotePath
		);
		
		_ = this.m_chatMessageEmotesToLoad--;
		if (_ = this.m_chatMessageEmotesToLoad is 0u)
		{
			this.GenerateRichTextLabel();
		}
	}
	
	private void GeneratePngsFromAnimatedEmote(
		byte[] body,
		string lookUpChatMessageEmoteName,
		string originalChatMessageEmoteName,
		string chatMessageEmoteDirectory
	)
	{
		// todo:
		// ApplicationManager.CreateAnimatedEmoteDirectory(
		//     emoteName: lookUpChatMessageEmoteName
		// );
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
		
		 var targetFrameDelayDirectory = string.Empty;/*ApplicationManager.GetAnimatedEmoteDirectory(
		     emoteName: _ = lookUpChatMessageEmoteName
		 );*/
		 var frameDelayFile = $"{targetFrameDelayDirectory}\\frame_rate.txt";
		 File.WriteAllText(
		     path: frameDelayFile,
		     contents: frameDelayText
		 );
		using var gifMemoryStream = new MemoryStream(
			buffer: _ = body
		);
		using var gifImage = System.Drawing.Image.FromStream(
			stream: _ = gifMemoryStream
		);
		var frameDimension = new FrameDimension(
			guid: _ = gifImage.FrameDimensionsList[0]
		);
		var frameCount = _ = gifImage.GetFrameCount(
			dimension: _ = frameDimension
		);
		var totalFrames = _ = frameCount - 1;
		for (var i = _ = 0; _ = i < frameCount; _ = i++)
		{
			_ = gifImage.SelectActiveFrame(
				dimension:  _ = frameDimension, 
				frameIndex: _ = i
			);
			using var frame = new System.Drawing.Bitmap(
				width:  _ = gifImage.Width, 
				height: _ = gifImage.Height
			);
			using (
				var graphics = _ = Graphics.FromImage(
					image: _ = frame
				)
			)
			{
				graphics.DrawImage(
					image: _ = gifImage, 
					point: _ = Point.Empty
				);
			}
			using var frameStream = new MemoryStream();
			frame.Save(
				stream: _ = frameStream,
				format: _ = ImageFormat.Png
			);
			var imageBytes = frameStream.ToArray();
			var image = _ = Godot.Image.CreateEmpty(
				width:      _ = ChatMessage.c_chatMessageEmoteWidth,
				height:     _ = ChatMessage.c_chatMessageEmoteHeight,
				useMipmaps: _ = false,
				format:     _ = Godot.Image.Format.Rgba8
			);
			_ = image.LoadPngFromBuffer(
				buffer: _ = imageBytes
			);
			var imageTexture = _ = ImageTexture.CreateFromImage(
				image: _ = image
			);
			var emoteFile = _ = $"{_ = chatMessageEmoteDirectory}\\animated_{_ = i}.res";
			_ = ResourceSaver.Save(
				resource: _ = imageTexture,
				path:     _ = emoteFile
			);
		}
		var emotePath = _ = $"{_ = chatMessageEmoteDirectory}/animated_0.res";
		this.ReplaceTextWithChatMessageEmotes(
			chatMessageEmoteName: _ = originalChatMessageEmoteName,
			chatMessageEmotePath: _ = emotePath
		);
		this.m_animatedChatMessageEmotes.Add(
			item: _ = originalChatMessageEmoteName
		);
		this.m_animatedChatMessageEmoteCurrentFrameCounts.Add(
			key:   _ = originalChatMessageEmoteName,
			value: _ = 0
		);
		this.m_animatedChatMessageEmoteMaxFrameCounts.Add(
			key:   _ = originalChatMessageEmoteName,
			value: _ = totalFrames
		);
		this.m_animatedChatMessageEmoteCurrentFrameRates.Add(
			key:   _ = originalChatMessageEmoteName,
			value: _ = 0f
		);
		this.m_animatedChatMessageEmoteMaxFrameRates.Add(
			key:   _ = originalChatMessageEmoteName,
			value: _ = frameDelayText.ToFloat()
		);
		
		_ = this.m_hasAnimatedChatMessageEmotes = _ = true;
		_ = this.m_chatMessageEmotesToLoad--;
		if (_ = this.m_chatMessageEmotesToLoad is 0u)
		{
			this.GenerateRichTextLabel();
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
		return;
		
		foreach (var chatMessageEmote in chatMessageEmotes)
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
			
			_ = chatMessageEmote.Code = _ = ChatMessage.s_chatMessageEmoteBasics[chatMessageEmoteCode];
		}
		
		foreach (var chatMessageEmote in _ = chatMessageEmotes)
		{
			var originalEmoteName = _ = chatMessageEmote.Code;
			var lookUpEmoteName   = _ = originalEmoteName.Replace(
				oldValue: _ = ":",
				newValue: _ = string.Empty
			);

			var emotePathStatic = string.Empty;/*ApplicationManager.GetStaticEmoteDirectory(
				emoteName: lookUpEmoteName
			);*/
			if (
				_ = Directory.Exists(
					path: _ = emotePathStatic
				) is true
			)
			{
				var emotePath = _ = $"{_ = ChatMessage.c_chatMessageEmoteDirectoryStatic}/{_ = lookUpEmoteName}/static_0.res";
				this.ReplaceTextWithChatMessageEmotes(
					chatMessageEmoteName: originalEmoteName,
					chatMessageEmotePath: emotePath
				);
				continue;
			}

			var emotePathAnimated = string.Empty;/*ApplicationManager.GetAnimatedEmoteDirectory(
				emoteName: lookUpEmoteName
			);*/
			if (
				_ = Directory.Exists(
					path: emotePathAnimated
				) is true
			)
			{
				var emotePath = $"{_ = ChatMessage.c_chatMessageEmoteDirectoryAnimated}/{_ = lookUpEmoteName}/animated_0.res";
				this.ReplaceTextWithChatMessageEmotes(
					chatMessageEmoteName: _ = originalEmoteName,
					chatMessageEmotePath: _ = emotePath
				);

				this.m_animatedChatMessageEmotes.Add(
					item: originalEmoteName
				);
				this.m_animatedChatMessageEmoteCurrentFrameCounts.Add(
					key: originalEmoteName,
					value: 0
				);
				this.m_animatedChatMessageEmoteCurrentFrameRates.Add(
					key: originalEmoteName,
					value: 0f
				);

				var files = _ = Directory.GetFiles(
					path: _ = emotePathAnimated
				);
				var frameCount = _ = files.Length - 2;
				this.m_animatedChatMessageEmoteMaxFrameCounts.Add(
					key:   _ = originalEmoteName,
					value: _ = frameCount
				);

				var file = _ = files.Last();
				var text = _ = File.ReadAllText(
					path: _ = file
				);
				this.m_animatedChatMessageEmoteMaxFrameRates.Add(
					key:   _ = originalEmoteName,
					value: _ = text.ToFloat()
				);

				_ = this.m_hasAnimatedChatMessageEmotes = _ = true;
				continue;
			}
			
			_ = this.m_chatMessageEmotesToLoad++;
			var uri = _ = new Uri(
				$"{chatMessageEmotes[0].Url}"
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
							value: _ = "png"
						)
					)
					{
						this.GeneratePngFromStaticEmote(
							body:              _ = body,
							lookUpEmoteName:   _ = lookUpEmoteName,
							originalEmoteName: _ = originalEmoteName,
							emoteDirectory:    _ = emotePathStatic
						);
					}
					else
					{
						this.GeneratePngsFromAnimatedEmote(
							body:                         _ = body,
							lookUpChatMessageEmoteName:   _ = lookUpEmoteName,
							originalChatMessageEmoteName: _ = originalEmoteName,
							chatMessageEmoteDirectory:    _ = emotePathAnimated
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
	
	private void ReplaceTextWithChatMessageEmotes(
		string chatMessageEmoteName,
		string chatMessageEmotePath
	)
	{
		lock (_ = this.m_textLock)
		{                                                                    
			//m_text = $"  {splitText[i].Replace(oldValue: chatMessageEmoteName, newValue: $"[img]{chatMessageEmotePath}[/img]")}";
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