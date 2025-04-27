
using Overlay.Core.Services.Databases.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Databases.Tasks;

internal static class ServiceDatabaseTaskNonQueries
{
    static ServiceDatabaseTaskNonQueries()
    {
        
    }

    internal static async Task ExecuteAsyncNonQuery(
        ServiceDatabaseTaskNonQueryType       serviceDatabaseTaskNonQueryType,
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskSqlParameters
	)
	{
        await ServiceDatabaseTaskNonQueries.c_taskNonQueries[
            key: _ = serviceDatabaseTaskNonQueryType
        ].Invoke(
            arg: _ = serviceDatabaseTaskSqlParameters
        );
    }

    private static readonly Dictionary<ServiceDatabaseTaskNonQueryType, Func<List<ServiceDatabaseTaskNpgsqlParameter>, Task>> c_taskNonQueries = new()
    {
        {
            _ = ServiceDatabaseTaskNonQueryType.AddJoystickUser, 
            ServiceDatabaseTaskNonQueries.AddAsyncJoystickUser
        },
        {
            _ = ServiceDatabaseTaskNonQueryType.AddJoystickLatestFollower, 
            ServiceDatabaseTaskNonQueries.AddJoystickLatestFollowerAsync
        },
        {
            _ = ServiceDatabaseTaskNonQueryType.AddJoystickLatestSubscriber, 
            ServiceDatabaseTaskNonQueries.AddJoystickLatestSubscriberAsync
        },
        {
            _ = ServiceDatabaseTaskNonQueryType.AddJoystickLatestTipper, 
            ServiceDatabaseTaskNonQueries.AddJoystickLatestTipperAsync
        },
	};

    private static async Task AddAsyncJoystickUser(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		var npgsqlStatement = _ = 
            $"INSERT INTO JoystickUsers (" +
            $"{_ = nameof(ServiceDatabaseJoystickUser.JoystickUser_Custom_Chat_Text_Color)}, " +
            $"{_ = nameof(ServiceDatabaseJoystickUser.JoystickUser_Username)}" +
            $") VALUES (" +
            $"@{_ = nameof(ServiceDatabaseJoystickUser.JoystickUser_Custom_Chat_Text_Color)}, " +
            $"@{_ = nameof(ServiceDatabaseJoystickUser.JoystickUser_Username)}" +
            $")";
        
		await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     _ = npgsqlStatement,
            serviceDatabaseTaskNpgsqlParameters: _ = serviceDatabaseTaskNpgsqlParameters
		);
    }
    
    private static async Task AddJoystickLatestFollowerAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        var npgsqlStatement = _ = 
            $"INSERT INTO JoystickLatestFollower (" +
            $"{_ = nameof(ServiceDatabaseJoystickLatestFollower.JoystickLatest_Latest_Follower)}" +
            $") VALUES (" +
            $"@{_ = nameof(ServiceDatabaseJoystickLatestFollower.JoystickLatest_Latest_Follower)}" +
            $")";
        
        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     _ = npgsqlStatement,
            serviceDatabaseTaskNpgsqlParameters: _ = serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task AddJoystickLatestSubscriberAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        var npgsqlStatement = _ = 
            $"INSERT INTO JoystickLatestSubscriber (" +
            $"{_ = nameof(ServiceDatabaseJoystickLatestSubscriber.JoystickLatest_Latest_Subscriber)}" +
            $") VALUES (" +
            $"@{_ = nameof(ServiceDatabaseJoystickLatestSubscriber.JoystickLatest_Latest_Subscriber)}" +
            $")";
        
        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     _ = npgsqlStatement,
            serviceDatabaseTaskNpgsqlParameters: _ = serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task AddJoystickLatestTipperAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        var npgsqlStatement = _ = 
            $"INSERT INTO JoystickLatestTipper (" +
            $"{_ = nameof(ServiceDatabaseJoystickLatestTipper.JoystickLatest_Latest_Tipper)}" +
            $") VALUES (" +
            $"@{_ = nameof(ServiceDatabaseJoystickLatestTipper.JoystickLatest_Latest_Tipper)}" +
            $")";
        
        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     _ = npgsqlStatement,
            serviceDatabaseTaskNpgsqlParameters: _ = serviceDatabaseTaskNpgsqlParameters
        );
    }
}