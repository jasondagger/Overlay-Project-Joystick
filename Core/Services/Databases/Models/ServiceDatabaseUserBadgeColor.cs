
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseUserBadgeColor() :
	ServiceDatabaseModel()
{
	[Key]
	internal string UserBadgeColors_Username { get; set; } = string.Empty;
	internal int    UserBadgeColors_Color_Id { get; set; } = 0;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerUsername = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserBadgeColor.UserBadgeColors_Username )}"];
		var readerColorId  = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserBadgeColor.UserBadgeColors_Color_Id )}"];

		this.UserBadgeColors_Username = readerUsername;
		this.UserBadgeColors_Color_Id = readerColorId;
	}
}