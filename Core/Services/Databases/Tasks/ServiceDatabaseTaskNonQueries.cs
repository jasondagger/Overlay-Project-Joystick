
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
	};

    private static async Task AddAsyncJoystickUser(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		var npgsqlStatement = _ = 
            $"INSERT INTO JoystickUsers (" +
            $"{_ = nameof(ServiceDatabaseJoystickUser.JoystickUser_CustomChatTextColor)}, " +
            $"{_ = nameof(ServiceDatabaseJoystickUser.JoystickUser_Username)}" +
            $") VALUES (" +
            $"@{_ = nameof(ServiceDatabaseJoystickUser.JoystickUser_CustomChatTextColor)}, " +
            $"@{_ = nameof(ServiceDatabaseJoystickUser.JoystickUser_Username)}" +
            $")";
        
		await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     _ = npgsqlStatement,
            serviceDatabaseTaskNpgsqlParameters: _ = serviceDatabaseTaskNpgsqlParameters
		);
    }
}