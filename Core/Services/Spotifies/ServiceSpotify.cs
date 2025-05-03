
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

namespace Overlay.Core.Services.Spotifies;

public sealed partial class ServiceSpotify : 
    IService
{
    Task IService.Setup()
    {
        this.RetrieveResources();
        return _ = Task.CompletedTask;
    }
    
    Task IService.Start()
    {
        return _ = Task.CompletedTask;
    }
    
    Task IService.Stop()
    {
        return _ = Task.CompletedTask;
    }

    private const string                 c_authorizationCode = "AQAvQ63vU3X5guTzivywl3-CqF5g7ETZVZ513VUUpGhpeGezU1SBeXgrYZ091LpSDecYW1i3kmaqfWjrin340DCvjmhm5SEhX6Mgcgm64Os2ZeDbbnaZfq2F8bb9bWGvIjk8CH10CLJELbDenwdt7xHk-5Cw_MvusNi9pWfle3Bj5T-clvvGAyGlrVQscIGY0NZEWoJ4SRKeIczQdWFpBw571q2NYPe6cbl9jDIOh4SzSiKvLNNTx3Xp0g7Hu5_3FxBdI2libvSygIBE8HQXJAHjWqMH";
    private const string                 c_uriAccessToken    = "https://accounts.spotify.com/api/token";
    private const string                 c_uriApi            = "https://api.spotify.com/v1";
    private const string                 c_uriRedirect       = "http://127.0.0.1:8888/callback";
    private const string                 c_userAccessScopes  = "user-modify-playback-state user-read-currently-playing user-read-playback-state";
    private const string                 c_refreshToken     = "AQDTGMYa6MkXzFEHIbtM8ckrtWam-cSCWFO1sOorKTMhjSJCMVWj2hrSwa_os4vQfhLENzT-pG0TLr9Y69AewEj4z8jM-Pl_IIaIZ-4jlX7srBVbkweIIi4CQ757tyeEhiU";
    
    private Queue<ServiceSpotifyRequest> m_requestQueue      = new();
    private ServiceGodotHttp             m_serviceGodotHttp  = null;
    private SpotifyResponseAccessToken   m_accessToken       = new();
    private string                       m_clientId          = _ = string.Empty;
    private string                       m_clientSecret      = _ = string.Empty;

    private void HandleServiceDatabaseRetrievedSpotifyData(
        ServiceDatabaseTaskRetrievedSpotifyData spotifyData
    )
    {
        var result = _ = spotifyData.Result;
        
        _ = this.m_clientId                 = _ = result.SpotifyData_Client_Id;
        _ = this.m_clientSecret             = _ = result.SpotifyData_Client_Secret;
        _ = this.m_accessToken.AccessToken  = _ = result.SpotifyData_Access_Token;
        _ = this.m_accessToken.RefreshToken = _ = result.SpotifyData_Refresh_Token;
        
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
                responseCode: _ = responseCode
            ) is false
        )
        {
            return;
        }
        
        _ = this.m_accessToken = _ = JsonHelper.Deserialize<SpotifyResponseAccessToken>(
            json: _ = Encoding.UTF8.GetString(
                bytes: _ = body,
                index: _ = 0,
                count: _ = body.Length
            )
        );
    }
    
    private void OnRequestTrackQueueCompleted(
        long     result,
        long     responseCode,
        string[] headers,
        byte[]   body
    )
    {
        var serviceJoystickBot = _ = Services.GetService<ServiceJoystickBot>();
        
        if (
            ServiceGodotHttp.IsResponseCodeSuccessful(
                responseCode: _ = responseCode
            ) is false
        )
        {
            serviceJoystickBot.SendChatMessage(
                message: "Song failed to queue."
            );
            return;
        }

        serviceJoystickBot.SendChatMessage(
            message: "Song successfully queued."
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
                responseCode: _ = responseCode
            ) is false
        )
        {
            return;
        }
        
        var spotifyResponse = _ = JsonHelper.Deserialize<SpotifyResponseSearchItem>(
            json: _ = Encoding.UTF8.GetString(
                bytes: _ = body,
                index: _ = 0,
                count: _ = body.Length
            )
        );

        var tracks = _ = spotifyResponse?.Tracks;
        var items  = _ = tracks?.Items;
        if ( 
            _ = items is null || 
            items.Length is 0
        )
        {
            return;
        }
            
        var track = _ = items[0];
        var uri   = _ = track.Uri;
        this.RequestTrackAddedToQueue(
            trackUri: uri
        );
    }

    /*private void ProcessRequests()
    {
        _ = Task.Run(
            function:
            async () =>
            {
                while (true)
                {
                    ServiceSpotifyRequest request;
                    lock (m_currentSpotifyTwitchDatasLock)
                    {
                        if (_ = this.m_requestQueue.Count > 0u)
                        {
                            _ = request = this.m_requestQueue.Dequeue();
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
            $"Authorization: Basic {_ = Convert.ToBase64String(inArray: _ = Encoding.UTF8.GetBytes(s: _ = $"{_ = this.m_clientId}:{_ = this.m_clientSecret}"))}",
        };
        this.m_serviceGodotHttp.SendHttpRequest(
            url:                     _ = $"{_ = ServiceSpotify.c_uriAccessToken}",
            headers:                 _ = headers,
            method:                  _ = HttpClient.Method.Post,
            json:                    _ = $"grant_type=authorization_code&" +
                                     $"code={_ = ServiceSpotify.c_authorizationCode}&" +
                                     $"redirect_uri={_ = ServiceSpotify.c_uriRedirect}",
            requestCompletedHandler: this.OnRequestAccessTokenCompleted
        );
    }
    
    private void RequestTrackAddedToQueue(
        string trackUri
    )
    {
        var headers = _ = new List<string>()
        {
            $"Authorization: Bearer {_ = this.m_accessToken.AccessToken}",
        };
        this.m_serviceGodotHttp.SendHttpRequest(
            url:                     $"{_ = ServiceSpotify.c_uriApi}/me/player/queue?uri={_ = Uri.EscapeDataString(stringToEscape: _ = trackUri)}",
            headers:                 headers,
            method:                  HttpClient.Method.Post,
            json:                    string.Empty,
            requestCompletedHandler: this.OnRequestTrackQueueCompleted
        );
    }
    
    private void RequestTrackQueueBySearchTerms(
        string searchParameters
    )
    {
        var headers = _ = new List<string>()
        {
            $"Authorization: Bearer {_ = this.m_accessToken.AccessToken}",
        };
        this.m_serviceGodotHttp.SendHttpRequest(
            url:                     _ = $"{_ = ServiceSpotify.c_uriApi}/search?q={_ = Uri.EscapeDataString(stringToEscape: _ = searchParameters)}&type=track&limit=1",
            headers:                 _ = headers,
            method:                  _ = HttpClient.Method.Get,
            json:                    _ = string.Empty,
            requestCompletedHandler: this.OnRequestTrackSearchCompleted
        );
    }
    
    private void RequestUserAuthorization()
    {
        _ = OS.ShellOpen(
            uri: _ = 
                $"https://accounts.spotify.com/authorize?" +
                $"response_type=code&" +
                $"client_id={_ = this.m_clientId}&" +
                $"scope={_ = Uri.EscapeDataString(stringToEscape: _ = ServiceSpotify.c_userAccessScopes)}&" +
                $"redirect_uri={_ = ServiceSpotify.c_uriRedirect}&" +
                $"state=1&" +
                $"show_dialog=true"
        );
    }
    
    private void RetrieveResources()
    {
        var serviceGodots           = _ = Services.GetService<ServiceGodots>();
        _ = this.m_serviceGodotHttp = _ = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
        
        _ = ServiceDatabaseTaskEvents.RetrievedSpotifyData += this.HandleServiceDatabaseRetrievedSpotifyData;
        
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType: _ = ServiceDatabaseTaskQueryType.RetrieveSpotifyData
        );
    }
}