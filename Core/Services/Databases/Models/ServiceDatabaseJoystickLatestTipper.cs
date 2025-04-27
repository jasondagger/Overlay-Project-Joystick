
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseJoystickLatestTipper() :
	ServiceDatabaseModel()
{
	[Key]
	internal int    JoystickLatest_Id			 { get; set; } = _ = 0;
	
	internal string JoystickLatest_Latest_Tipper { get; set; } = _ = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerId	       = _ = (int)    npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickLatestTipper.JoystickLatest_Id            )}"];
		var readerLatestTipper = _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickLatestTipper.JoystickLatest_Latest_Tipper )}"];

		_ = this.JoystickLatest_Id            = _ = readerId;
		_ = this.JoystickLatest_Latest_Tipper = _ = readerLatestTipper;
	}
}