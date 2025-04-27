
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
			_ = ServiceDatabaseTaskQueryType.RetrieveGoveeData,
			ServiceDatabaseTaskQueries.RetrieveAsyncGoveeData
		},
		{
			_ = ServiceDatabaseTaskQueryType.RetrieveListGoveeLights,
			ServiceDatabaseTaskQueries.RetrieveAsyncListGoveeLights
		},
		{
			_ = ServiceDatabaseTaskQueryType.RetrieveJoystickData,
			ServiceDatabaseTaskQueries.RetrieveAsyncJoystickData
		},
		{
			_ = ServiceDatabaseTaskQueryType.RetrieveJoystickLatest,
			ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickLatest
		},
        {
			_ = ServiceDatabaseTaskQueryType.RetrieveListJoystickUsers,
			ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickUsers
		},
	};
    
    private static async Task RetrieveAsyncGoveeData()
    {
	    var npgsqlStatement = _ = ServiceDatabaseTaskQueryStatements.RetrieveGoveeData;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  _ = npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedGoveeData
	    );
    }
    
    private static async Task RetrieveAsyncListGoveeLights()
    {
	    var npgsqlStatement = _ = ServiceDatabaseTaskQueryStatements.RetrieveListGoveeLights;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  _ = npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListGoveeLights
	    );
    }
    
    private static async Task RetrieveAsyncJoystickData()
    {
	    var npgsqlStatement = _ = ServiceDatabaseTaskQueryStatements.RetrieveJoystickData;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  _ = npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedJoystickData
	    );
    }
    
    private static async Task RetrieveAsyncListJoystickLatest()
    {
	    var npgsqlStatement = _ = ServiceDatabaseTaskQueryStatements.RetrieveJoystickLatest;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  _ = npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedJoystickLatest
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
		await ServiceDatabaseTaskQueries.RetrieveAsyncGoveeData();
		await ServiceDatabaseTaskQueries.RetrieveAsyncListGoveeLights();
		
	    await ServiceDatabaseTaskQueries.RetrieveAsyncJoystickData();
	    await ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickLatest();
	    await ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickUsers();
	}
};