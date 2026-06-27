
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseUserUnlockModel() :
	ServiceDatabaseModel()
{
	[Key]
	internal int    UserUnlockModels_Id       { get; set; } = 0;
	internal string UserUnlockModels_Username { get; set; } = string.Empty;
	internal int    UserUnlockModels_Model_Id { get; set; } = 0;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerId       = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserUnlockModel.UserUnlockModels_Id       )}"];
		var readerUsername = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserUnlockModel.UserUnlockModels_Username )}"];
		var readerModelId  = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserUnlockModel.UserUnlockModels_Model_Id )}"];

		this.UserUnlockModels_Id       = readerId;
		this.UserUnlockModels_Username = readerUsername;
		this.UserUnlockModels_Model_Id = readerModelId;
	}
}