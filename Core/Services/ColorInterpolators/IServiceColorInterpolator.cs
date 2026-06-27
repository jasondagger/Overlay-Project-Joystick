using Godot;

namespace Overlay.Core.Services.ColorInterpolators;

public interface IServiceColorInterpolator : IService
{
    public abstract void SetColorMode(
        ServiceColorInterpolatorColorMode serviceColorInterpolatorColorMode
    );
}