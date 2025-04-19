using System;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;

namespace Overlay.Core.Services.Joysticks.Payloads;

internal static class ServiceJoystickWebSocketPayloadStreamEvents
{
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataChatTimersCleared>      ChatTimersCleared      { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataChatTimerStarted>       ChatTimerStarted       { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataDeviceConnected>        DeviceConnected        { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataDeviceDisconnected>     DeviceDisconnected     { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataDeviceSettingsUpdated>  DeviceSettingsUpdated  { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataDropinStream>           DropinStream           { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataEnded>                  Ended                  { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataFollowed>               Followed               { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataFollowerCountUpdated>   FollowerCountUpdated   { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataGiftedSubscriptions>    GiftedSubscriptions    { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataMilestoneCompleted>     MilestoneCompleted     { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataPvpSessionEnded>        PvpSessionEnded        { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataPvpSessionEnding>       PvpSessionEnding       { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataPvpSessionReady>        PvpSessionReady        { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataPvpSessionRequested>    PvpSessionRequested    { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataPvpSessionStarted>      PvpSessionStarted      { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataResubscribed>           Resubscribed           { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataSceneUpdated>           SceneUpdated           { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataSettingsUpdated>        SettingsUpdated        { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataStarted>                Started                { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataStreamDroppedIn>        StreamDroppedIn        { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataStreamEnding>           StreamEnding           { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataStreamModeUpdated>      StreamModeUpdated      { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataStreamResuming>         StreamResuming         { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataSubscribed>             Subscribed             { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataSubscriberCountUpdated> SubscriberCountUpdated { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataTipGoalCreated>         TipGoalCreated         { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataTipGoalDeleted>         TipGoalDeleted         { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataTipGoalIncreased>       TipGoalIncreased       { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataTipGoalMet>             TipGoalMet             { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataTipGoalUpdated>         TipGoalUpdated         { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataTipMenuItemLocked>      TipMenuItemLocked      { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataTipMenuItemUnlocked>    TipMenuItemUnlocked    { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataTipped>                 Tipped                 { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataUserMuted>              UserMuted              { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataUserUnmuted>            UserUnmuted            { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataViewerCountUpdated>     ViewerCountUpdated     { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessageMetadataWheelSpinClaimed>       WheelSpinClaimed       { get; set; } = null;
}