
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseValidateUserHas() :
	ServiceDatabaseModel()
{
	internal bool   IsValid  { get; private set; } = false;
	internal string Username { get; private set; } = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerIsValid  = (bool)   npgsqlDataReader[name: nameof( ServiceDatabaseValidateUserHas.IsValid  )];
		var readerUsername = (string) npgsqlDataReader[name: nameof( ServiceDatabaseValidateUserHas.Username )];

		this.IsValid  = readerIsValid;
		this.Username = readerUsername ?? string.Empty;
	}
}