
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseJoystickLatestFollower() :
	ServiceDatabaseModel()
{
	internal string JoystickLatest_Latest_Follower { get; set; } = _ = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerLatestFollower = _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickLatestFollower.JoystickLatest_Latest_Follower )}"];

		_ = this.JoystickLatest_Latest_Follower = _ = readerLatestFollower;
	}
}