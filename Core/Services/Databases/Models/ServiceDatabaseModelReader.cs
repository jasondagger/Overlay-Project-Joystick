
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
		var serviceDatabaseModels = new List<TServiceDatabaseModel>();

		while (true)
		{
			var hasRecordAvailable = await npgsqlDataReader.ReadAsync();
			if (hasRecordAvailable is false)
			{
				break;
			}

			serviceDatabaseModels.Add(
				item: ServiceDatabaseModelReader.CreateServiceDatabaseModelFromSqlDataReader<TServiceDatabaseModel>(
					npgsqlDataReader: npgsqlDataReader
				)
			);
		}

		return serviceDatabaseModels;
	}

	internal static TServiceDatabaseModel ReadServiceDatabaseModelFromSqlDataReader<TServiceDatabaseModel>(
		NpgsqlDataReader npgsqlDataReader
	) where TServiceDatabaseModel :
		ServiceDatabaseModel,
		new()
	{
		var hasRecordAvailable = npgsqlDataReader.Read();
		if (hasRecordAvailable is true)
		{
			return ServiceDatabaseModelReader.CreateServiceDatabaseModelFromSqlDataReader<TServiceDatabaseModel>(
				npgsqlDataReader: npgsqlDataReader
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
		var serviceDatabaseModel = new TServiceDatabaseModel();

		serviceDatabaseModel.CreateFromNpgsqlDataReader(
			npgsqlDataReader: npgsqlDataReader
		);

		return serviceDatabaseModel;
	}
}