
using Overlay.Core.Services.Databases.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Databases;

internal sealed class ServiceDatabase() :
	IService
{
	Task IService.Setup()
	{
		ServiceDatabase.TestConnection();
		ServiceDatabaseBankTaskHandler.Start();
		return Task.CompletedTask;
	}

	Task IService.Start()
	{
		ServiceDatabase.ExecuteTaskQuery(
			serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.Start
		);
		return Task.CompletedTask;
	}

	Task IService.Stop()
	{
		return Task.CompletedTask;
	}

	internal static void ExecuteTaskNonQuery(
		ServiceDatabaseTaskNonQueryType          serviceDatabaseTaskNonQueryType,
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskSqlParameters
	)
	{
		Task.Run(
			function: async () =>
			{
				await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
					serviceDatabaseTaskNonQueryType:  serviceDatabaseTaskNonQueryType,
					serviceDatabaseTaskSqlParameters: serviceDatabaseTaskSqlParameters
				);
			}
		);
	}

	internal static void ExecuteTaskQuery(
		ServiceDatabaseTaskQueryType             serviceDatabaseTaskQueryType,
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters = null
	)
	{
		Task.Run(
			function: async () =>
			{
				await ServiceDatabaseTaskQueries.ExecuteAsyncQuery(
					serviceDatabaseTaskQueryType:        serviceDatabaseTaskQueryType,
					serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
				);
			}
		);
	}

	private static void TestConnection()
	{
		Task.Run(
			function: async () =>
			{
				var isConnected = await ServiceDatabaseTaskLogic.TestConnection();
				if (isConnected is false)
				{
					throw new Exception(
						message:
							$"{nameof(ServiceDatabase)}." +
							$"{nameof(ServiceDatabase.TestConnection)}() - " +
							$"EXCEPTION: {nameof(ServiceDatabase)} is not connected."
					);
				}
			}
		);
	}
}