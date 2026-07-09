
using Overlay.Core.Services.ColorInterpolators;
using Overlay.Core.Services.Govee;

namespace Overlay.Core.Contents.Mappings;

internal static class MappingLightPatterns
{
    internal struct LightCommandResult 
    {
        public IServiceColorInterpolatorDefinition.ColorType? Color;
        public string                                         Scene;
        public bool IsValid => 
            this.Color.HasValue || 
            !string.IsNullOrEmpty(
                value: this.Scene
            );
    }
    
    internal static LightCommandResult GetLightPatternAction(
        string lightPattern
    )
    {
        return lightPattern switch 
        { 
            "blue"                   => new LightCommandResult { Color = IServiceColorInterpolatorDefinition.ColorType.Blue    },
            "blue raspberry"         => new LightCommandResult { Scene = ServiceGoveeSceneNames.BlueRaspberry                  },
            "creamsicle banana"      => new LightCommandResult { Scene = ServiceGoveeSceneNames.CreamsicleBanana               },
            "creamsicle blueberry"   => new LightCommandResult { Scene = ServiceGoveeSceneNames.CreamsicleBlueberry            },
            "creamsicle dragonfruit" => new LightCommandResult { Scene = ServiceGoveeSceneNames.CreamsicleDragonfruit          },
            "creamsicle lime"        => new LightCommandResult { Scene = ServiceGoveeSceneNames.CreamsicleLime                 },
            "creamsicle orange"      => new LightCommandResult { Scene = ServiceGoveeSceneNames.CreamsicleOrange               },
            "creamsicle strawberry"  => new LightCommandResult { Scene = ServiceGoveeSceneNames.CreamsicleStrawberry           },
            "cyan"                   => new LightCommandResult { Color = IServiceColorInterpolatorDefinition.ColorType.Cyan    },
            "cyberpunk"              => new LightCommandResult { Scene = ServiceGoveeSceneNames.Cyberpunk                      },
            "forest sunset"          => new LightCommandResult { Scene = ServiceGoveeSceneNames.ForestSunset                   },
            "green"                  => new LightCommandResult { Color = IServiceColorInterpolatorDefinition.ColorType.Green   },
            "heatwave"               => new LightCommandResult { Scene = ServiceGoveeSceneNames.Heatwave                       },
            "icy"                    => new LightCommandResult { Scene = ServiceGoveeSceneNames.Icy                            },
            "magenta"                => new LightCommandResult { Color = IServiceColorInterpolatorDefinition.ColorType.Magenta },
            "orange"                 => new LightCommandResult { Color = IServiceColorInterpolatorDefinition.ColorType.Orange  },
            "orange purple"          => new LightCommandResult { Scene = ServiceGoveeSceneNames.OrangePurple                   },
            "purple"                 => new LightCommandResult { Color = IServiceColorInterpolatorDefinition.ColorType.Purple  },
            "rainbow"                => new LightCommandResult { Scene = ServiceGoveeSceneNames.Rainbow                        },
            "red"                    => new LightCommandResult { Color = IServiceColorInterpolatorDefinition.ColorType.Red     },
            "red white blue"         => new LightCommandResult { Scene = ServiceGoveeSceneNames.RedWhiteBlue                   },
            "toxic"                  => new LightCommandResult { Scene = ServiceGoveeSceneNames.Toxic                          },
            "vaporwave"              => new LightCommandResult { Scene = ServiceGoveeSceneNames.Vaporwave                      },
            "watermelon"             => new LightCommandResult { Scene = ServiceGoveeSceneNames.Watermelon                     },
            "white"                  => new LightCommandResult { Color = IServiceColorInterpolatorDefinition.ColorType.White   },
            "yellow"                 => new LightCommandResult { Color = IServiceColorInterpolatorDefinition.ColorType.Yellow  },
            _                        => default
        };
    }
}