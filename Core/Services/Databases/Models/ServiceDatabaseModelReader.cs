
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Databases.Models;

internal static class ServiceDatabaseModelReader
{
	static ServiceDatabaseModelReader()
	{
		
	}

	internal static async Task<List<TServiceDatabaseModel>> ReadServiceDatabaseModelsFromSqlDataReaderAsync<TServiceDatabaseModel>(
		NpgsqlDataReader npgsqlDataReader
	) where TServiceDatabaseModel :
		ServiceDatabaseModel,
		new()
	{
		var serviceDatabaseModels = _ = new List<TServiceDatabaseModel>();

		while (true)
		{
			var hasRecordAvailable = _ = await npgsqlDataReader.ReadAsync();
			if (_ = hasRecordAvailable is false)
			{
				break;
			}

			serviceDatabaseModels.Add(
				item: _ = ServiceDatabaseModelReader.CreateServiceDatabaseModelFromSqlDataReader<TServiceDatabaseModel>(
					npgsqlDataReader: _ = npgsqlDataReader
				)
			);
		}

		return _ = serviceDatabaseModels;
	}

	internal static TServiceDatabaseModel ReadServiceDatabaseModelFromSqlDataReader<TServiceDatabaseModel>(
		NpgsqlDataReader npgsqlDataReader
	) where TServiceDatabaseModel :
		ServiceDatabaseModel,
		new()
	{
		var hasRecordAvailable = _ = npgsqlDataReader.Read();
		if (_ = hasRecordAvailable is true)
		{
			return _ = ServiceDatabaseModelReader.CreateServiceDatabaseModelFromSqlDataReader<TServiceDatabaseModel>(
				npgsqlDataReader: _ = npgsqlDataReader
			);
		}

		return null;
	}

	private static TServiceDatabaseModel CreateServiceDatabaseModelFromSqlDataReader<TServiceDatabaseModel>(
		NpgsqlDataReader npgsqlDataReader
	) where TServiceDatabaseModel :
		ServiceDatabaseModel,
		new()
	{
		var serviceDatabaseModel = _ = new TServiceDatabaseModel();

		serviceDatabaseModel.CreateFromNpgsqlDataReader(
			npgsqlDataReader: _ = npgsqlDataReader
		);

		return _ = serviceDatabaseModel;
	}
}