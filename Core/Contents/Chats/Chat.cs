
using Godot;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Overlay.Core.Contents.Chats;

internal sealed partial class Chat() :
    Control()
{
	public override void _Process(
		double delta
	)
	{
		base._Process(
			delta: _ = delta
		);

		this.ProcessQueuedChatMessage();
		this.ProcessQueuedChatMessageData();
	}
	
    public override void _Ready()
    {
	    base._Ready();
	    
	    this.RetrieveResources();
	    this.PopulateChatMessageCache();
    }

	internal static Chat Instance { get; private set; }
    
    internal void AddChatMessage(
        string username,
        string usernameColor,
        string message,
        bool   isModerator,
        bool   isStreamer,
        bool   isSubscriber
    )
    {
	    var chatMessageData = _ = new ChatMessageData(
		    username:      _ = username,
		    usernameColor: _ = usernameColor,
		    message:       _ = message,
		    isModerator:    _ = isModerator,
		    isStreamer:    _ = isStreamer,
			isSubscriber:  _ = isSubscriber
	    );
	    lock (_ = this.m_pendingChatMessageDatasLock)
	    {
		    this.m_pendingChatMessageDatas.Enqueue(
			    item: _ = chatMessageData
		    );
	    }
    }
    
    private struct ChatMessageData
    {
	    public readonly string Username      = _ = string.Empty;
	    public readonly string UsernameColor = _ = string.Empty;
	    public readonly string Message       = _ = string.Empty;
	    public readonly bool   IsModerator   = _ = false;
	    public readonly bool   IsStreamer    = _ = false;
	    public readonly bool   IsSubscriber  = _ = false;

	    public ChatMessageData(
		    string username,
		    string usernameColor,
		    string message,
		    bool   isModerator,
		    bool   isStreamer,
			bool   isSubscriber
	    )
	    {
		    _ = this.Username      = _ = username;
		    _ = this.UsernameColor = _ = usernameColor;
		    _ = this.Message       = _ = message;
		    _ = this.IsModerator   = _ = isModerator;
		    _ = this.IsStreamer    = _ = isStreamer;
		    _ = this.IsSubscriber  = _ = isSubscriber;
	    }
    };

    private const int    c_chatMessageCacheSize = 11;
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

    private readonly Queue<ChatMessage>     m_availableChatMessages           = new();
    private readonly Queue<ChatMessage>     m_displayedTwitchChatMessages     = new();
    private readonly Queue<ChatMessage>     m_queuedTwitchChatMessages        = new();
    private readonly Queue<ChatMessageData> m_pendingChatMessageDatas         = new();

    private readonly object                 m_availableChatMessagesLock       = new();
    private readonly object                 m_displayedTwitchChatMessagesLock = new();
    private readonly object                 m_pendingChatMessageDatasLock     = new();
    private readonly object                 m_queuedTwitchChatMessagesLock    = new();

    private Control                         m_chatPivot                       = null;
    private int                             m_currentPixel                    = 0;
    
    private static bool IsChatMessageIllegal(
        string message
    )
    {
        var match = _ = Regex.Match(
            input:   _ = message,
            pattern: _ = Chat.c_illegalBbCodePattern,
            options: _ = RegexOptions.IgnoreCase
        );
        return _ = match?.Success is true;
    }
    
    private void OnChatMessageDestroyed()
	{
		ChatMessage oldChatMessage;
        lock (_ = this.m_displayedTwitchChatMessagesLock)
		{
            _ = oldChatMessage = _ = this.m_displayedTwitchChatMessages.Dequeue();
        }

        var oldestLabelHeight = _ = oldChatMessage.GetLabelHeightInPixels() + c_pixelSpacing;
        _ = this.m_currentPixel -= _ = oldestLabelHeight;

        lock (_ = this.m_displayedTwitchChatMessagesLock)
		{ 
            foreach (var displayedTwitchChatMessage in _ = this.m_displayedTwitchChatMessages)
            {
                var position = _ = displayedTwitchChatMessage.Position;
                position -= _ = new Vector2(
                    x: _ = 0u,
                    y: _ = oldestLabelHeight
                );

                displayedTwitchChatMessage.Position = _ = position;
            }
        }

		this.RecycleChatMessage(
		    chatMessage: _ = oldChatMessage
		);
    }

	private void OnChatMessageGenerated(
		ChatMessage chatMessage
	)
	{
		lock (m_queuedTwitchChatMessagesLock)
		{
            m_queuedTwitchChatMessages.Enqueue(
			    item: chatMessage
			);
        }
	}

	private void PopulateChatMessageCache()
	{
		for (var i = 0; i < Chat.c_chatMessageCacheSize; i++)
		{
            var chatMessage = _ = new ChatMessage
            {
                Generated = this.OnChatMessageGenerated,
                Destroyed = this.OnChatMessageDestroyed
            };

			this.m_chatPivot.AddChild(
			    node: _ = chatMessage
			);

			this.m_availableChatMessages.Enqueue(
				item: _ = chatMessage	
			);
        }
	}

    private void ProcessQueuedChatMessage()
	{
        ChatMessage chatMessage;
        lock (_ = this.m_queuedTwitchChatMessagesLock)
        {
            if (_ = this.m_queuedTwitchChatMessages.Count > 0u)
            {
                _ = chatMessage = _ = this.m_queuedTwitchChatMessages.Dequeue();
            }
			else
			{
				return;
			}
        }

		chatMessage.Position = new Vector2(
			x: _ = 0u,
			y: _ = this.m_currentPixel
		);
		chatMessage.ShowLabel();

        lock (_ = this.m_displayedTwitchChatMessagesLock)
        {
            this.m_displayedTwitchChatMessages.Enqueue(
                item: _ = chatMessage
            );
        }

		var labelHeight = _ = chatMessage.GetLabelHeightInPixels();
		_ = this.m_currentPixel = _ = this.m_currentPixel + labelHeight + Chat.c_pixelSpacing;
		while (_ = this.m_currentPixel > Chat.c_maxPixelCount)
		{
            ChatMessage oldestChatMessage;
            lock (_ = this.m_displayedTwitchChatMessagesLock)
			{
				_ = oldestChatMessage = _ = this.m_displayedTwitchChatMessages.Dequeue();
            }

			var oldestLabelHeight = _ = oldestChatMessage.GetLabelHeightInPixels() + Chat.c_pixelSpacing;
			_ = this.m_currentPixel -= _ = oldestLabelHeight;

			var offset = _ = new Vector2(
				x: _ = 0u,
				y: _ = oldestLabelHeight
			);

            lock (_ = this.m_displayedTwitchChatMessagesLock)
            {
                foreach (var displayedTwitchChatMessage in _ = this.m_displayedTwitchChatMessages)
                {
                    _ = displayedTwitchChatMessage.Position -= _ = offset;
                }
            }

			this.RecycleChatMessage(
				chatMessage: _ = oldestChatMessage	
			);
        }
	}

	private void ProcessQueuedChatMessageData()
	{
        ChatMessageData messageData;
        lock (_ = this.m_pendingChatMessageDatasLock)
		{
            if (_ = this.m_pendingChatMessageDatas.Count > 0u)
			{
                messageData = _ = this.m_pendingChatMessageDatas.Dequeue();
            }
			else
			{
				return;
			}
        }

        ChatMessage chatMessage;
        lock (_ = this.m_availableChatMessagesLock)
        {
            chatMessage = _ = this.m_availableChatMessages.Dequeue();
        }

        chatMessage.Generate(
            username:      _ = messageData.Username,
            usernameColor: _ = messageData.UsernameColor,
            message:       _ = messageData.Message,
            isModerator:   _ = messageData.IsModerator,
            isStreamer:    _ = messageData.IsStreamer,
            isSubscriber:  _ = messageData.IsSubscriber
        );
    }

    private void RecycleChatMessage(
        ChatMessage chatMessage
    )
    {
        lock (_ = this.m_availableChatMessagesLock)
        {
            this.m_availableChatMessages.Enqueue(
                item: chatMessage
            );
        }
		chatMessage.Reset();
    }

    private void RetrieveResources()
	{
		_ = Chat.Instance = _ = this;
		_ = this.m_chatPivot = _ = this.GetNode<Control>(
			path: _ = $"ChatPivot"
		);
	}
}