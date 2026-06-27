
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseBankUser() :
	ServiceDatabaseModel()
{
	[Key]
	internal string BankUser_Joystick_Username                                   { get; set; } = string.Empty;
	internal int    BankUser_Lovense_Gush_Control_Link_Time_In_Minutes           { get; set; } = 0;
	internal int    BankUser_Lovense_Gush_Control_Link_Time_Limit_In_Minutes     { get; set; } = 0;
	internal int    BankUser_Joystick_Tip_Total                                  { get; set; } = 0;
	internal int    BankUser_Joystick_Tip_Threshold_For_Gush_Control_Link_Minute { get; set; } = 0;
	internal int    BankUser_Joystick_Current_Tip_Total_For_Tip_Threshold        { get; set; } = 0;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerJoystickUsername                             = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseBankUser.BankUser_Joystick_Username                                   )}"];
		var readerLovenseGushControlLinkTimeInMinutes          = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseBankUser.BankUser_Lovense_Gush_Control_Link_Time_In_Minutes           )}"];
		var readerLovenseGushControlLinkTimeLimitInMinutes     = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseBankUser.BankUser_Lovense_Gush_Control_Link_Time_Limit_In_Minutes     )}"];
		var readerJoystickTipTotal                             = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseBankUser.BankUser_Joystick_Tip_Total                                  )}"];
		var readerJoystickTipThresholdForGushControlLinkMinute = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseBankUser.BankUser_Joystick_Tip_Threshold_For_Gush_Control_Link_Minute )}"];
		var readerJoystickCurrentTipTotalForTipThreshold       = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseBankUser.BankUser_Joystick_Current_Tip_Total_For_Tip_Threshold        )}"];

		this.BankUser_Joystick_Username                                   = readerJoystickUsername;
		this.BankUser_Lovense_Gush_Control_Link_Time_In_Minutes           = readerLovenseGushControlLinkTimeInMinutes;
		this.BankUser_Lovense_Gush_Control_Link_Time_Limit_In_Minutes     = readerLovenseGushControlLinkTimeLimitInMinutes;
		this.BankUser_Joystick_Tip_Total                                  = readerJoystickTipTotal;
		this.BankUser_Joystick_Tip_Threshold_For_Gush_Control_Link_Minute = readerJoystickTipThresholdForGushControlLinkMinute;
		this.BankUser_Joystick_Current_Tip_Total_For_Tip_Threshold        = readerJoystickCurrentTipTotalForTipThreshold;
	}
}