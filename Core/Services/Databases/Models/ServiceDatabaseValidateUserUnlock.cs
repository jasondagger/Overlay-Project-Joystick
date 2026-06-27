
using Npgsql;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseValidateUserUnlock() :
	ServiceDatabaseModel()
{
	internal bool   IsValid  { get; private set; } = false;
	internal string Username { get; private set; } = string.Empty;
	internal int    Id       { get; private set; } = 0;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerIsValid  = (bool)   npgsqlDataReader[name: nameof( ServiceDatabaseValidateUserUnlock.IsValid  )];
		var readerUsername = (string) npgsqlDataReader[name: nameof( ServiceDatabaseValidateUserUnlock.Username )];
		var readerId       = (int)    npgsqlDataReader[name: nameof( ServiceDatabaseValidateUserUnlock.Id       )];

		this.IsValid  = readerIsValid;
		this.Username = readerUsername ?? string.Empty;
		this.Id       = readerId;
	}
}