
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
        var tipMenuItem = messageMetadata.TipMenuItem;
        switch (tipMenuItem)
        {
            case "Set Light Color Blue":
                GoveeLightController.Instance.SetLightColor(
                    colorType: ServicePastelInterpolator.ColorType.Blue
                );
                break;

            case "Set Light Color Cyan":
                GoveeLightController.Instance.SetLightColor(
                    colorType: ServicePastelInterpolator.ColorType.Cyan
                );
                break;

            case "Set Light Color Green":
                GoveeLightController.Instance.SetLightColor(
                    colorType: ServicePastelInterpolator.ColorType.Green
                );
                break;

            case "Set Light Color Magenta":
                GoveeLightController.Instance.SetLightColor(
                    colorType: ServicePastelInterpolator.ColorType.Magenta
                );
                break;

            case "Set Light Color Red":
                GoveeLightController.Instance.SetLightColor(
                    colorType: ServicePastelInterpolator.ColorType.Red
                );
                break;
            
            case "Set Light Color White":
                GoveeLightController.Instance.SetLightColor(
                    colorType: ServicePastelInterpolator.ColorType.White
                );
                break;

            case "Set Light Color Yellow":
                GoveeLightController.Instance.SetLightColor(
                    colorType: ServicePastelInterpolator.ColorType.Yellow
                );
                break;
            
            case "Set Light Scene Heatwave":
                GoveeLightController.Instance.SetLightScene(
                    sceneName: "Heatwave"
                );
                break;
            
            case "Set Light Scene Icy":
                GoveeLightController.Instance.SetLightScene(
                    sceneName: "Icy"
                );
                break;
            
            case "Set Light Scene Rainbow":
                GoveeLightController.Instance.SetLightScene(
                    sceneName: "Rainbow"
                );
                break;
            
            case "Set Light Scene Toxic":
                GoveeLightController.Instance.SetLightScene(
                    sceneName: "Toxic"
                );
                break;
            
            case "Turn Off Lights":
                GoveeLightController.Instance.TurnOffLights();
                break;
            
            default:
                ConsoleLogger.LogMessage(
                    message: 
                        $"EXCEPTION: " +
                        $"{nameof(StreamEventsTipMenuLights)}." +
                        $"{nameof(StreamEventsTipMenuLights.HandleTipMenuItem)}() - " +
                        $"Missing tip menu item \"{tipMenuItem}\"."
                );
                return;
        }
        
        var serviceGodots     = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
    }
}