
using Godot;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Https;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Spotifies.Responses;
using Overlay.Core.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Overlay.Core.Services.Databases;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Joysticks.Payloads;

namespace Overlay.Core.Services.Spotifies;

public sealed partial class ServiceSpotify : 
    IService
{
    Task IService.Setup()
    {
        this.RetrieveResources();
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
    
    internal void RequestSkipToNextTrack()
    {
        var headers = new List<string>()
        {
            $"Authorization: Bearer {this.m_accessToken.AccessToken}",
        };
        this.m_serviceGodotHttp.SendHttpRequest(
            url:                     $"{ServiceSpotify.c_uriApi}/me/player/next",
            headers:                 headers,
            method:                  HttpClient.Method.Post,
            json:                    string.Empty,
            requestCompletedHandler: this.OnRequestSkipToNextCompleted
        );
    }
    
    internal void RequestTrackQueueBySearchTerms(
        string searchParameters
    )
    {
        var headers = new List<string>()
        {
            $"Authorization: Bearer {this.m_accessToken.AccessToken}",
        };
        this.m_serviceGodotHttp.SendHttpRequest(
            url:                     $"{ServiceSpotify.c_uriApi}/search?q={Uri.EscapeDataString(stringToEscape: searchParameters)}&type=track&limit=10",
            headers:                 headers,
            method:                  HttpClient.Method.Get,
            json:                    string.Empty,
            requestCompletedHandler: this.OnRequestTrackSearchCompleted
        );
    }

    private const string                        c_authorizationCode = "AQCSSUISzULYyX9xAvkXzy1dKRJeWuu-YzyKxniJ3wa8IV69tYhoheOUS3V4_-xEW4-Hyq8wPYUWhU5mj0CnkYgSylswkPS-YIERH-bMOnYtkbibD6lO_CNSJR5kN4EhIzKMa4cukiZWLtjCqlvtwG2IMC6csiN1vWGArby_kiPXz9h3GlAQwmGxZ0R-UH5E8_xzotBdS-YOZt16Jp3hyOOXCNIexNZgXlAdZTXn2QZxcbtRQgDu51nO6EAgQZ5Pt6cW9GnTvYRHvze6qVH3IYDBv4Ni";
    private const string                        c_uriAccessToken    = "https://accounts.spotify.com/api/token";
    private const string                        c_uriApi            = "https://api.spotify.com/v1";
    private const string                        c_uriRedirect       = "http://127.0.0.1:8888/callback";
    private const string                        c_userAccessScopes  = "user-modify-playback-state user-read-currently-playing user-read-playback-state";
    
    private Queue<ServiceSpotifyRequest>        m_requestQueue      = new();
    private ServiceGodotHttp                    m_serviceGodotHttp  = null;
    private readonly SpotifyResponseAccessToken m_accessToken       = new();
    private string                              m_clientId          = string.Empty;
    private string                              m_clientSecret      = string.Empty;

    private void HandleServiceDatabaseRetrievedSpotifyData(
        ServiceDatabaseTaskRetrievedSpotifyData spotifyData
    )
    {
        var result = spotifyData.Result;
        
        this.m_clientId                 = result.SpotifyData_Client_Id;
        this.m_clientSecret             = result.SpotifyData_Client_Secret;
        this.m_accessToken.AccessToken  = result.SpotifyData_Access_Token;
        this.m_accessToken.RefreshToken = result.SpotifyData_Refresh_Token;
        
        this.RequestAccessToken();
    }
    
    private void OnRequestAccessTokenCompleted(
        long     result,
        long     responseCode,
        string[] headers,
        byte[]   body
    )
    {
        if (
            ServiceGodotHttp.IsResponseCodeSuccessful(
                responseCode: responseCode
            ) is false
        )
        {
            return;
        }

        var json = Encoding.UTF8.GetString(
            bytes: body,
            index: 0,
            count: body.Length
        );
        var accessToken = JsonHelper.Deserialize<SpotifyResponseAccessToken>(
            json: json
        );
        
        this.m_accessToken.AccessToken = accessToken.AccessToken;

        Task.Run(
            function:
            async () =>
            {
                await Task.Delay(
                    millisecondsDelay: 3500000
                );
                
                this.RequestAccessToken();
            }
        );
    }
    
    private void OnRequestSkipToNextCompleted(
        long     result,
        long     responseCode,
        string[] headers,
        byte[]   body
    )
    {
        var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
        
        if (
            ServiceGodotHttp.IsResponseCodeSuccessful(
                responseCode: responseCode
            ) is false
        )
        {
            serviceJoystickBot.SendChatMessage(
                message: "Song failed to skip."
            );
            return;
        }

        serviceJoystickBot.SendChatMessage(
            message: "Song successfully skipped."
        );
    }
    
    private void OnRequestTrackQueueCompleted(
        long     result,
        long     responseCode,
        string[] headers,
        byte[]   body
    )
    {
        var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();

        if (
            ServiceGodotHttp.IsResponseCodeSuccessful(
                responseCode: responseCode
            ) is false
        )
        {
            serviceJoystickBot.SendChatMessage(
                message: "Song failed to queue."
            );
            ServiceJoystickWebSocketPayloadChatHandler.ProcessSongRequest(
                succeeded: false
            );
            return;
        }

        serviceJoystickBot.SendChatMessage(
            message: "Song successfully queued."
        );
        ServiceJoystickWebSocketPayloadChatHandler.ProcessSongRequest(
            succeeded: true
        );
    }
    
    private void OnRequestTrackSearchCompleted(
        long     result,
        long     responseCode,
        string[] headers,
        byte[]   body
    )
    {
        if (
            ServiceGodotHttp.IsResponseCodeSuccessful(
                responseCode: responseCode
            ) is false
        )
        {
            return;
        }
        
        var spotifyResponse = JsonHelper.Deserialize<SpotifyResponseSearchItem>(
            json: Encoding.UTF8.GetString(
                bytes: body,
                index: 0,
                count: body.Length
            )
        );

        var tracks = spotifyResponse?.Tracks;
        var items  = tracks?.Items;
        if ( 
            items is null || 
            items.Length is 0
        )
        {
            return;
        }
            
        var track = items[0];
        var uri   = track.Uri;
        this.RequestTrackAddedToQueue(
            trackUri: uri
        );
    }

    /*private void ProcessRequests()
    {
        Task.Run(
            function:
            async () =>
            {
                while (true)
                {
                    ServiceSpotifyRequest request;
                    lock (m_currentSpotifyTwitchDatasLock)
                    {
                        if (this.m_requestQueue.Count > 0u)
                        {
                            request = this.m_requestQueue.Dequeue();
                        }
                        else
                        {
                            return;
                        }
                    }

                    var spotifyTwitchDataRequestType = m_spotifyTwitchData.SpotifyTwitchDataRequestType;
                    switch (spotifyTwitchDataRequestType)
                    {
                        case SpotifyTwitchDataRequestType.CurrentTrack:
                            RequestCurrentTrack();
                            break;

                        case SpotifyTwitchDataRequestType.TrackQueueBySearchTerms:
                            RequestUserQueueBeforeTrackQueuedBySearchTerms();
                            break;

                        case SpotifyTwitchDataRequestType.TrackQueueByTrackId:
                            RequestUserQueueBeforeTrackQueuedByTrackId();
                            break;

                        case SpotifyTwitchDataRequestType.TrackSkip:
                            RequestCurrentTrackAndSkip();
                            break;

                        case SpotifyTwitchDataRequestType.UserTrackQueue:
                            RequestUserQueueForUser();
                            break;

                        default:
                            break;
                    }
                }
            }
        );
    }*/
    
    private void RequestAccessToken()
    {
        var headers = new List<string>()
        {
            $"content-type: application/x-www-form-urlencoded",
            $"Authorization: Basic {Convert.ToBase64String(inArray: Encoding.UTF8.GetBytes(s: $"{this.m_clientId}:{this.m_clientSecret}"))}",
        };
        this.m_serviceGodotHttp.SendHttpRequest(
            url:                     $"{ServiceSpotify.c_uriAccessToken}",
            headers:                 headers,
            method:                  HttpClient.Method.Post,
            json:                    $"grant_type=refresh_token&" +
                                     $"refresh_token={this.m_accessToken.RefreshToken}",
            requestCompletedHandler: this.OnRequestAccessTokenCompleted
        );
    }

    private void RequestAccessTokenOld()
    {
        var headers = new List<string>()
        {
            $"content-type: application/x-www-form-urlencoded",
            $"Authorization: Basic {Convert.ToBase64String(inArray: Encoding.UTF8.GetBytes(s: $"{this.m_clientId}:{this.m_clientSecret}"))}",
        };
        this.m_serviceGodotHttp.SendHttpRequest(
            url:                     $"{ServiceSpotify.c_uriAccessToken}",
            headers:                 headers,
            method:                  HttpClient.Method.Post,
            json:                    $"grant_type=authorization_code&" +
                                     $"code={ServiceSpotify.c_authorizationCode}&" +
                                     $"redirect_uri={ServiceSpotify.c_uriRedirect}",
            requestCompletedHandler: this.OnRequestAccessTokenCompleted
        );
    }
    
    private void RequestTrackAddedToQueue(
        string trackUri
    )
    {
        var headers = new List<string>()
        {
            $"Authorization: Bearer {this.m_accessToken.AccessToken}",
        };
        this.m_serviceGodotHttp.SendHttpRequest(
            url:                     $"{ServiceSpotify.c_uriApi}/me/player/queue?uri={Uri.EscapeDataString(stringToEscape: trackUri)}",
            headers:                 headers,
            method:                  HttpClient.Method.Post,
            json:                    string.Empty,
            requestCompletedHandler: this.OnRequestTrackQueueCompleted
        );
    }
    
    private void RequestUserAuthorization()
    {
        OS.ShellOpen(
            uri: 
                $"https://accounts.spotify.com/authorize?" +
                $"response_type=code&" +
                $"client_id={this.m_clientId}&" +
                $"scope={Uri.EscapeDataString(stringToEscape: ServiceSpotify.c_userAccessScopes)}&" +
                $"redirect_uri={ServiceSpotify.c_uriRedirect}&" +
                $"state=1&" +
                $"show_dialog=true"
        );
    }
    
    private void RetrieveResources()
    {
        var serviceGodots           = Services.GetService<ServiceGodots>();
        this.m_serviceGodotHttp = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
        
        ServiceDatabaseTaskEvents.RetrievedSpotifyData += this.HandleServiceDatabaseRetrievedSpotifyData;
        
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveSpotifyData
        );
    }
}