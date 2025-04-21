
using Npgsql;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Tools;
using System;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Databases.Tasks;

internal static class ServiceDatabaseTaskQueryHandlers
{
    internal static Task HandleExecuteQueryAsyncRetrievedJoystickData(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedJoystickData?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedJoystickData(
                    result: _ = ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseJoystickData>(
                        npgsqlDataReader: _ = npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{_ = nameof(ServiceDatabaseTaskQueryHandlers)}." +
                    $"{_ = nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedJoystickData)}() " +
                    $"EXCEPTION: {_ = exception.Message}"
            );
        }

        return _ = Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedJoystickLatest(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedJoystickLatest?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedJoystickLatest(
                    result: _ = ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseJoystickLatest>(
                        npgsqlDataReader: _ = npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{_ = nameof(ServiceDatabaseTaskQueryHandlers)}." +
                    $"{_ = nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickUsers)}() " +
                    $"EXCEPTION: {_ = exception.Message}"
            );
        }
        
        return _ = Task.CompletedTask;
    }
    
    internal static async Task HandleExecuteQueryAsyncRetrievedListJoystickUsers(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedListJoystickUsers?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedListJoystickUsers(
                    result: _ = await ServiceDatabaseModelReader.ReadServiceDatabaseModelsFromSqlDataReaderAsync<ServiceDatabaseJoystickUser>(
						npgsqlDataReader: _ = npgsqlDataReader
					)
				)
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{_ = nameof(ServiceDatabaseTaskQueryHandlers)}." +
                    $"{_ = nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickUsers)}() " +
                    $"EXCEPTION: {_ = exception.Message}"
            );
        }
    }
}