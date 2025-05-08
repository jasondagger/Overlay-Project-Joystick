
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;
using Overlay.Core.Services.PastelInterpolators;
using Overlay.Core.Tools;

namespace Overlay.Core.Contents.StreamEvents;

internal static class StreamEventsTipMenuLights
{
    internal static void HandleTipMenuItem(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var tipMenuItem   = _ = messageMetadata.TipMenuItem;
        switch (_ = tipMenuItem)
        {
            case "Set Light Color Blue":
                GoveeLightController.Instance.SetLightColor(
                    colorType: _ = ServicePastelInterpolator.ColorType.Blue
                );
                break;

            case "Set Light Color Cyan":
                GoveeLightController.Instance.SetLightColor(
                    colorType: _ = ServicePastelInterpolator.ColorType.Cyan
                );
                break;

            case "Set Light Color Green":
                GoveeLightController.Instance.SetLightColor(
                    colorType: _ = ServicePastelInterpolator.ColorType.Green
                );
                break;

            case "Set Light Color Magenta":
                GoveeLightController.Instance.SetLightColor(
                    colorType: _ = ServicePastelInterpolator.ColorType.Magenta
                );
                break;

            case "Set Light Color Red":
                GoveeLightController.Instance.SetLightColor(
                    colorType: _ = ServicePastelInterpolator.ColorType.Red
                );
                break;
            
            case "Set Light Color White":
                GoveeLightController.Instance.SetLightColor(
                    colorType: _ = ServicePastelInterpolator.ColorType.White
                );
                break;

            case "Set Light Color Yellow":
                GoveeLightController.Instance.SetLightColor(
                    colorType: _ = ServicePastelInterpolator.ColorType.Yellow
                );
                break;
            
            case "Set Light Scene Heatwave":
                GoveeLightController.Instance.SetLightScene(
                    sceneName: _ = "Heatwave"
                );
                break;
            
            case "Set Light Scene Icy":
                GoveeLightController.Instance.SetLightScene(
                    sceneName: _ = "Icy"
                );
                break;
            
            case "Set Light Scene Rainbow":
                GoveeLightController.Instance.SetLightScene(
                    sceneName: _ = "Rainbow"
                );
                break;
            
            case "Turn Off Lights":
                GoveeLightController.Instance.TurnOffLights();
                break;
            
            default:
                ConsoleLogger.LogMessage(
                    message: _ = 
                        $"EXCEPTION: " +
                        $"{_ = nameof(StreamEventsTipMenuLights)}." +
                        $"{_ = nameof(StreamEventsTipMenuLights.HandleTipMenuItem)}() - " +
                        $"Missing tip menu item \"{tipMenuItem}\"."
                );
                return;
        }
        
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.Tip
        );
    }
}