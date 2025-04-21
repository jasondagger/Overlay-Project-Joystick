
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseJoystickLatest() :
	ServiceDatabaseModel()
{
	[Key]
	internal int    JoystickLatest_Id			   { get; set; } = _ = 0;
	
	internal string JoystickLatest_Latest_Follower     { get; set; } = _ = string.Empty;
	internal string JoystickLatest_Latest_Subscriber { get; set; } = _ = string.Empty;
	internal string JoystickLatest_Latest_Tipper     { get; set; } = _ = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerId	           = _ = (int)    npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickLatest.JoystickLatest_Id                )}"];
		var readerLatestFollower   = _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickLatest.JoystickLatest_Latest_Follower   )}"];
		var readerLatestSubscriber = _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickLatest.JoystickLatest_Latest_Subscriber )}"];
		var readerLatestTipper	   = _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickLatest.JoystickLatest_Latest_Tipper     )}"];

		_ = this.JoystickLatest_Id                = _ = readerId;
		_ = this.JoystickLatest_Latest_Follower   = _ = readerLatestFollower;
		_ = this.JoystickLatest_Latest_Subscriber = _ = readerLatestSubscriber;
		_ = this.JoystickLatest_Latest_Tipper     = _ = readerLatestTipper;
	}
}