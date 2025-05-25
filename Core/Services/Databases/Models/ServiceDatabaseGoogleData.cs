
using Npgsql;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseGoogleData() :
	ServiceDatabaseModel()
{
	internal string GoogleData_Api_Key { get; set; } = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerApiKey = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseGoogleData.GoogleData_Api_Key )}"];
		
		this.GoogleData_Api_Key = readerApiKey;
	}
}