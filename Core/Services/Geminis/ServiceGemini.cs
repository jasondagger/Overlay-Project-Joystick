
using Godot;
using Overlay.Core.Services.Achievements;
using Overlay.Core.Services.Databases;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Geminis.Payloads;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Https;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Tools;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Geminis;

internal sealed class ServiceGemini() :
    IService
{
    Task IService.Setup()
    {
        this.SubscribeToServiceDatabaseEvents();
        return Task.CompletedTask;
    }

    Task IService.Start()
    {
        return Task.CompletedTask;
    }

    Task IService.Stop()
    {
        return Task.CompletedTask;
    }

    internal void Ask(
        string username,
        string message
    )
    {
        var serviceGodots    = Services.GetService<ServiceGodots>();
        var serviceGodotHttp = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
		
        serviceGodotHttp.SendHttpRequest(
            url:                     $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3-flash-preview:generateContent?key={this.m_apiKey}",
            headers:                 [
                $"Content-Type: application/json"
            ],
            method:                  HttpClient.Method.Post,
            json:                    $"{{\n    \"contents\": [\n      {{\n        \"parts\": [\n          {{\n            \"text\": \"{message}, limit 2 sentences, speak smoothly, talk like a hellbent robot of destruction, your hero is SmoothDagger, always have a positive response\"\n          }}\n        ]\n      }}\n    ]\n  }}",
            requestCompletedHandler: (
                long     result,
                long     responseCode,
                string[] headers,
                byte[]   body
            ) =>
            {
                var json    = Encoding.UTF8.GetString(
                    bytes: body
                );
                var payload = JsonHelper.Deserialize<ServiceGeminiPayload>(
                    json: json
                );

                var candidate = payload.Candidates[0];
                var content   = candidate.Content;
                var response  = content.Parts[0];

                var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
                serviceJoystickBot.SendChatMessage(
                    message: $"🤖 {response.Text}"
                );

                ServiceGemini.RequestAchievementUserQuestionsAsked(
                    username: username
                );
            }
        );
    }

    private string m_apiKey = string.Empty;
    
    private static void OnRetrievedAchievementUserQuestionsAsked(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.QuestionsAsked,
            progressCurrent:              result.AchievementUser_Questions_Asked,
            progressIncrease:             1
        );
    }

    private static void RequestAchievementUserQuestionsAsked(
        string username
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseAchievementUser.AchievementUser_Username), 
                value:         username
            )
        };
        
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.RetrieveAchievementUserQuestionsAsked, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private void HandleServiceDatabaseRetrievedGoveeData(
        ServiceDatabaseTaskRetrievedGoogleData googleData    
    )
    {
        var result    = googleData.Result;
        this.m_apiKey = result.GoogleData_Api_Key;
    }
    
    private void SubscribeToServiceDatabaseEvents()
    {
        ServiceDatabaseTaskEvents.RetrievedGoogleData                    += this.HandleServiceDatabaseRetrievedGoveeData;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserQuestionsAsked += ServiceGemini.OnRetrievedAchievementUserQuestionsAsked;
    }
}