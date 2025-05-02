
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

namespace Overlay.Core.Services.Spotifies;

public sealed partial class ServiceSpotify : 
    IService
{
    Task IService.Setup()
    {
        this.RetrieveResources();
        this.RequestAccessToken();
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

    private const string                 c_authorizationCode = "AQAL6RpysdNnTXyrlNT1Au2qTiLdM3sFKbkKMZ62T1nBZAqrBPV1Bypx3OWoWLKj54oZCRByJUnF6R8hAj1VpkFLYH_zCHsAQblaay4J-u1djDz1XMQxBB7uYCoLzlH3kBRCppHeZ5FzOiy_QPQvFqaD1uefqlWv6RBG95oIZi3xl1PJiZTIJXfDCanikerfaCA55aNUH3vP1FaxDqILjHrp-oL3BQ-op82_y2Y8gWTlESVkTzGVz2opzOBkVlPlADfq-ZiPb3t2s1Y4MxE4GeSyWY9B";
    private const string                 c_uriAccessToken    = "https://accounts.spotify.com/api/token";
    private const string                 c_uriApi            = "https://api.spotify.com/v1";
    private const string                 c_uriRedirect       = "http://127.0.0.1:8888/callback";
    private const string                 c_userAccessScopes  = "user-modify-playback-state user-read-currently-playing user-read-playback-state";

    private Queue<ServiceSpotifyRequest> m_requestQueue      = new();
    private ServiceGodotHttp             m_serviceGodotHttp  = null;
    private SpotifyResponseAccessToken   m_accessToken       = null;
    private string                       m_clientId          = _ = "a5e70c13f82c4f2ea0babe2177759985";
    private string                       m_clientSecret      = _ = "2760fb2829d540fea372a2868b03b501";
    
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
    }
}