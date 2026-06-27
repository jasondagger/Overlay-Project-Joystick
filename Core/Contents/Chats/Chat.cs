
using Godot;
using Overlay.Core.Services.ColorInterpolators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Overlay.Core.Contents.Chats;

internal sealed partial class Chat() :
    Control()
{
	public override void _EnterTree()
	{
		base._EnterTree();
		
		this.RetrieveResources();
		Chat.StartBadgeColorUpdater();
	}
	
	public override void _Process(
		double delta
	)
	{
		base._Process(
			delta: delta
		);

		this.ProcessQueuedChatMessage();
		this.ProcessQueuedChatMessageData();
	}
	
    public override void _Ready()
    {
	    base._Ready();
	    
	    this.PopulateChatMessageCache();
    }
    
    internal static void AddChatMessageToInstances(
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
	    foreach (var instance in Chat.s_instances)
	    {
		    instance.AddChatMessage(
			    username:            username,
			    hasCustomBadgeColor: hasCustomBadgeColor,
			    customBadgeColor:    customBadgeColor,
			    hasCustomNameColor:  hasCustomNameColor,
			    customNameColor:     customNameColor,
			    message:             message,
			    chatMessageEmotes:   chatMessageEmotes,
			    isModerator:         isModerator,
			    isStreamer:          isStreamer,
			    isSubscriber:        isSubscriber,
			    isBot:               isBot
		    );
	    }
    }

    internal static void AddDebugMessageToInstances(
	    string message
	)
    {
	    foreach (var instance in Chat.s_instances)
	    {
		    instance.AddDebugMessage(
			    message: message
			);
	    }
    }

    internal static void UpdateChatMessageBadgeColorForInstances(
        string                            username,
	    bool                              hasCustomColor,
	    ServiceColorInterpolatorColorMode customColor
	)
    {
	    foreach (var instance in Chat.s_instances)
	    {
		    instance.UpdateChatMessageBadgeColor(
			    username:       username,
			    hasCustomColor: hasCustomColor,
			    customColor:    customColor
		    );
	    }
    }

    internal static void UpdateChatMessageNameColorForInstances(
	    string                            username,
	    bool                              hasCustomColor,
	    ServiceColorInterpolatorColorMode customColor
	)
    {
	    foreach (var instance in Chat.s_instances)
	    {
		    instance.UpdateChatMessageNameColor(
			    username:       username,
			    hasCustomColor: hasCustomColor,
			    customColor:    customColor
		    );
	    }
    }
    
    private sealed class ChatMessageData
    {
	    internal readonly string                    Username;
	    internal readonly string                    Message;
	    internal readonly ChatMessageEmote[]        ChatMessageEmotes   = null;
	    internal readonly bool                      IsModerator         = false;
	    internal readonly bool                      IsStreamer          = false;
	    internal readonly bool                      IsSubscriber        = false;
	    internal readonly bool                      IsBot               = false;
	    
	    internal  bool                              HasCustomNameColor  = false;
	    internal  ServiceColorInterpolatorColorMode CustomNameColor;
	    internal  bool                              HasCustomBadgeColor = false;
	    internal  ServiceColorInterpolatorColorMode CustomBadgeColor;

	    public ChatMessageData(
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
		    this.Username            = username;
		    this.HasCustomBadgeColor = hasCustomBadgeColor;
		    this.CustomBadgeColor    = customBadgeColor;
		    this.HasCustomNameColor  = hasCustomNameColor;
		    this.CustomNameColor     = customNameColor;
		    this.Message             = message;
		    this.ChatMessageEmotes   = chatMessageEmotes;
		    this.IsModerator         = isModerator;
		    this.IsStreamer          = isStreamer;
		    this.IsSubscriber        = isSubscriber;
		    this.IsBot               = isBot;
	    }
    };

    private enum BadgeType
    {
	    Bot,
	    Moderator,
	    Streamer,
	    Subscriber,
    }
	
    private const int    c_chatMessageCacheSize = 20;
    private const int    c_maxPixelCount        = 400;
    private const int    c_pixelSpacing         = 2;
    private const string c_illegalBbCodePattern =
        $"\\[(" +
        $"alm" +
        $"|b" +
        $"|bgcolor" +
        $"|cell" +
        $"|center" +
        $"|code" +
        $"|color" +
        $"|dropcap" +
        $"|fade" +
        $"|fgcolor" +
        $"|fill" +
        $"|font" +
        $"|font_size" +
        $"|fsi" +
        $"|hint" +
        $"|i" +
        $"|img" +
        $"|indent" +
        $"|lb" +
        $"|left" +
        $"|lre" +
        $"|lri" +
        $"|lrm" +
        $"|lro" +
        $"|ol" +
        $"|opentype_features" +
        $"|outline_color" +
        $"|outline_size" +
        $"|p" +
        $"|pdf" +
        $"|pdi" +
        $"|rainbow" +
        $"|rb" +
        $"|right" +
        $"|rle" +
        $"|rli" +
        $"|rlm" +
        $"|rlo" +
        $"|s" +
        $"|shake" +
        $"|shy" +
        $"|table" +
        $"|tornado" +
        $"|u" +
        $"|ul" +
        $"|url" +
        $"|wave" +
        $"|wj" +
        $"|zwj" +
        $"|zwnj" +
        $@")[^\]]*\]";

    private static readonly List<Chat>                                                               s_instances                   = [];
    private static readonly Dictionary<BadgeType, string>                                            s_badgePaths                  = new()
    {
	    {
		    BadgeType.Bot,
		    "res://Resources/Textures/Icons/Icon_Joystick_Bot.png"
	    },
	    {
		    BadgeType.Moderator,
		    "res://Resources/Textures/Icons/Icon_Joystick_Moderator.png"
	    },
	    {
		    BadgeType.Streamer,
		    "res://Resources/Textures/Icons/Icon_Joystick_Streamer.png"
	    },
	    {
		    BadgeType.Subscriber,
		    "res://Resources/Textures/Icons/Icon_Joystick_Subscriber.png"
	    },
    };
    private static readonly Dictionary<BadgeType, ImageTexture>                                      s_dynamicTexturesBadge        = new();
    private static readonly Dictionary<(BadgeType, ServiceColorInterpolatorColorMode), ImageTexture> s_dynamicTexturesBadgeColor   = new();
    private static readonly Dictionary<BadgeType, byte[]>                                            s_basePixelData               = new();
    
    private readonly Queue<ChatMessage>                                                              m_availableChatMessages       = new();
    private readonly Queue<ChatMessage>                                                              m_displayedChatMessages       = new();
    private readonly Queue<ChatMessage>                                                              m_queuedChatMessages          = new();
    private readonly Queue<ChatMessageData>                                                          m_pendingChatMessageDatas     = new();
    
    private readonly object                                                                          m_availableChatMessagesLock   = new();
    private readonly object                                                                          m_displayedChatMessagesLock   = new();
    private readonly object                                                                          m_pendingChatMessageDatasLock = new();
    private readonly object                                                                          m_queuedChatMessagesLock      = new();
    
    private Control                                                                                  m_chatPivot                   = null;
    private int                                                                                      m_currentPixel                = 0;
    
    private void AddChatMessage(
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
	    var chatMessageData = new ChatMessageData(
		    username:            username,
		    hasCustomBadgeColor: hasCustomBadgeColor,
		    customBadgeColor:    customBadgeColor,
		    hasCustomNameColor:  hasCustomNameColor,
		    customNameColor:     customNameColor,
		    message:             message,
		    chatMessageEmotes:   chatMessageEmotes,
		    isModerator:         isModerator,
		    isStreamer:          isStreamer,
		    isSubscriber:        isSubscriber,
		    isBot:               isBot
	    );
	    lock (this.m_pendingChatMessageDatasLock)
	    {
		    this.m_pendingChatMessageDatas.Enqueue(
			    item: chatMessageData
		    );
	    }
    }

    private void AddDebugMessage(
	    string message
    )
    {
	    var chatMessageData = new ChatMessageData(
		    username:            "SmoothBot",
		    hasCustomBadgeColor: true,
		    customBadgeColor:    ServiceColorInterpolatorColorMode.White,
		    hasCustomNameColor:  true,
		    customNameColor:     ServiceColorInterpolatorColorMode.TeamFortress2KillStreak20,
		    message:             message,
		    chatMessageEmotes:   null,
		    isModerator:         true,
		    isStreamer:          false,
		    isSubscriber:        true,
		    isBot:               true
	    );
	    lock (this.m_pendingChatMessageDatasLock)
	    {
		    this.m_pendingChatMessageDatas.Enqueue(
			    item: chatMessageData
		    );
	    }
    }
    
    private static void ApplyUpdateBase(
	    BadgeType                         type,
	    Image                             image
    )
    {
	    Chat.s_dynamicTexturesBadge[key: type].Update(
		    image: image
	    );
    }
    
    private static void ApplyUpdateColor(
	    BadgeType                         type,
	    ServiceColorInterpolatorColorMode color,
	    Image                             image
	)
    {
	    Chat.s_dynamicTexturesBadgeColor[key: (type, color)].Update(
		    image: image
		);
    }
    
    private static bool IsChatMessageIllegal(
        string message
    )
    {
        var match = Regex.Match(
            input:   message,
            pattern: Chat.c_illegalBbCodePattern,
            options: RegexOptions.IgnoreCase
        );
        return match?.Success is true;
    }
    
    private void OnChatMessageDestroyed()
	{
		ChatMessage oldChatMessage;
        lock (this.m_displayedChatMessagesLock)
		{
            oldChatMessage = this.m_displayedChatMessages.Dequeue();
        }

        var oldestLabelHeight = oldChatMessage.GetLabelHeightInPixels() + Chat.c_pixelSpacing;
        this.m_currentPixel -= oldestLabelHeight;

        lock (this.m_displayedChatMessagesLock)
		{ 
            foreach (var displayedTwitchChatMessage in this.m_displayedChatMessages)
            {
                var position  = displayedTwitchChatMessage.Position;
                position -= new Vector2(
                    x: 0u,
                    y: oldestLabelHeight
                );

                displayedTwitchChatMessage.Position = position;
            }
        }

		this.RecycleChatMessage(
		    chatMessage: oldChatMessage
		);
    }

	private void OnChatMessageGenerated(
		ChatMessage chatMessage
	)
	{
		lock (this.m_queuedChatMessagesLock)
		{
            this.m_queuedChatMessages.Enqueue(
			    item: chatMessage
			);
        }
	}

	private void PopulateChatMessageCache()
	{
		for (var i = 0; i < Chat.c_chatMessageCacheSize; i++)
		{
            var chatMessage = new ChatMessage
            {
                Generated = this.OnChatMessageGenerated,
                Destroyed = this.OnChatMessageDestroyed
            };

			this.m_chatPivot.AddChild(
			    node: chatMessage
			);

			lock (this.m_availableChatMessagesLock)
			{
				this.m_availableChatMessages.Enqueue(
					item: chatMessage	
				);
			}
        }
	}

    private void ProcessQueuedChatMessage()
	{
        ChatMessage chatMessage;
        lock (this.m_queuedChatMessagesLock)
        {
            if (this.m_queuedChatMessages.Count > 0u)
            {
                chatMessage = this.m_queuedChatMessages.Dequeue();
            }
			else
			{
				return;
			}
        }

		chatMessage.Position = new Vector2(
			x: 0u,
			y: this.m_currentPixel
		);
		chatMessage.ShowLabel();

        lock (this.m_displayedChatMessagesLock)
        {
            this.m_displayedChatMessages.Enqueue(
                item: chatMessage
            );
        }

		var labelHeight     = chatMessage.GetLabelHeightInPixels();
		this.m_currentPixel = this.m_currentPixel + labelHeight + Chat.c_pixelSpacing;
		while (this.m_currentPixel > Chat.c_maxPixelCount)
		{
            ChatMessage oldestChatMessage;
            lock (this.m_displayedChatMessagesLock)
			{
				oldestChatMessage = this.m_displayedChatMessages.Dequeue();
            }

			var oldestLabelHeight  = oldestChatMessage.GetLabelHeightInPixels() + Chat.c_pixelSpacing;
			this.m_currentPixel   -= oldestLabelHeight;

			var offset = new Vector2(
				x: 0u,
				y: oldestLabelHeight
			);

            lock (this.m_displayedChatMessagesLock)
            {
                foreach (var displayedTwitchChatMessage in this.m_displayedChatMessages)
                {
                    displayedTwitchChatMessage.Position -= offset;
                }
            }

			this.RecycleChatMessage(
				chatMessage: oldestChatMessage	
			);
        }
	}

	private void ProcessQueuedChatMessageData()
	{
        ChatMessageData chatMessageData;
        lock (this.m_pendingChatMessageDatasLock)
		{
            if (this.m_pendingChatMessageDatas.Count > 0u)
			{
                chatMessageData = this.m_pendingChatMessageDatas.Dequeue();
            }
			else
			{
				return;
			}
        }

        ChatMessage chatMessage;
        lock (this.m_availableChatMessagesLock)
        {
            chatMessage = this.m_availableChatMessages.Dequeue();
        }

        chatMessage.Generate(
            username:            chatMessageData.Username,
            hasCustomBadgeColor: chatMessageData.HasCustomBadgeColor,
            customBadgeColor:    chatMessageData.CustomBadgeColor,
            hasCustomNameColor:  chatMessageData.HasCustomNameColor,
            customNameColor:     chatMessageData.CustomNameColor,
            message:             chatMessageData.Message,
            chatMessageEmotes:   chatMessageData.ChatMessageEmotes,
            isModerator:         chatMessageData.IsModerator,
            isStreamer:          chatMessageData.IsStreamer,
            isSubscriber:        chatMessageData.IsSubscriber,
            isBot:               chatMessageData.IsBot
        );
    }

    private void RecycleChatMessage(
        ChatMessage chatMessage
    )
    {
        lock (this.m_availableChatMessagesLock)
        {
            this.m_availableChatMessages.Enqueue(
                item: chatMessage
            );
        }
		chatMessage.Reset();
    }

    private void RetrieveResources()
	{
		Chat.s_instances.Add(
			item: this
		);
		this.m_chatPivot = this.GetNode<Control>(
			path: $"ChatPivot"
		);
	}

	private static void StartBadgeColorUpdater()
	{
		var badges = Enum.GetValues<BadgeType>();
		var colors = Enum.GetValues<ServiceColorInterpolatorColorMode>();
		foreach (var badge in badges)
		{
			var textureOriginalPath = Chat.s_badgePaths[key: badge];
			var textureOriginal     = GD.Load<Texture2D>(
				path: textureOriginalPath
			);
			var textureImage        = textureOriginal.GetImage();
			textureImage.Convert(
				format: Image.Format.Rgba8
			);
					
			Chat.s_basePixelData[key: badge]        = textureImage.GetData();
			var dynamicTexture                      = ImageTexture.CreateFromImage(
				image: textureImage
			);
			dynamicTexture.ResourcePath             = $"user://dynamic_{badge}_Default.res";
			Chat.s_dynamicTexturesBadge[key: badge] = dynamicTexture;

			foreach (var color in colors)
			{
				dynamicTexture                                        = ImageTexture.CreateFromImage(
					image: textureImage
				);
				dynamicTexture.ResourcePath                           = $"user://dynamic_{badge}_{color}.res";
				Chat.s_dynamicTexturesBadgeColor[key: (badge, color)] = dynamicTexture;
			}
		}
		
		Task.Run(
			function: async () =>
			{
				var serviceColorInterpolator = Services.Services.GetService<ServiceColorInterpolatorInverse>();
				while (true)
				{
					if (
						Chat.s_instances.Count is 0 || 
						GodotObject.IsInstanceValid(
							instance: Chat.s_instances[0]
						) is false
					) 
					{
						await Task.Delay(
							millisecondsDelay: 16
						);
						continue;
					}
					
					var color = serviceColorInterpolator.GetColorByCurrentMode(
						colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0
					);
					foreach (var badge in Chat.s_dynamicTexturesBadge.Keys)
					{
						var baseData = Chat.s_basePixelData[key: badge];
						var newData  = new byte[baseData.Length];

						for (var i = 0; i < baseData.Length; i += 4)
						{
							newData[i]     = (byte)(baseData[i]     * color.R);
							newData[i + 1] = (byte)(baseData[i + 1] * color.G);
							newData[i + 2] = (byte)(baseData[i + 2] * color.B);
							newData[i + 3] = baseData[i + 3];
						}

						var updatedImage = Image.CreateFromData(
							width:      32,
							height:     32,
							useMipmaps: false,
							format:     Image.Format.Rgba8, 
							data:       newData
						);
                
						var callable = new Callable(
							target: Chat.s_instances[0],
							method: MethodName.ApplyUpdateBase
						);
						callable.CallDeferred(
							args: [
								Variant.From(
									from: badge
								),
								updatedImage
							]
						);
					}
					
					foreach (var badgeColor in Chat.s_dynamicTexturesBadgeColor.Keys)
					{
						var baseData = Chat.s_basePixelData[key: badgeColor.Item1];
						var newData  = new byte[baseData.Length];

						color = serviceColorInterpolator.GetColorByMode(
							colorMode:      badgeColor.Item2,
							colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0
						);
						for (var i = 0; i < baseData.Length; i += 4)
						{
							newData[i]     = (byte)(baseData[i]     * color.R);
							newData[i + 1] = (byte)(baseData[i + 1] * color.G);
							newData[i + 2] = (byte)(baseData[i + 2] * color.B);
							newData[i + 3] = baseData[i + 3];
						}

						var updatedImage = Image.CreateFromData(
							width:      32,
							height:     32,
							useMipmaps: false,
							format:     Image.Format.Rgba8, 
							data:       newData
						);
                
						var callable = new Callable(
							target: Chat.s_instances[0],
							method: MethodName.ApplyUpdateColor
						);
						callable.CallDeferred(
							args: [
								Variant.From(
									from: badgeColor.Item1
								),
								Variant.From(
									from: badgeColor.Item2
								),
								updatedImage
							]
						);
					}
					
					await Task.Delay(
						millisecondsDelay: 16
					);
				}
			}
		);
	}
	
	internal void UpdateChatMessageBadgeColor(
		string                            username,
		bool                              hasCustomColor,
		ServiceColorInterpolatorColorMode customColor
	)
	{
		lock (this.m_displayedChatMessagesLock)
		{
			foreach (
				var displayedChatMessage in from displayedChatMessage in this.m_displayedChatMessages 
					let hasUsername = displayedChatMessage.HasUsername(
				         username: username
			        ) where hasUsername is true select displayedChatMessage
			)
			{
				displayedChatMessage.UpdateBadgeColor(
					hasCustomColor: hasCustomColor,
					customColor:    customColor
				);
			}
		}
		
		lock (this.m_pendingChatMessageDatasLock)
		{
			foreach (
				var pendingChatMessageData in this.m_pendingChatMessageDatas.Where(
					predicate: pendingChatMessageData => pendingChatMessageData.Username == username
				)
			)
			{
				pendingChatMessageData.HasCustomBadgeColor = hasCustomColor;
				pendingChatMessageData.CustomBadgeColor    = customColor;
			}
		}
	}
	
	internal void UpdateChatMessageNameColor(
		string                            username,
		bool                              hasCustomColor,
		ServiceColorInterpolatorColorMode customColor
	)
	{
		lock (this.m_displayedChatMessagesLock)
		{
			foreach (
				var displayedChatMessage in from displayedChatMessage in this.m_displayedChatMessages 
				let hasUsername = displayedChatMessage.HasUsername(
					username: username
				) where hasUsername is true select displayedChatMessage
			)
			{
				displayedChatMessage.UpdateNameColor(
					hasCustomColor: hasCustomColor,
					customColor:    customColor
				);
			}
		}

		lock (this.m_pendingChatMessageDatasLock)
		{
			foreach (
				var pendingChatMessageData in this.m_pendingChatMessageDatas.Where(
					predicate: pendingChatMessageData => pendingChatMessageData.Username == username
				)
			)
			{
				pendingChatMessageData.HasCustomNameColor = hasCustomColor;
				pendingChatMessageData.CustomNameColor    = customColor;
			}
		}
	}
}