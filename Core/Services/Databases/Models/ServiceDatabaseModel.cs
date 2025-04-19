
using Npgsql;

namespace Overlay.Core.Services.Databases.Models;

internal abstract class ServiceDatabaseModel()
{
	internal abstract void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	);
}