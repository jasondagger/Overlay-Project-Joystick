
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
			_ = ServiceDatabaseTaskQueryType.RetrieveListJoystickLatestFollowers,
			ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickLatestFollowers
		},
		{
			_ = ServiceDatabaseTaskQueryType.RetrieveListJoystickLatestSubscribers,
			ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickLatestSubscribers
		},
		{
			_ = ServiceDatabaseTaskQueryType.RetrieveListJoystickLatestTippers,
			ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickLatestTippers
		},
        {
			_ = ServiceDatabaseTaskQueryType.RetrieveListJoystickUsers,
			ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickUsers
		},
		{
			_ = ServiceDatabaseTaskQueryType.RetrieveLovenseData,
			ServiceDatabaseTaskQueries.RetrieveAsyncLovenseData
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
    
    private static async Task RetrieveAsyncJoystickData()
    {
	    var npgsqlStatement = _ = ServiceDatabaseTaskQueryStatements.RetrieveJoystickData;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  _ = npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedJoystickData
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
    
    private static async Task RetrieveAsyncListJoystickLatestFollowers()
    {
	    var npgsqlStatement = _ = ServiceDatabaseTaskQueryStatements.RetrieveListJoystickLatestFollowers;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  _ = npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickLatestFollowers
	    );
    }
    
    private static async Task RetrieveAsyncListJoystickLatestSubscribers()
    {
	    var npgsqlStatement = _ = ServiceDatabaseTaskQueryStatements.RetrieveListJoystickLatestSubscribers;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  _ = npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickLatestSubscribers
	    );
    }
    
    private static async Task RetrieveAsyncListJoystickLatestTippers()
    {
	    var npgsqlStatement = _ = ServiceDatabaseTaskQueryStatements.RetrieveListJoystickLatestTippers;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  _ = npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickLatestTippers
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
	
	private static async Task RetrieveAsyncLovenseData()
	{
		var npgsqlStatement = _ = ServiceDatabaseTaskQueryStatements.RetrieveLovenseData;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		  _ = npgsqlStatement,
			executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedLovenseData
		);
	}

	private static async Task Start()
	{
		await ServiceDatabaseTaskQueries.RetrieveAsyncGoveeData();
		await ServiceDatabaseTaskQueries.RetrieveAsyncListGoveeLights();
		
	    await ServiceDatabaseTaskQueries.RetrieveAsyncJoystickData();
	    await ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickUsers();

	    await ServiceDatabaseTaskQueries.RetrieveAsyncLovenseData();
	}
};