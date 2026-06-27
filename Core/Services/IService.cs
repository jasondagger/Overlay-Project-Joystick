
namespace Overlay.Core.Services;

using System.Threading.Tasks;

public interface IService
{
    internal abstract Task Setup();
    internal abstract Task Start();
    internal abstract Task Stop();
}