
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseBankUserTimeLimit() :
	ServiceDatabaseModel()
{
	[Key]
	internal string BankUser_Joystick_Username                               { get; set; } = string.Empty;
	internal int    BankUser_Lovense_Gush_Control_Link_Time_Limit_In_Minutes { get; set; } = 0;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerJoystickUsername                         = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseBankUser.BankUser_Joystick_Username                               )}"];
		var readerLovenseGushControlLinkTimeLimitInMinutes = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseBankUser.BankUser_Lovense_Gush_Control_Link_Time_Limit_In_Minutes )}"];

		this.BankUser_Joystick_Username                               = readerJoystickUsername;
		this.BankUser_Lovense_Gush_Control_Link_Time_Limit_In_Minutes = readerLovenseGushControlLinkTimeLimitInMinutes;
	}
}