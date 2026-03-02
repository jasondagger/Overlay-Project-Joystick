
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;
using Overlay.Core.Tools;

namespace Overlay.Core.Services.Joysticks.Payloads;

internal static class ServiceJoystickWebSocketPayloadStreamEventHandler
{
    internal static void HandleWebSocketPayloadStreamEvent(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        var payloadMessageType = payloadMessage.Type;
        switch (payloadMessageType)
        {
            case "ChatTimersCleared":
	            ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventChatTimersCleared(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "ChatTimerStarted":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventChatTimerStarted(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "DeviceConnected":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventDeviceConnected(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "DeviceDisconnected":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventDeviceDisconnected(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "DeviceSettingsUpdated":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventDeviceSettingsUpdated(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "DropinStream":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventDropinStream(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "Ended":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventEnded(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "Followed":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventFollowed(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "FollowerCountUpdated":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventFollowerCountUpdated(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "GiftedSubscriptions":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventGiftedSubscriptions(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "MilestoneCompleted":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventMilestoneCompleted(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "PvpSessionEnded":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventPvpSessionEnded(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "PvpSessionEnding":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventPvpSessionEnding(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "PvpSessionReady":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventPvpSessionReady(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "PvpSessionRequested":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventPvpSessionRequested(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "PvpSessionStarted":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventPvpSessionStarted(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "Resubscribed":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventResubscribed(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "SceneUpdated":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventSceneUpdated(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "SettingsUpdated":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventSettingsUpdated(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "Started":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventStarted(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "StreamDroppedIn":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventStreamDroppedIn(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "StreamEnding":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventStreamEnding(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "StreamModeUpdated":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventStreamModeUpdated(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "StreamResuming":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventStreamResuming(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "Subscribed":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventSubscribed(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "SubscriberCountUpdated":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventSubscriberCountUpdated(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "TipGoalCreated":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventTipGoalCreated(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "TipGoalDeleted":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventTipGoalDeleted(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "TipGoalIncreased":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventTipGoalIncreased(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "TipGoalMet":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventTipGoalMet(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "TipGoalUpdated":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventTipGoalUpdated(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "TipMenuItemLocked":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventTipMenuItemLocked(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "TipMenuItemUnlocked":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventTipMenuItemUnlocked(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "Tipped":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventTipped(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "UserMuted":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventUserMuted(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "UserUnmuted":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventUserUnmuted(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "ViewerCountUpdated":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventViewerCountUpdated(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "WheelSpinClaimed":
                ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEventWheelSpinClaimed(
                    payloadMessage: payloadMessage
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
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataChatTimersCleared>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventChatTimerStarted(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.ChatTimerStarted?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataChatTimerStarted>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventDeviceConnected(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.DeviceConnected?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataDeviceConnected>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventDeviceDisconnected(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.DeviceDisconnected?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataDeviceDisconnected>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventDeviceSettingsUpdated(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.DeviceSettingsUpdated?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataDeviceSettingsUpdated>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventDropinStream(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.DropinStream?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataDropinStream>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventEnded(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.Ended?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataEnded>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventFollowed(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.Followed?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataFollowed>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventFollowerCountUpdated(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.FollowerCountUpdated?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataFollowerCountUpdated>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventGiftedSubscriptions(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.GiftedSubscriptions?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataGiftedSubscriptions>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventMilestoneCompleted(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.MilestoneCompleted?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataMilestoneCompleted>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventPvpSessionEnded(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.PvpSessionEnded?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataPvpSessionEnded>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventPvpSessionEnding(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.PvpSessionEnding?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataPvpSessionEnding>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventPvpSessionReady(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.PvpSessionReady?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataPvpSessionReady>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventPvpSessionRequested(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.PvpSessionRequested?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataPvpSessionRequested>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventPvpSessionStarted(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.PvpSessionStarted?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataPvpSessionStarted>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventResubscribed(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.Resubscribed?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataResubscribed>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventSceneUpdated(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.SceneUpdated?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataSceneUpdated>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventSettingsUpdated(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.SettingsUpdated?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataSettingsUpdated>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventStarted(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.Started?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataStarted>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventStreamDroppedIn(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.StreamDroppedIn?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataStreamDroppedIn>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventStreamEnding(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.StreamEnding?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataStreamEnding>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventStreamModeUpdated(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.StreamModeUpdated?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataStreamModeUpdated>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventStreamResuming(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.StreamResuming?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataStreamResuming>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventSubscribed(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.Subscribed?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataSubscribed>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventSubscriberCountUpdated(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.SubscriberCountUpdated?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataSubscriberCountUpdated>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventTipGoalCreated(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.TipGoalCreated?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataTipGoalCreated>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventTipGoalDeleted(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.TipGoalDeleted?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataTipGoalDeleted>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventTipGoalIncreased(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.TipGoalIncreased?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataTipGoalIncreased>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventTipGoalMet(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.TipGoalMet?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataTipGoalMet>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventTipGoalUpdated(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.TipGoalUpdated?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataTipGoalUpdated>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventTipMenuItemLocked(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.TipMenuItemLocked?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataTipMenuItemLocked>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventTipMenuItemUnlocked(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.TipMenuItemUnlocked?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataTipMenuItemUnlocked>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventTipped(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.Tipped?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataTipped>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventUserMuted(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.UserMuted?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataUserMuted>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventUserUnmuted(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.UserUnmuted?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataUserUnmuted>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventViewerCountUpdated(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.ViewerCountUpdated?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataViewerCountUpdated>(
                json: payloadMessage.Metadata
            )
        );
    }
    
    private static void HandleWebSocketPayloadStreamEventWheelSpinClaimed(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadStreamEvents.WheelSpinClaimed?.Invoke(
            obj: JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadMessageMetadataWheelSpinClaimed>(
                json: payloadMessage.Metadata
            )
        );
    }
}