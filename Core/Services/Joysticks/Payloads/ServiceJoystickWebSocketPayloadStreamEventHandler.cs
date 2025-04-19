
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;
using Overlay.Core.Tools;

namespace Overlay.Core.Services.Joysticks.Payloads;

internal static class ServiceJoystickWebSocketPayloadStreamEventHandler
{
    internal static void HandleWebSocketPayloadStreamEvent(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        var payloadMessageType = _ = payloadMessage.Type;
        switch (_ = payloadMessageType)
        {
            case "ChatTimersCleared":
	            ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventChatTimersCleared(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "ChatTimerStarted":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventChatTimerStarted(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "DeviceConnected":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventDeviceConnected(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "DeviceDisconnected":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventDeviceDisconnected(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "DeviceSettingsUpdated":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventDeviceSettingsUpdated(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "DropinStream":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventDropinStream(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "Ended":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventEnded(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "Followed":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventFollowed(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "FollowerCountUpdated":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventFollowerCountUpdated(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "GiftedSubscriptions":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventGiftedSubscriptions(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "MilestoneCompleted":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventMilestoneCompleted(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "PvpSessionEnded":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventPvpSessionEnded(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "PvpSessionEnding":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventPvpSessionEnding(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "PvpSessionReady":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventPvpSessionReady(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "PvpSessionRequested":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventPvpSessionRequested(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "PvpSessionStarted":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventPvpSessionStarted(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "Resubscribed":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventResubscribed(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "SceneUpdated":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventSceneUpdated(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "SettingsUpdated":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventSettingsUpdated(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "Started":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventStarted(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "StreamDroppedIn":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventStreamDroppedIn(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "StreamEnding":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventStreamEnding(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "StreamModeUpdated":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventStreamModeUpdated(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "StreamResuming":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventStreamResuming(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "Subscribed":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventSubscribed(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "SubscriberCountUpdated":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventSubscriberCountUpdated(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "TipGoalCreated":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventTipGoalCreated(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "TipGoalDeleted":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventTipGoalDeleted(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "TipGoalIncreased":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventTipGoalIncreased(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "TipGoalMet":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventTipGoalMet(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "TipGoalUpdated":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventTipGoalUpdated(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "TipMenuItemLocked":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventTipMenuItemLocked(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "TipMenuItemUnlocked":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventTipMenuItemUnlocked(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "Tipped":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventTipped(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "UserMuted":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventUserMuted(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "UserUnmuted":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventUserUnmuted(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "ViewerCountUpdated":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventViewerCountUpdated(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "WheelSpinClaimed":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventWheelSpinClaimed(
                    payloadMessage: _ = payloadMessage
                );
                break;
			
            default:
                return;
        }
    }
    
	private static void HandleWebSocketPayloadStreamEventChatTimersCleared(
		ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.ChatTimersCleared?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataChatTimersCleared>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventChatTimerStarted(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.ChatTimerStarted?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataChatTimerStarted>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventDeviceConnected(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.DeviceConnected?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataDeviceConnected>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventDeviceDisconnected(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.DeviceDisconnected?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataDeviceDisconnected>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventDeviceSettingsUpdated(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.DeviceSettingsUpdated?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataDeviceSettingsUpdated>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventDropinStream(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.DropinStream?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataDropinStream>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventEnded(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.Ended?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataEnded>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventFollowed(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.Followed?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataFollowed>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventFollowerCountUpdated(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.FollowerCountUpdated?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataFollowerCountUpdated>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventGiftedSubscriptions(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.GiftedSubscriptions?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataGiftedSubscriptions>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventMilestoneCompleted(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.MilestoneCompleted?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataMilestoneCompleted>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventPvpSessionEnded(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.PvpSessionEnded?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataPvpSessionEnded>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventPvpSessionEnding(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.PvpSessionEnding?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataPvpSessionEnding>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventPvpSessionReady(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.PvpSessionReady?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataPvpSessionReady>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventPvpSessionRequested(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.PvpSessionRequested?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataPvpSessionRequested>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventPvpSessionStarted(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.PvpSessionStarted?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataPvpSessionStarted>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventResubscribed(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.Resubscribed?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataResubscribed>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventSceneUpdated(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.SceneUpdated?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataSceneUpdated>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventSettingsUpdated(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.SettingsUpdated?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataSettingsUpdated>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventStarted(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.Started?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataStarted>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventStreamDroppedIn(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.StreamDroppedIn?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataStreamDroppedIn>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventStreamEnding(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.StreamEnding?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataStreamEnding>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventStreamModeUpdated(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.StreamModeUpdated?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataStreamModeUpdated>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventStreamResuming(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.StreamResuming?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataStreamResuming>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventSubscribed(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.Subscribed?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataSubscribed>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventSubscriberCountUpdated(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.SubscriberCountUpdated?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataSubscriberCountUpdated>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventTipGoalCreated(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.TipGoalCreated?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataTipGoalCreated>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventTipGoalDeleted(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.TipGoalDeleted?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataTipGoalDeleted>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventTipGoalIncreased(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.TipGoalIncreased?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataTipGoalIncreased>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventTipGoalMet(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.TipGoalMet?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataTipGoalMet>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventTipGoalUpdated(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.TipGoalUpdated?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataTipGoalUpdated>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventTipMenuItemLocked(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.TipMenuItemLocked?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataTipMenuItemLocked>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventTipMenuItemUnlocked(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.TipMenuItemUnlocked?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataTipMenuItemUnlocked>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventTipped(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.Tipped?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataTipped>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventUserMuted(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.UserMuted?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataUserMuted>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventUserUnmuted(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.UserUnmuted?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataUserUnmuted>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventViewerCountUpdated(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.ViewerCountUpdated?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataViewerCountUpdated>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventWheelSpinClaimed(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.WheelSpinClaimed?.Invoke(
            obj: _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataWheelSpinClaimed>(
                json: _ = payloadMessage.Metadata
            )
        );
    }
}