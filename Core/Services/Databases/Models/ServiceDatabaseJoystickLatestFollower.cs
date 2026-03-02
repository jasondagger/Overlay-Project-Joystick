
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseJoystickLatestFollower() :
	ServiceDatabaseModel()
{
	internal string JoystickLatest_Latest_Follower { get; set; } = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerLatestFollower = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseJoystickLatestFollower.JoystickLatest_Latest_Follower )}"];

		this.JoystickLatest_Latest_Follower = readerLatestFollower;
	}
}