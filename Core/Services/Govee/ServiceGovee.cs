
using System.Threading.Tasks;

namespace Overlay.Core.Services.Govee;

public partial class ServiceGovee() :
    IService
{
    Task IService.Setup()
    {
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
    
    private const string c_goveeAddress  = "https://openapi.api.govee.com/";
    
    
}