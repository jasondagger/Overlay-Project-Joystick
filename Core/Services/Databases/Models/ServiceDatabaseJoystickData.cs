
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseJoystickData() :
	ServiceDatabaseModel()
{
	[Key]
	internal int    JoystickData_Id				    { get; set; } = 0;
	
	internal string JoystickData_Authorization_Code { get; set; } = string.Empty;
	internal string JoystickData_Channel_Id  	    { get; set; } = string.Empty;
	internal string JoystickData_Client_Id		    { get; set; } = string.Empty;
	internal string JoystickData_Client_Secret	    { get; set; } = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerId			    = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseJoystickData.JoystickData_Id			     )}"];
		var readerAuthorizationCode = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseJoystickData.JoystickData_Authorization_Code )}"];
		var readerChannelId			= (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseJoystickData.JoystickData_Channel_Id	     )}"];
		var readerClientId			= (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseJoystickData.JoystickData_Client_Id	         )}"];
		var readerClientSecret		= (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseJoystickData.JoystickData_Client_Secret      )}"];
		
		this.JoystickData_Id				 = readerId;
		this.JoystickData_Authorization_Code = readerAuthorizationCode;
		this.JoystickData_Channel_Id		 = readerChannelId;
		this.JoystickData_Client_Id			 = readerClientId;
		this.JoystickData_Client_Secret		 = readerClientSecret;
	}
}