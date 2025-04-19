
namespace Overlay.Core.Services.StreamStates;

internal interface IServiceStreamState
{
    public abstract void Load();
    public abstract void Unload();
}