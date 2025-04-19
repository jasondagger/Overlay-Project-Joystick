
namespace Overlay.Core.Services.Databases.Tasks;

internal static class ServiceDatabaseTaskQueryStatements
{
	static ServiceDatabaseTaskQueryStatements()
	{
		
	}

	internal const string RetrieveJoystickData      = $"SELECT * FROM JoystickData LIMIT 1";
	internal const string RetrieveListJoystickUsers = $"SELECT * FROM JoystickUsers";
}