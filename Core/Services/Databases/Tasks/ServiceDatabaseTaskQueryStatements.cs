
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks;

internal static class ServiceDatabaseTaskQueryStatements
{
	static ServiceDatabaseTaskQueryStatements()
	{
		
	}
	
	internal const string           RetrieveGoveeData                     = $"SELECT * FROM GoveeData LIMIT 1";
	internal const string           RetrieveListGoveeLights               = $"SELECT * FROM GoveeLights";
	
	internal const string           RetrieveJoystickData                  = $"SELECT * FROM JoystickData LIMIT 1";
	internal static readonly string RetrieveListJoystickLatestFollowers   = $"SELECT * FROM JoystickLatestFollower ORDER BY {_ = nameof(ServiceDatabaseJoystickLatestFollower.JoystickLatest_Id)} DESC LIMIT 5";
	internal static readonly string RetrieveListJoystickLatestSubscribers = $"SELECT * FROM JoystickLatestSubscriber ORDER BY {_ = nameof(ServiceDatabaseJoystickLatestSubscriber.JoystickLatest_Id)} DESC LIMIT 5";
	internal static readonly string RetrieveListJoystickLatestTippers     = $"SELECT * FROM JoystickLatestTipper ORDER BY {_ = nameof(ServiceDatabaseJoystickLatestTipper.JoystickLatest_Id)} DESC LIMIT 5";
	internal const string           RetrieveListJoystickUsers             = $"SELECT * FROM JoystickUsers";
	
	internal const string           RetrieveLovenseData                   = $"SELECT * FROM LovenseData LIMIT 1";
	
	internal const string           RetrieveSpotifyData                   = $"SELECT * FROM SpotifyData LIMIT 1";
}