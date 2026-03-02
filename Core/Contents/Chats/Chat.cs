
using Godot;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Overlay.Core.Contents.Chats;

internal sealed partial class Chat() :
    Control()
{
	public override void _EnterTree()
	{
		base._EnterTree();
		
		this.RetrieveResources();
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
        string             username,
        string             usernameColor,
        string             message,
        ChatMessageEmote[] chatMessageEmotes,
        bool               isModerator,
        bool               isStreamer,
        bool               isSubscriber
    )
    {
	    foreach (var instance in Chat.s_instances)
	    {
		    instance.AddChatMessage(
			    username:          username,
			    usernameColor:     usernameColor,
			    message:           message,
			    chatMessageEmotes: chatMessageEmotes,
			    isModerator:       isModerator,
			    isStreamer:        isStreamer,
			    isSubscriber:      isSubscriber
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
    
    private readonly struct ChatMessageData
    {
	    internal readonly string             Username          = string.Empty;
	    internal readonly string             UsernameColor     = string.Empty;
	    internal readonly string             Message           = string.Empty;
	    internal readonly ChatMessageEmote[] ChatMessageEmotes = null;
	    internal readonly bool               IsModerator       = false;
	    internal readonly bool               IsStreamer        = false;
	    internal readonly bool               IsSubscriber      = false;

	    public ChatMessageData(
		    string             username,
		    string             usernameColor,
		    string             message,
		    ChatMessageEmote[] chatMessageEmotes,
		    bool               isModerator,
		    bool               isStreamer,
			bool               isSubscriber
	    )
	    {
		    this.Username          = username;
		    this.UsernameColor     = usernameColor;
		    this.Message           = message;
		    this.ChatMessageEmotes = chatMessageEmotes;
		    this.IsModerator       = isModerator;
		    this.IsStreamer        = isStreamer;
		    this.IsSubscriber      = isSubscriber;
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

    private static readonly List<Chat>      s_instances                   = [];

    private readonly Queue<ChatMessage>     m_availableChatMessages       = new();
    private readonly Queue<ChatMessage>     m_displayedChatMessages       = new();
    private readonly Queue<ChatMessage>     m_queuedChatMessages          = new();
    private readonly Queue<ChatMessageData> m_pendingChatMessageDatas     = new();

    private readonly object                 m_availableChatMessagesLock   = new();
    private readonly object                 m_displayedChatMessagesLock   = new();
    private readonly object                 m_pendingChatMessageDatasLock = new();
    private readonly object                 m_queuedChatMessagesLock      = new();

    private Control                         m_chatPivot                   = null;
    private int                             m_currentPixel                = 0;
    
    private void AddChatMessage(
	    string             username,
	    string             usernameColor,
	    string             message,
	    ChatMessageEmote[] chatMessageEmotes,
	    bool               isModerator,
	    bool               isStreamer,
	    bool               isSubscriber
    )
    {
	    var chatMessageData = new ChatMessageData(
		    username:          username,
		    usernameColor:     usernameColor,
		    message:           message,
		    chatMessageEmotes: chatMessageEmotes,
		    isModerator:       isModerator,
		    isStreamer:        isStreamer,
		    isSubscriber:      isSubscriber
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
		    username:          "SmoothBot",
		    usernameColor:     string.Empty,
		    message:           message,
		    chatMessageEmotes: null,
		    isModerator:       true,
		    isStreamer:        false,
		    isSubscriber:      true
	    );
	    lock (this.m_pendingChatMessageDatasLock)
	    {
		    this.m_pendingChatMessageDatas.Enqueue(
			    item: chatMessageData
		    );
	    }
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

			this.m_availableChatMessages.Enqueue(
				item: chatMessage	
			);
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

		var labelHeight         = chatMessage.GetLabelHeightInPixels();
		this.m_currentPixel = this.m_currentPixel + labelHeight + Chat.c_pixelSpacing;
		while (this.m_currentPixel > Chat.c_maxPixelCount)
		{
            ChatMessage oldestChatMessage;
            lock (this.m_displayedChatMessagesLock)
			{
				oldestChatMessage = this.m_displayedChatMessages.Dequeue();
            }

			var oldestLabelHeight    = oldestChatMessage.GetLabelHeightInPixels() + Chat.c_pixelSpacing;
			this.m_currentPixel -= oldestLabelHeight;

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
            username:          chatMessageData.Username,
            usernameColor:     chatMessageData.UsernameColor,
            message:           chatMessageData.Message,
            chatMessageEmotes: chatMessageData.ChatMessageEmotes,
            isModerator:       chatMessageData.IsModerator,
            isStreamer:        chatMessageData.IsStreamer,
            isSubscriber:      chatMessageData.IsSubscriber
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
}