
using Godot;
using Overlay.Core.Services.Databases;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Https;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Spotifies.Responses;
using Overlay.Core.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.Godots.TextToSpeeches;

namespace Overlay.Core.Services.Spotifies;

internal sealed class ServiceSpotify : 
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
        this.ResetCooldown();
        
        var headers = new List<string>()
        {
            $"Authorization: Bearer {this.m_accessToken.AccessToken}",
        };
        this.m_serviceGodotHttp.SendHttpRequest(
            url:                     $"{ServiceSpotify.c_uriApi}/me/player/next",
            headers:                 headers,
            method:                  HttpClient.Method.Post,
            json:                    string.Empty,
            requestCompletedHandler: ServiceSpotify.OnRequestSkipToNextCompleted
        );
    }
    
    internal void RequestSkipToNextTrackWithNoNotification()
    {
        var isOnCooldown = this.IsCooldownActive();
        if (isOnCooldown is true)
        {
            return;
        }
        
        var headers = new List<string>()
        {
            $"Authorization: Bearer {this.m_accessToken.AccessToken}",
        };
        this.m_serviceGodotHttp.SendHttpRequest(
            url:                     $"{ServiceSpotify.c_uriApi}/me/player/next",
            headers:                 headers,
            method:                  HttpClient.Method.Post,
            json:                    string.Empty,
            requestCompletedHandler: ServiceSpotify.OnRequestSkipToNextWithNoNotificationCompleted
        );
    }
    
    internal void RequestTrackQueueBySearchTerms(
        string searchParameters
    )
    {
        this.ResetCooldown();
        
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
    
    internal void RequestTrackQueueBySearchTermsWithNoNotification(
        string searchParameters
    )
    {
        var isOnCooldown = this.IsCooldownActive();
        if (isOnCooldown is true)
        {
            return;
        }
        
        var headers = new List<string>()
        {
            $"Authorization: Bearer {this.m_accessToken.AccessToken}",
        };
        this.m_serviceGodotHttp.SendHttpRequest(
            url:                     $"{ServiceSpotify.c_uriApi}/search?q={Uri.EscapeDataString(stringToEscape: searchParameters)}&type=track&limit=10",
            headers:                 headers,
            method:                  HttpClient.Method.Get,
            json:                    string.Empty,
            requestCompletedHandler: this.OnRequestTrackSearchWithNoNotificationCompleted
        );
    }

    internal void RequestTrackRepeat()
    {
        this.RequestTrackQueueBySearchTerms(
            searchParameters: this.m_lastTrackAndArtist 
        );
    }

    private const string                        c_authorizationCode         = "AQDRY-0WsMTiBxh0a_7Y6NjiHKSEcIjxoriW9soDZMXWhvkBqkrLTXUZ1p76rJj8TjNN7r1veNNHZk3SDnTmrWlTWNSuug9XRrs1XuVwNaSXehJBkYGaUkhSE5tW95yExd64WUTYF3SdTSXjJxa0gbdLW4OOGlUaI9_MPkiGV9GxGBVFOGC2cBgX3z9FDGNHmLmRN6SUOUKLnP7AkMYDAVC0kWwhlwI9Bt4SCdPDtnFxl0EAkBVQvs6oAH1AdcEk9MN-xpQkyMfj7fc_8xJ1bd6O6l5W";
    private const string                        c_uriAccessToken            = "https://accounts.spotify.com/api/token";
    private const string                        c_uriApi                    = "https://api.spotify.com/v1";
    private const string                        c_uriRedirect               = "http://127.0.0.1:8888/callback";
    private const string                        c_userAccessScopes          = "user-modify-playback-state user-read-currently-playing user-read-playback-state";
    private const int                           c_cooldownDurationInMinutes = 10;
    
    private readonly SpotifyResponseAccessToken m_accessToken               = new();
    private string                              m_clientId                  = string.Empty;
    private string                              m_clientSecret              = string.Empty;
    private DateTime                            m_cooldownEndTime           = DateTime.MinValue;
    private readonly object                     m_cooldownLock              = new();
    private string                              m_lastTrackId               = string.Empty;
    private string                              m_lastTrackAndArtist        = string.Empty;
    private ServiceGodotHttp                    m_serviceGodotHttp          = null;

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

    private bool IsCooldownActive()
    {
        lock (this.m_cooldownLock)
        {
            return DateTime.Now < this.m_cooldownEndTime;
        }
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
        
        this.RequestCurrentlyPlaying();

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
    
    private void OnRequestCurrentlyPlayingCompleted(
        long     result,
        long     responseCode,
        string[] headers,
        byte[]   body
    )
    {
        if (
            responseCode is 204 || 
            ServiceGodotHttp.IsResponseCodeSuccessful(
                responseCode: responseCode
            ) is false
        )
        {
            this.ScheduleNextCurrentlyPlayingPoll(
                millisecondsDelay: 10000
            );
            return;
        }

        var json = Encoding.UTF8.GetString(
            bytes: body
        );
        var currentTrack = JsonHelper.Deserialize<SpotifyResponseCurrentTrack>(
            json: json
        );

        if (
            currentTrack?.Track    is not null && 
            currentTrack.IsPlaying is true &&
            currentTrack.Track.Id  != this.m_lastTrackId
        )
        {
            this.m_lastTrackId        = currentTrack.Track.Id;
            this.m_lastTrackAndArtist = $"{currentTrack.Track.Name} by {currentTrack.Track.Artists[0].Name}";
        
            var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
            serviceJoystickBot.SendChatMessageSilently(
                message: $"🎵 Now Playing: {currentTrack.Track.Name} by {currentTrack.Track.Artists[0].Name}"
            );

            ServiceGodotTextToSpeech.SpeakWithInterrupt(
                message: $"Now Playing: {currentTrack.Track.Name} by {currentTrack.Track.Artists[0].Name}"
            );
        }

        this.ScheduleNextCurrentlyPlayingPoll(
            millisecondsDelay: 10000
        );
    }
    
    private static void OnRequestSkipToNextCompleted(
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
            serviceJoystickBot.SendChatMessageSilently(
                message: "🛑 Song failed to skip."
            );
            return;
        }

        serviceJoystickBot.SendChatMessageSilently(
            message: "🎵 Song successfully skipped."
        );
    }
    
    private static void OnRequestSkipToNextWithNoNotificationCompleted(
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
            var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
            serviceJoystickBot.SendChatMessageSilently(
                message: "🛑 Automatically queued song failed to skip."
            );
        }
    }
    
    private static void OnRequestTrackQueueCompleted(
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
            serviceJoystickBot.SendChatMessageSilently(
                message: "🛑 Song failed to queue."
            );
            ServiceJoystickWebSocketPayloadChatHandler.ProcessSongRequest(
                succeeded: false
            );
            return;
        }

        serviceJoystickBot.SendChatMessageSilently(
            message: "🎵 Song successfully queued."
        );
        ServiceJoystickWebSocketPayloadChatHandler.ProcessSongRequest(
            succeeded: true
        );
    }
    
    private void OnRequestTrackQueueWithNoNotificationCompleted(
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
            var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
            serviceJoystickBot.SendChatMessageSilently(
                message: "🛑 Automatically queued song failed to queue."
            );

            return;
        }

        this.RequestSkipToNextTrackWithNoNotification();
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
    
    private void OnRequestTrackSearchWithNoNotificationCompleted(
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
        this.RequestTrackAddedToQueueWithNoNotification(
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
    
    private void RequestCurrentlyPlaying()
    {
        var headers = new List<string>()
        {
            $"Authorization: Bearer {this.m_accessToken.AccessToken}",
        };
    
        this.m_serviceGodotHttp.SendHttpRequest(
            url:                     $"{ServiceSpotify.c_uriApi}/me/player/currently-playing",
            headers:                 headers,
            method:                  HttpClient.Method.Get,
            json:                    string.Empty,
            requestCompletedHandler: this.OnRequestCurrentlyPlayingCompleted
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
            requestCompletedHandler: ServiceSpotify.OnRequestTrackQueueCompleted
        );
    }
    
    private void RequestTrackAddedToQueueWithNoNotification(
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
            requestCompletedHandler: this.OnRequestTrackQueueWithNoNotificationCompleted
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
    
    private void ResetCooldown()
    {
        lock (this.m_cooldownLock)
        {
            this.m_cooldownEndTime = DateTime.Now.AddMinutes(
                value: ServiceSpotify.c_cooldownDurationInMinutes
            );
        }
    }
    
    private void RetrieveResources()
    {
        var serviceGodots       = Services.GetService<ServiceGodots>();
        this.m_serviceGodotHttp = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
        
        ServiceDatabaseTaskEvents.RetrievedSpotifyData += this.HandleServiceDatabaseRetrievedSpotifyData;
        
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveSpotifyData
        );
    }
    
    private void ScheduleNextCurrentlyPlayingPoll(
        int millisecondsDelay
    )
    {
        Task.Run(
            function: async () =>
            {
                await Task.Delay(
                    millisecondsDelay: millisecondsDelay
                );
                this.RequestCurrentlyPlaying();
            }
        );
    }
}