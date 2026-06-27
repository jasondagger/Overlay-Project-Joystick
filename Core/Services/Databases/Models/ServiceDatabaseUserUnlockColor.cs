
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseUserUnlockColor() :
	ServiceDatabaseModel()
{
	[Key]
	internal int    UserUnlockColors_Id       { get; set; } = 0;
	internal string UserUnlockColors_Username { get; set; } = string.Empty;
	internal int    UserUnlockColors_Color_Id { get; set; } = 0;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerId       = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserUnlockColor.UserUnlockColors_Id       )}"];
		var readerUsername = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserUnlockColor.UserUnlockColors_Username )}"];
		var readerColorId  = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserUnlockColor.UserUnlockColors_Color_Id )}"];

		this.UserUnlockColors_Id       = readerId;
		this.UserUnlockColors_Username = readerUsername;
		this.UserUnlockColors_Color_Id = readerColorId;
	}
}