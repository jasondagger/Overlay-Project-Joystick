
using Godot;
using System;
using System.Collections.Generic;
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
	             this.HandlePastelInterpolationTextAnimation();
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
         
         this.GenerateRichTextLabel();
     }

 	internal int GetLabelHeightInPixels()
	{
		return _ = this.m_richTextLabel.GetContentHeight() + 5;
	}

	internal void Reset()
    {
        _ = this.m_richTextLabel.Text = _ = string.Empty;
        _ = this.m_generateState      = _ = GenerateStateType.Inactive;
        _ = this.m_visibleState       = _ = VisibleStateType.Visible;
        _ = this.m_fadeElapsed        = _ = 0f;
        _ = this.m_isStreamer         = _ = false;
        _ = this.m_isSubscriber       = _ = false;
        _ = this.m_text               = _ = string.Empty;
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

    private const string                                        c_labelPastelInterpolatorColor = $"00000000";
    private const string                                        c_labelFontSize                = $"[font_size=28]";
	private const string                                        c_labelNameFont                = $"[font=res://Resources/Fonts/Roboto-Black.ttf]";
	private const string                                        c_labelMessageFont             = $"[font=res://Resources/Fonts/Roboto-Bold.ttf]";
	private const string                                        c_labelMessageColor            = $"[color=#F2F2F2FF]";
    private const uint                                          c_labelWidth                   = 1100u;
    
    private static readonly Dictionary<VisibleStateType, float> c_fadeDelays                   = new()
    {
	    { _ = VisibleStateType.Visible, _ = 32f },
	    { _ = VisibleStateType.Fade,    _ = 2f },
    };

    private ServiceGodotHttp                                    m_serviceGodotHttp             = null;
    private ServicePastelInterpolator                           m_servicePastelInterpolator    = null;
	private RichTextLabel                                       m_richTextLabel                = new();
	private GenerateStateType                                   m_generateState                = GenerateStateType.Inactive;
	private VisibleStateType                                    m_visibleState                 = VisibleStateType.Visible;
	private float                                               m_fadeElapsed                  = 0f;
	private bool                                                m_isStreamer                   = false;
    private bool                                                m_isSubscriber                 = false;
	private string                                              m_text                         = string.Empty;

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

	private void HandlePastelInterpolationTextAnimation()
	{
		if (
			_ = this.m_isSubscriber is false && 
			this.m_isStreamer is false
		)
		{
			return;
		}
		
		var color = _ = this.m_servicePastelInterpolator.GetColorAsHex(
			rainbowColorIndexType: _ = ServicePastelInterpolator.RainbowColorIndexType.Color0    
		);
		_ = this.m_richTextLabel.Text = _ = this.m_text.Replace(
			oldValue: _ = ChatMessage.c_labelPastelInterpolatorColor,
			newValue: _ = color
		);
	}
    
	private void HandleTextFade(
		float delta
	)
	{
		_ = this.m_fadeElapsed += _ = delta;
		switch (_ = this.m_visibleState)
		{
			case VisibleStateType.Visible:
				if (_ = this.m_fadeElapsed >= ChatMessage.c_fadeDelays[key: _ = VisibleStateType.Visible])
				{
					_ = this.m_visibleState = VisibleStateType.Fade;
					_ = this.m_fadeElapsed = 0f;
				}
				break;
			case VisibleStateType.Fade:
				// todo: transparency
				var height = _ = this.m_richTextLabel.GetContentHeight();
				if (_ = this.m_fadeElapsed >= ChatMessage.c_fadeDelays[key: _ = VisibleStateType.Fade])
				{
					this.Destroyed?.Invoke();
					_ = this.m_generateState = _ = GenerateStateType.Inactive;
				}
				break;

			default:
				break;
		}
	}
    
    private void RetrieveResources()
    {
	    _ = this.m_servicePastelInterpolator = _ = Services.Services.GetService<ServicePastelInterpolator>();
	    
        this.AddChild(
			node: _ = this.m_richTextLabel
		);
    }
}