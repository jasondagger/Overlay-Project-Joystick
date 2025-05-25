
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
        await ServiceDatabaseTaskQueries.s_taskQueries[
			key: serviceDatabaseTaskQueryType
		].Invoke();
    }

    private static readonly Dictionary<ServiceDatabaseTaskQueryType, Func<Task>> s_taskQueries = new()
    {
        {
			ServiceDatabaseTaskQueryType.Start,
			ServiceDatabaseTaskQueries.Start
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveGoogleData,
			ServiceDatabaseTaskQueries.RetrieveAsyncGoogleData
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveGoveeData,
			ServiceDatabaseTaskQueries.RetrieveAsyncGoveeData
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveListGoveeLights,
			ServiceDatabaseTaskQueries.RetrieveAsyncListGoveeLights
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveJoystickData,
			ServiceDatabaseTaskQueries.RetrieveAsyncJoystickData
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveListJoystickLatestFollowers,
			ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickLatestFollowers
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveListJoystickLatestSubscribers,
			ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickLatestSubscribers
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveListJoystickLatestTippers,
			ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickLatestTippers
		},
        {
			ServiceDatabaseTaskQueryType.RetrieveListJoystickUsers,
			ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickUsers
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveLovenseData,
			ServiceDatabaseTaskQueries.RetrieveAsyncLovenseData
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveSpotifyData,
			ServiceDatabaseTaskQueries.RetrieveAsyncSpotifyData
		},
	};
    
    private static async Task RetrieveAsyncGoogleData()
    {
	    const string npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveGoogleData;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedGoogleData
	    );
    }
    
    private static async Task RetrieveAsyncGoveeData()
    {
	    const string npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveGoveeData;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedGoveeData
	    );
    }
    
    private static async Task RetrieveAsyncJoystickData()
    {
	    const string npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveJoystickData;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedJoystickData
	    );
    }
    
    private static async Task RetrieveAsyncListGoveeLights()
    {
	    const string npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveListGoveeLights;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListGoveeLights
	    );
    }
    
    private static async Task RetrieveAsyncListJoystickLatestFollowers()
    {
	    const string npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveListJoystickLatestFollowers;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickLatestFollowers
	    );
    }
    
    private static async Task RetrieveAsyncListJoystickLatestSubscribers()
    {
	    const string npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveListJoystickLatestSubscribers;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickLatestSubscribers
	    );
    }
    
    private static async Task RetrieveAsyncListJoystickLatestTippers()
    {
	    const string npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveListJoystickLatestTippers;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickLatestTippers
	    );
    }

	private static async Task RetrieveAsyncListJoystickUsers()
	{
		const string npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveListJoystickUsers;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		  npgsqlStatement,
			executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickUsers
		);
	}
	
	private static async Task RetrieveAsyncLovenseData()
	{
		const string npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveLovenseData;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		  npgsqlStatement,
			executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedLovenseData
		);
	}
	
	private static async Task RetrieveAsyncSpotifyData()
	{
		const string npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveSpotifyData;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		  npgsqlStatement,
			executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedSpotifyData
		);
	}

	private static async Task Start()
	{
		await ServiceDatabaseTaskQueries.RetrieveAsyncGoogleData();
		
		await ServiceDatabaseTaskQueries.RetrieveAsyncGoveeData();
		await ServiceDatabaseTaskQueries.RetrieveAsyncListGoveeLights();
		
	    await ServiceDatabaseTaskQueries.RetrieveAsyncJoystickData();
	    await ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickUsers();

	    await ServiceDatabaseTaskQueries.RetrieveAsyncLovenseData();
	}
};