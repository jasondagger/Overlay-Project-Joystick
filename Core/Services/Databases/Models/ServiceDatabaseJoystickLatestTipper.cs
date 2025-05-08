
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseJoystickLatestTipper() :
	ServiceDatabaseModel()
{
	internal string JoystickLatest_Latest_Tipper { get; set; } = _ = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerLatestTipper = _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickLatestTipper.JoystickLatest_Latest_Tipper )}"];

		_ = this.JoystickLatest_Latest_Tipper = _ = readerLatestTipper;
	}
}