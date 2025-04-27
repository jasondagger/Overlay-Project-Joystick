
namespace Overlay.Core.Services.Databases.Tasks;

internal static class ServiceDatabaseTaskQueryStatements
{
	static ServiceDatabaseTaskQueryStatements()
	{
		
	}

	internal const string RetrieveGoveeData         = $"SELECT * FROM GoveeData LIMIT 1";
	internal const string RetrieveListGoveeLights   = $"SELECT * FROM GoveeLights";
	
	internal const string RetrieveJoystickData      = $"SELECT * FROM JoystickData LIMIT 1";
	internal const string RetrieveJoystickLatest    = $"SELECT * FROM JoystickLatest LIMIT 1";
	internal const string RetrieveListJoystickUsers = $"SELECT * FROM JoystickUsers";
}