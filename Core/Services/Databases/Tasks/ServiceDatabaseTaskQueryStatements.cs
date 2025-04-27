
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks;

internal static class ServiceDatabaseTaskQueryStatements
{
	static ServiceDatabaseTaskQueryStatements()
	{
		
	}

	internal static readonly string RetrieveGoveeData                     = $"SELECT * FROM GoveeData LIMIT 1";
	internal static readonly string RetrieveListGoveeLights               = $"SELECT * FROM GoveeLights";
	
	internal static readonly string RetrieveJoystickData                  = $"SELECT * FROM JoystickData LIMIT 1";
	internal static readonly string RetrieveListJoystickLatestFollowers   = $"SELECT * FROM JoystickLatestFollower ORDER BY {_ = nameof(ServiceDatabaseJoystickLatestFollower.JoystickLatest_Id)} DESC LIMIT 5";
	internal static readonly string RetrieveListJoystickLatestSubscribers = $"SELECT * FROM JoystickLatestSubscriber ORDER BY {_ = nameof(ServiceDatabaseJoystickLatestSubscriber.JoystickLatest_Id)} DESC LIMIT 5";
	internal static readonly string RetrieveListJoystickLatestTippers     = $"SELECT * FROM JoystickLatestTipper ORDER BY {_ = nameof(ServiceDatabaseJoystickLatestTipper.JoystickLatest_Id)} DESC LIMIT 5";
	internal static readonly string RetrieveListJoystickUsers             = $"SELECT * FROM JoystickUsers";
}