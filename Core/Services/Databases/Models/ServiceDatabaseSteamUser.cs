
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseSteamUser() :
	ServiceDatabaseModel()
{
	[Key]
	internal string SteamUser_Joystick_Username { get; set; } = string.Empty;
	internal string SteamUser_Steam_Username    { get; set; } = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerJoystickUsername = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseSteamUser.SteamUser_Joystick_Username )}"];
		var readerSteamUsername    = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseSteamUser.SteamUser_Steam_Username    )}"];

		this.SteamUser_Joystick_Username = readerJoystickUsername;
		this.SteamUser_Steam_Username    = readerSteamUsername;
	}
}