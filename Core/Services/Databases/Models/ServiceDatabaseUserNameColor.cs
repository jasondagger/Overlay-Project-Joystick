
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseUserNameColor() :
	ServiceDatabaseModel()
{
	[Key]
	internal string UserNameColors_Username { get; set; } = string.Empty;
	internal int    UserNameColors_Color_Id { get; set; } = 0;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerUsername = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserNameColor.UserNameColors_Username )}"];
		var readerColorId  = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserNameColor.UserNameColors_Color_Id )}"];

		this.UserNameColors_Username = readerUsername;
		this.UserNameColors_Color_Id = readerColorId;
	}
}