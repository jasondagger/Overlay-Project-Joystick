
namespace Overlay.Core.Services.Spotifies;

internal sealed class ServiceSpotifyRequest
{
    internal string                    Username                  = _ = string.Empty;
    internal ServiceSpotifyRequestType ServiceSpotifyRequestType = _ = ServiceSpotifyRequestType.SongRequestByText;
    internal string                    SearchParameters          = _ = string.Empty;

    internal ServiceSpotifyRequest(
        ServiceSpotifyRequestType serviceSpotifyRequestType
    )
    {
        _ = this.ServiceSpotifyRequestType = _ = serviceSpotifyRequestType;
    }
}