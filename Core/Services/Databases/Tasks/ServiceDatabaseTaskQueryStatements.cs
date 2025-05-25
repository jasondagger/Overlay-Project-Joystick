
namespace Overlay.Core.Services.Databases.Tasks;

internal static class ServiceDatabaseTaskQueryStatements
{
	static ServiceDatabaseTaskQueryStatements()
	{
		
	}
	
	internal const string RetrieveGoogleData                     = $"SELECT * FROM GoogleData LIMIT 1";
	
	internal const string RetrieveGoveeData                     = $"SELECT * FROM GoveeData LIMIT 1";
	internal const string RetrieveListGoveeLights               = $"SELECT * FROM GoveeLights";
	
	internal const string RetrieveJoystickData                  = $"SELECT * FROM JoystickData LIMIT 1";
	internal const string RetrieveListJoystickLatestFollowers   = $"SELECT * FROM JoystickLatestFollower";
	internal const string RetrieveListJoystickLatestSubscribers = $"SELECT * FROM JoystickLatestSubscriber";
	internal const string RetrieveListJoystickLatestTippers     = $"SELECT * FROM JoystickLatestTipper";
	internal const string RetrieveListJoystickUsers             = $"SELECT * FROM JoystickUsers";
	
	internal const string RetrieveLovenseData                   = $"SELECT * FROM LovenseData LIMIT 1";
	
	internal const string RetrieveSpotifyData                   = $"SELECT * FROM SpotifyData LIMIT 1";
}