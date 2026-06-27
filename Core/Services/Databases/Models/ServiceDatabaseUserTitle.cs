
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseUserTitle() :
	ServiceDatabaseModel()
{
	[Key]
	internal string UserTitles_Username { get; set; } = string.Empty;
	internal int    UserTitles_Title_Id { get; set; } = 0;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerUsername = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserTitle.UserTitles_Username )}"];
		var readerTitleId  = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserTitle.UserTitles_Title_Id )}"];

		this.UserTitles_Username = readerUsername;
		this.UserTitles_Title_Id = readerTitleId;
	}
}