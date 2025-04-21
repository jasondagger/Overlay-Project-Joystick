
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseJoystickData() :
	ServiceDatabaseModel()
{
	[Key]
	internal int    JoystickData_Id				   { get; set; } = _ = 0;
	
	internal string JoystickData_AuthorizationCode { get; set; } = _ = string.Empty;
	internal string JoystickData_ChannelId  	   { get; set; } = _ = string.Empty;
	internal string JoystickData_ClientId		   { get; set; } = _ = string.Empty;
	internal string JoystickData_ClientSecret	   { get; set; } = _ = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerId			    = _ = (int)    npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickData.JoystickData_Id			    )}"];
		var readerAuthorizationCode = _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickData.JoystickData_AuthorizationCode )}"];
		var readerChannelId			= _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickData.JoystickData_ChannelId	        )}"];
		var readerClientId			= _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickData.JoystickData_ClientId	        )}"];
		var readerClientSecret		= _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickData.JoystickData_ClientSecret      )}"];
		
		_ = this.JoystickData_Id				= _ = readerId;
		_ = this.JoystickData_AuthorizationCode	= _ = readerAuthorizationCode;
		_ = this.JoystickData_ChannelId			= _ = readerChannelId;
		_ = this.JoystickData_ClientId			= _ = readerClientId;
		_ = this.JoystickData_ClientSecret		= _ = readerClientSecret;
	}
}