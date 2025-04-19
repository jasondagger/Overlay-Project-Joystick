
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseJoystickUser() :
	ServiceDatabaseModel()
{
	[Key]
	internal int      JoystickUser_Id				   { get; set; } = _ = 0;
	
	internal string   JoystickUser_CustomChatTextColor { get; set; } = _ = string.Empty;
	internal string   JoystickUser_Username            { get; set; } = _ = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerId	               	          = _ = (int)    npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickUser.JoystickUser_Id                  )}"];
		var readerCustomChatTextColor			  = _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickUser.JoystickUser_CustomChatTextColor )}"];
		var readerUsername				          = _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickUser.JoystickUser_Username            )}"];

		_ = this.JoystickUser_Id                  = _ = readerId;
		_ = this.JoystickUser_CustomChatTextColor = _ = readerCustomChatTextColor;
		_ = this.JoystickUser_Username			  = _ = readerUsername;
	}
}