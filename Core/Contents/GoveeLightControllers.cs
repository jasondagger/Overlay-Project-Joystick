
using Overlay.Core.Services.ColorInterpolators;
using Overlay.Core.Services.Govee;
using Overlay.Core.Services.TeamFortress2s;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Overlay.Core.Services.JoystickBots;

namespace Overlay.Core.Contents;

public static class GoveeLightControllers
{
    public const int DelayInColorCycleLongInMilliseconds  = 900000;
    public const int DelayInColorCycleInShortMilliseconds = 60000;
    
    public static int GetRemainingTime()
    {
        return Math.Max(
            val1: 0,
            val2: GoveeLightControllers.s_currentTargetDuration - (int)GoveeLightControllers.s_syncStopwatch.ElapsedMilliseconds
        );
    }

    public static void Reset()
    {
        GoveeLightControllerCeiling.Instance.Reset();
        GoveeLightControllerStanding.Instance.Reset();
    }

    public static void SetGoveeLightControllers(
        (IServiceColorInterpolatorDefinition.ColorType? color, string scene) lightsCeiling,
        (IServiceColorInterpolatorDefinition.ColorType? color, string scene) lightsStanding
    )
    {
        if (lightsCeiling.scene is not null)
        {
            GoveeLightControllerCeiling.Instance.SetAndStoreLightScene(
                sceneName: lightsCeiling.scene
            );
        }
        else if (lightsCeiling.color.HasValue)
        {
            GoveeLightControllerCeiling.Instance.SetAndStoreLightColor(
                colorType: lightsCeiling.color.Value
            );
        }

        if (lightsStanding.scene is not null)
        {
            GoveeLightControllerStanding.Instance.SetAndStoreLightScene(
                sceneName: lightsStanding.scene
            );
        }
        else if (lightsStanding.color.HasValue)
        {
            GoveeLightControllerStanding.Instance.SetAndStoreLightColor(
                colorType: lightsStanding.color.Value
            );
        }
        
        GoveeLightControllers.SendBotMessage(
            stateA: lightsCeiling,
            stateB: lightsStanding
        );
    }
    
    public static void StartColorCycleChange(
        int  delayInMilliseconds,
        int? customDelay = null
    )
    {
            GoveeLightControllers.s_cancellationTokenSource?.Cancel();
            GoveeLightControllers.s_cancellationTokenSource?.Dispose();
            GoveeLightControllers.s_cancellationTokenSource = new CancellationTokenSource();

            if (customDelay is null)
            {
                GoveeLightControllers.s_currentTargetDuration = delayInMilliseconds;
                GoveeLightControllers.s_syncStopwatch.Restart();
            }
            
            var cancellationToken = GoveeLightControllers.s_cancellationTokenSource.Token;
            var actualDelay       = customDelay ?? GoveeLightControllers.s_currentTargetDuration;

            Task.Run(
                function: async () =>
                {
                    await Task.Delay(
                        millisecondsDelay: actualDelay,
                        cancellationToken: cancellationToken
                    );

                    while (cancellationToken.IsCancellationRequested is false)
                    {
                        var duration = GoveeLightControllers.s_currentTargetDuration;
                                
                        GoveeLightControllers.s_syncStopwatch.Restart();
                        GoveeLightControllers.s_currentTargetDuration = GoveeLightControllers.DelayInColorCycleInShortMilliseconds;
                                
                        GoveeLightControllers.ExecuteUnisonTick();
                            
                        await Task.Delay(
                            millisecondsDelay: duration, 
                            cancellationToken: cancellationToken
                        );
                    }
                }, 
                cancellationToken: cancellationToken
            );
    }
    
    private const int                      c_chanceThresholdLightScene = 86;
    
    private static CancellationTokenSource s_cancellationTokenSource   = null;
    private static readonly Stopwatch      s_syncStopwatch             = new();
    private static int                     s_currentTargetDuration     = 0;
    
    private static void ApplyStateToController<TServiceColorInterpolator>(
        GoveeLightController<TServiceColorInterpolator>                      instance, 
        (IServiceColorInterpolatorDefinition.ColorType? color, string scene) state, 
        bool                                                                 storeOnly
    ) where TServiceColorInterpolator : IServiceColorInterpolator
    {
        if (storeOnly)
        {
            if (state.scene != null)
            {
                instance.StoreSceneName(
                    sceneName: state.scene
                );
            }
            else
            {
                instance.StoreColorType(
                    colorType: state.color.Value
                );
            }
            instance.StoreDelayInColorCycleInMilliseconds(
                delayInMilliseconds: GoveeLightControllers.DelayInColorCycleInShortMilliseconds
            );
        }
        else
        {
            if (state.scene != null)
            {
                instance.SetAndStoreLightScene(
                    sceneName:              state.scene, 
                    delayInMilliseconds:    GoveeLightControllers.DelayInColorCycleInShortMilliseconds, 
                    ignoreColorChangeStart: true
                );
            }
            else
            {
                instance.SetAndStoreLightColor(
                    colorType:              state.color.Value, 
                    delayInMilliseconds:    GoveeLightControllers.DelayInColorCycleInShortMilliseconds, 
                    ignoreColorChangeStart: true
                );
            }
        }
    }

    private static void ExecuteUnisonTick()
    {
        var rollForSame  = Random.Shared.Next(
            maxValue: 10
        );
        var useSameState = rollForSame > 0;

        var serviceTeamFortress2 = Services.Services.GetService<ServiceTeamFortress2>();
        var isActive = serviceTeamFortress2.IsBhopping || serviceTeamFortress2.IsKillStreaking;
        
        var stateA = GoveeLightControllers.GetRandomState();
        var stateB = useSameState ? stateA : GoveeLightControllers.GetRandomState();

        if (isActive)
        {
            GoveeLightControllers.ApplyStateToController(
                instance:  GoveeLightControllerCeiling.Instance, 
                state:     stateA, 
                storeOnly: true
            );
            GoveeLightControllers.ApplyStateToController(
                instance:  GoveeLightControllerStanding.Instance, 
                state:     stateB, 
                storeOnly: true
            );
        }
        else
        {
            GoveeLightControllers.ApplyStateToController(
                instance:  GoveeLightControllerCeiling.Instance, 
                state:     stateA, 
                storeOnly: false
            );
            GoveeLightControllers.ApplyStateToController(
                instance:  GoveeLightControllerStanding.Instance, 
                state:     stateB, 
                storeOnly: false
            );
        }

        GoveeLightControllers.SendBotMessage(
            stateA: stateA,
            stateB: stateB
        );
    }
    
    private static string GetStateName(
        (IServiceColorInterpolatorDefinition.ColorType? color, string scene) state
    ) => state.scene ?? state.color.ToString();
    
    private static (IServiceColorInterpolatorDefinition.ColorType? color, string scene) GetRandomState()
    {
        var roll = Random.Shared.Next(
            maxValue: 101
        );
        if (roll >= GoveeLightControllers.c_chanceThresholdLightScene)
        {
            return (null, ServiceGoveeSceneRandomizer.GetRandomSceneName());
        }
    
        return (ServiceColorInterpolatorColorRandomizer.GetRandomColorType(), null);
    }

    private static void SendBotMessage(
        (IServiceColorInterpolatorDefinition.ColorType? color, string scene) stateA,
        (IServiceColorInterpolatorDefinition.ColorType? color, string scene) stateB
    )
    {
        var stateNameA = GoveeLightControllers.GetStateName(
            state: stateA
        );
        var stateNameB = GoveeLightControllers.GetStateName(
            state: stateB
        );

        var message = (stateNameA == stateNameB) 
            ? $"💡 Lights set to {stateNameA}." 
            : $"💡 Lights set to {stateNameA} & {stateNameB}.";

        var serviceJoystickBot =  Services.Services.GetService<ServiceJoystickBot>();
        serviceJoystickBot.SendChatMessageSilently(
            message: message
        );
    }
}