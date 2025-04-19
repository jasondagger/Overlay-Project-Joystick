
namespace Overlay.Core.Services.Godots.Inputs;

internal enum ServiceGodotInputStateType :
    uint
{
    Pressed = 0U,
    Pressing,
    Released,
    Unbound,
}