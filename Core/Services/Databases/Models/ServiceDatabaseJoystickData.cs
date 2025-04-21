
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseJoystickData() :
	ServiceDatabaseModel()
{
	[Key]
	internal int    JoystickData_Id				    { get; set; } = _ = 0;
	
	internal string JoystickData_Authorization_Code { get; set; } = _ = string.Empty;
	internal string JoystickData_Channel_Id  	    { get; set; } = _ = string.Empty;
	internal string JoystickData_Client_Id		    { get; set; } = _ = string.Empty;
	internal string JoystickData_Client_Secret	    { get; set; } = _ = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerId			    = _ = (int)    npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickData.JoystickData_Id			     )}"];
		var readerAuthorizationCode = _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickData.JoystickData_Authorization_Code )}"];
		var readerChannelId			= _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickData.JoystickData_Channel_Id	     )}"];
		var readerClientId			= _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickData.JoystickData_Client_Id	         )}"];
		var readerClientSecret		= _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickData.JoystickData_Client_Secret      )}"];
		
		_ = this.JoystickData_Id				 = _ = readerId;
		_ = this.JoystickData_Authorization_Code = _ = readerAuthorizationCode;
		_ = this.JoystickData_Channel_Id		 = _ = readerChannelId;
		_ = this.JoystickData_Client_Id			 = _ = readerClientId;
		_ = this.JoystickData_Client_Secret		 = _ = readerClientSecret;
	}
}