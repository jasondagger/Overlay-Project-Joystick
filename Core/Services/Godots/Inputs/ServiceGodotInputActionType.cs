
namespace Overlay.Core.Services.Godots.Inputs;

internal enum ServiceGodotInputActionType :
    uint
{
    ChangeLayoutToDefault = 0U,
    ChangeLayoutToCode,
    ChangeLayoutToAfk,
    CloseApplication,
}