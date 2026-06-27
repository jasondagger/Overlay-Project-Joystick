
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseUserUnlocks() :
	ServiceDatabaseModel()
{
	internal string Username  { get; set; } = string.Empty;
	internal int[]  UnlockIds { get; set; } = null;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerUsername  = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserUnlocks.Username )}"];
		var readerUnlockIds = (int[])  npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserUnlocks.UnlockIds )}"];

		this.Username  = readerUsername;
		this.UnlockIds = readerUnlockIds;
	}
}