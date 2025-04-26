
using System.Text;
using Godot;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Https;
using Overlay.Core.Services.Govee.Payloads;
using Overlay.Core.Tools;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Govee;

internal sealed class ServiceGovee() :
    IService
{
    Task IService.Setup()
    {
        this.RetrieveResources();
        return _ = Task.CompletedTask;
    }

    Task IService.Start()
    {
        return _ = Task.CompletedTask;
    }

    Task IService.Stop()
    {
        return _ = Task.CompletedTask;
    }

    internal void SetLightColor(
        Color color
    )
    {
        var rgbValue = _ = ServiceGovee.ConvertColorToInt(
            color: _ = color
        );
        var serviceGoveePayload = _ = new ServiceGoveePayload();
        _ = serviceGoveePayload.Payload.Capability.Value = rgbValue;
        this.SendPayloads(
            serviceGoveePayload
        );
    }
    
    private const string             c_goveeAddress     = "https://openapi.api.govee.com/";
    private const string             c_sku              = "H607C";
    private const string             c_apiKey           = "8ac3d53e-b861-4b94-a59b-461918168427";
    private static readonly string[] s_hardwareIds      =
    [
        "2A:A8:D8:7C:B4:47:41:D6",
        "2A:E6:FA:FB:20:2E:71:B1",
    ];
    
    private ServiceGodotHttp         m_serviceGodotHttp = null;

    private static int ConvertColorToInt(
        Color color
    )
    {
        return ((color.R8 & 0xFF) << 16) | ((color.G8 & 0xFF) << 8) | ((color.B8 & 0xFF) << 0);
    }

    private void RetrieveResources()
    {
        var serviceGodots           = _ = Services.GetService<ServiceGodots>();
        _ = this.m_serviceGodotHttp = _ = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
    }

    private void SendPayloads(
        ServiceGoveePayload payload
    )
    {
        foreach (var hardwareId in _ = ServiceGovee.s_hardwareIds)
        {
            _ = payload.Payload.Device = _ = hardwareId;
            
            var json = _ = JsonHelper.Serialize(
                @object: _ = payload
            );
            
            this.m_serviceGodotHttp.SendHttpRequest(
                url:                     _ = $"{_ = ServiceGovee.c_goveeAddress}router/api/v1/device/control",
                headers:                 [
                    $"Govee-API-Key: {_ = ServiceGovee.c_apiKey}",
                    $"Content-Type: application/json",
                ],
                method:                  _ = HttpClient.Method.Post,
                json:                    _ = json,
                requestCompletedHandler: (
                    long     result,
                    long     responseCode,
                    string[] headers,
                    byte[]   body
                ) =>
                {
                    json = _ = Encoding.UTF8.GetString(body);
                }
            );
        }
    }
}