
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Databases.Tasks;

internal static class ServiceDatabaseTaskQueries
{
    static ServiceDatabaseTaskQueries()
    {

    }

    internal static async Task ExecuteAsyncQuery(
        ServiceDatabaseTaskQueryType serviceDatabaseTaskQueryType
    )
    {
        await ServiceDatabaseTaskQueries.c_taskQueries[
			key: _ = serviceDatabaseTaskQueryType
		].Invoke();
    }

    private static readonly Dictionary<ServiceDatabaseTaskQueryType, Func<Task>> c_taskQueries = new()
    {
        {
			_ = ServiceDatabaseTaskQueryType.Start,
			ServiceDatabaseTaskQueries.Start
		},
		{
			_ = ServiceDatabaseTaskQueryType.RetrieveJoystickData,
			ServiceDatabaseTaskQueries.RetrieveAsyncJoystickData
		},
        {
			_ = ServiceDatabaseTaskQueryType.RetrieveListJoystickUsers,
			ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickUsers
		},
	};
    
    private static async Task RetrieveAsyncJoystickData()
    {
	    var npgsqlStatement = _ = ServiceDatabaseTaskQueryStatements.RetrieveJoystickData;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  _ = npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedJoystickData
	    );
    }

	private static async Task RetrieveAsyncListJoystickUsers()
	{
		var npgsqlStatement = _ = ServiceDatabaseTaskQueryStatements.RetrieveListJoystickUsers;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		  _ = npgsqlStatement,
			executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickUsers
		);
	}

	private static async Task Start()
    {
	    await ServiceDatabaseTaskQueries.RetrieveAsyncJoystickData();
	    await ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickUsers();
	}
};