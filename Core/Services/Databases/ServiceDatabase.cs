
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
		return _ = Task.CompletedTask;
	}

	Task IService.Start()
	{
		ServiceDatabase.ExecuteTaskQuery(
			serviceDatabaseTaskQueryType: _ = ServiceDatabaseTaskQueryType.Start
		);
		return _ = Task.CompletedTask;
	}

	Task IService.Stop()
	{
		return _ = Task.CompletedTask;
	}

	internal static void ExecuteTaskNonQuery(
		ServiceDatabaseTaskNonQueryType          serviceDatabaseTaskNonQueryType,
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskSqlParameters
	)
	{
		_ = Task.Run(
			function: async () =>
			{
				await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
					serviceDatabaseTaskNonQueryType:  _ = serviceDatabaseTaskNonQueryType,
					serviceDatabaseTaskSqlParameters: _ = serviceDatabaseTaskSqlParameters
				);
			}
		);
	}

	internal static void ExecuteTaskQuery(
		ServiceDatabaseTaskQueryType serviceDatabaseTaskQueryType
	)
	{
		_ = Task.Run(
			function: async () =>
			{
				await ServiceDatabaseTaskQueries.ExecuteAsyncQuery(
					serviceDatabaseTaskQueryType: _ = serviceDatabaseTaskQueryType
				);
			}
		);
	}

	private static void TestConnection()
	{
		_ = Task.Run(
			function: async () =>
			{
				var isConnected = _ = await ServiceDatabaseTaskLogic.TestConnection();
				if (_ = isConnected is false)
				{
					throw _ = new Exception(
						message: _ =
							$"{_ = nameof(ServiceDatabase)}." +
							$"{_ = nameof(ServiceDatabase.TestConnection)}() - " +
							$"EXCEPTION: {_ = nameof(ServiceDatabase)} is not connected."
					);
				}
			}
		);
	}
}