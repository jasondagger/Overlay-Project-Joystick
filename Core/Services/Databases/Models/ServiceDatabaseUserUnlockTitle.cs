
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseUserUnlockTitle() :
	ServiceDatabaseModel()
{
	[Key]
	internal int    UserUnlockTitles_Id      { get; set; } = 0;
	internal string UserUnlockTitles_Username { get; set; } = string.Empty;
	internal int    UserUnlockTitles_Title_Id { get; set; } = 0;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerId       = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Id       )}"];
		var readerUsername = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Username )}"];
		var readerTitleId  = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Title_Id )}"];

		this.UserUnlockTitles_Id       = readerId;
		this.UserUnlockTitles_Username = readerUsername;
		this.UserUnlockTitles_Title_Id = readerTitleId;
	}
}