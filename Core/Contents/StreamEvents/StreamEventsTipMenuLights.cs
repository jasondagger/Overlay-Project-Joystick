
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.PastelInterpolators;
using Overlay.Core.Tools;

namespace Overlay.Core.Contents.StreamEvents;

internal static class StreamEventsTipMenuLights
{
    internal static void HandleTipMenuItem(
        string tipMenuItem
    )
    {
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

            case "Set Light Color Lime":
                GoveeLightController.Instance.SetLightColor(
                    colorType: _ = ServicePastelInterpolator.ColorType.Lime
                );
                break;

            case "Set Light Color Magenta":
                GoveeLightController.Instance.SetLightColor(
                    colorType: _ = ServicePastelInterpolator.ColorType.Magenta
                );
                break;

            case "Set Light Color Orange":
                GoveeLightController.Instance.SetLightColor(
                    colorType: _ = ServicePastelInterpolator.ColorType.Orange
                );
                break;

            case "Set Light Color Pink":
                GoveeLightController.Instance.SetLightColor(
                    colorType: _ = ServicePastelInterpolator.ColorType.Pink
                );
                break;

            case "Set Light Color Purple":
                GoveeLightController.Instance.SetLightColor(
                    colorType: _ = ServicePastelInterpolator.ColorType.Purple
                );
                break;

            case "Set Light Color Red":
                GoveeLightController.Instance.SetLightColor(
                    colorType: _ = ServicePastelInterpolator.ColorType.Red
                );
                break;

            case "Set Light Color Teal":
                GoveeLightController.Instance.SetLightColor(
                    colorType: _ = ServicePastelInterpolator.ColorType.Teal
                );
                break;

            case "Set Light Color Turquoise":
                GoveeLightController.Instance.SetLightColor(
                    colorType: _ = ServicePastelInterpolator.ColorType.Turquoise
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