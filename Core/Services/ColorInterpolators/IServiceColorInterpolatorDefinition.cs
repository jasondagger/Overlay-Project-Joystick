
using Godot;
using System.Collections.Generic;

namespace Overlay.Core.Services.ColorInterpolators;

public interface IServiceColorInterpolatorDefinition
{
    public enum ColorType :
        uint
    {
        Red = 0u,
        Yellow,
        Green,
        Cyan,
        Blue,
        Magenta,
        Orange,
        Purple,
        White,
        
        BananaShake0,
        BananaShake1,
        BananaShake2,
        BananaShake3,
        BananaShake4,
        BananaShake5,
        
        Blue0,
        Blue1,
        Blue2,
        Blue3,
        Blue4,
        Blue5,
        
        BlueRaspberry0,
        BlueRaspberry1,
        BlueRaspberry2,
        BlueRaspberry3,
        BlueRaspberry4,
        BlueRaspberry5,
        
        Border0,
        Border1,
        Border2,
        Border3,
        Border4,
        Border5,
        
        CreamsicleBanana0,
        CreamsicleBanana1,
        CreamsicleBanana2,
        CreamsicleBanana3,
        CreamsicleBanana4,
        CreamsicleBanana5,
        
        CreamsicleBlueberry0,
        CreamsicleBlueberry1,
        CreamsicleBlueberry2,
        CreamsicleBlueberry3,
        CreamsicleBlueberry4,
        CreamsicleBlueberry5,
        
        CreamsicleDragonfruit0,
        CreamsicleDragonfruit1,
        CreamsicleDragonfruit2,
        CreamsicleDragonfruit3,
        CreamsicleDragonfruit4,
        CreamsicleDragonfruit5,
        
        CreamsicleLime0,
        CreamsicleLime1,
        CreamsicleLime2,
        CreamsicleLime3,
        CreamsicleLime4,
        CreamsicleLime5,
        
        CreamsicleOrange0,
        CreamsicleOrange1,
        CreamsicleOrange2,
        CreamsicleOrange3,
        CreamsicleOrange4,
        CreamsicleOrange5,
        
        CreamsicleStrawberry0,
        CreamsicleStrawberry1,
        CreamsicleStrawberry2,
        CreamsicleStrawberry3,
        CreamsicleStrawberry4,
        CreamsicleStrawberry5,
        
        Cyan0,
        Cyan1,
        Cyan2,
        Cyan3,
        Cyan4,
        Cyan5,
        
        Cyberpunk0,
        Cyberpunk1,
        Cyberpunk2,
        Cyberpunk3,
        Cyberpunk4,
        Cyberpunk5,
        
        Dinner0,
        Dinner1,
        Dinner2,
        Dinner3,
        Dinner4,
        Dinner5,
        
        ForestSunset0,
        ForestSunset1,
        ForestSunset2,
        ForestSunset3,
        ForestSunset4,
        ForestSunset5,
        
        Green0,
        Green1,
        Green2,
        Green3,
        Green4,
        Green5,
        
        Heatwave0,
        Heatwave1,
        Heatwave2,
        Heatwave3,
        Heatwave4,
        Heatwave5,
        
        Icy0,
        Icy1,
        Icy2,
        Icy3,
        Icy4,
        Icy5,
        
        Magenta0,
        Magenta1,
        Magenta2,
        Magenta3,
        Magenta4,
        Magenta5,
        
        Orange0,
        Orange1,
        Orange2,
        Orange3,
        Orange4,
        Orange5,
        
        OrangePurple0,
        OrangePurple1,
        OrangePurple2,
        OrangePurple3,
        OrangePurple4,
        OrangePurple5,
        
        PoweradeSlushie0,
        PoweradeSlushie1,
        PoweradeSlushie2,
        PoweradeSlushie3,
        PoweradeSlushie4,
        PoweradeSlushie5,
        
        Purple0,
        Purple1,
        Purple2,
        Purple3,
        Purple4,
        Purple5,
        
        Red0,
        Red1,
        Red2,
        Red3,
        Red4,
        Red5,
        
        RedWhiteBlue0,
        RedWhiteBlue1,
        RedWhiteBlue2,
        RedWhiteBlue3,
        RedWhiteBlue4,
        RedWhiteBlue5,
        
        ShowinSomeLove0,
        ShowinSomeLove1,
        ShowinSomeLove2,
        ShowinSomeLove3,
        ShowinSomeLove4,
        ShowinSomeLove5,
        
        TeamFortress2KillStreak5_0,
        TeamFortress2KillStreak5_1,
        TeamFortress2KillStreak5_2,
        TeamFortress2KillStreak5_3,
        TeamFortress2KillStreak5_4,
        TeamFortress2KillStreak5_5,
        
        TeamFortress2KillStreak10_0,
        TeamFortress2KillStreak10_1,
        TeamFortress2KillStreak10_2,
        TeamFortress2KillStreak10_3,
        TeamFortress2KillStreak10_4,
        TeamFortress2KillStreak10_5,
        
        TeamFortress2KillStreak15_0,
        TeamFortress2KillStreak15_1,
        TeamFortress2KillStreak15_2,
        TeamFortress2KillStreak15_3,
        TeamFortress2KillStreak15_4,
        TeamFortress2KillStreak15_5,
        
        TeamFortress2KillStreak20_0,
        TeamFortress2KillStreak20_1,
        TeamFortress2KillStreak20_2,
        TeamFortress2KillStreak20_3,
        TeamFortress2KillStreak20_4,
        TeamFortress2KillStreak20_5,
        
        TokeUp0,
        TokeUp1,
        TokeUp2,
        TokeUp3,
        TokeUp4,
        TokeUp5,
        
        Toxic0,
        Toxic1,
        Toxic2,
        Toxic3,
        Toxic4,
        Toxic5,
        
        Transition0,
        Transition1,
        Transition2,
        Transition3,
        Transition4,
        Transition5,
        
        Vaporwave0,
        Vaporwave1,
        Vaporwave2,
        Vaporwave3,
        Vaporwave4,
        Vaporwave5,
        
        Watermelon0,
        Watermelon1,
        Watermelon2,
        Watermelon3,
        Watermelon4,
        Watermelon5,
        
        White0,
        White1,
        White2,
        White3,
        White4,
        White5,
        
        Yellow0,
        Yellow1,
        Yellow2,
        Yellow3,
        Yellow4,
        Yellow5,
        
        Off,
    }

    public enum ColorIndexType :
        uint
    {
        Color0 = 0u,
        Color1,
        Color2,
        Color3,
        Color4,
        Color5,
    }
    
    static abstract Dictionary<ColorType, Color> ColorCodes { get; }
}