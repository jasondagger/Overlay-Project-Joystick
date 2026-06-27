
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseBankUserTip() :
	ServiceDatabaseModel()
{
	[Key]
	internal string BankUser_Joystick_Username                                   { get; set; } = string.Empty;
	internal int    BankUser_Joystick_Current_Tip_Total_For_Tip_Threshold        { get; set; } = 0;
	internal int    BankUser_Joystick_Tip_Threshold_For_Gush_Control_Link_Minute { get; set; } = 0;
	internal int    BankUser_Joystick_Tip_Total                                  { get; set; } = 0;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerJoystickUsername                             = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseBankUserTip.BankUser_Joystick_Username                                   )}"];
		var readerJoystickCurrentTipTotalForTipThreshold       = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseBankUserTip.BankUser_Joystick_Current_Tip_Total_For_Tip_Threshold        )}"];
		var readerJoystickTipThresholdForGushControlLinkMinute = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseBankUserTip.BankUser_Joystick_Tip_Threshold_For_Gush_Control_Link_Minute )}"];
		var readerJoystickTipTotal                             = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseBankUserTip.BankUser_Joystick_Tip_Total                                  )}"];

		this.BankUser_Joystick_Username                                   = readerJoystickUsername;
		this.BankUser_Joystick_Current_Tip_Total_For_Tip_Threshold        = readerJoystickCurrentTipTotalForTipThreshold;
		this.BankUser_Joystick_Tip_Threshold_For_Gush_Control_Link_Minute = readerJoystickTipThresholdForGushControlLinkMinute;
		this.BankUser_Joystick_Tip_Total                                  = readerJoystickTipTotal;
	}
}