
using Npgsql;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Tools;
using System;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Databases.Tasks;

internal static class ServiceDatabaseTaskQueryHandlers
{
    internal static Task HandleExecuteQueryAsyncRetrievedGoveeData(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedGoveeData?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedGoveeData(
                    result: _ = ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseGoveeData>(
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
                    $"{_ = nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedGoveeData)}() " +
                    $"EXCEPTION: {_ = exception.Message}"
            );
        }
        
        return _ = Task.CompletedTask;
    }
    
    internal static async Task HandleExecuteQueryAsyncRetrievedListGoveeLights(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedListGoveeLights?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedListGoveeLights(
                    result: _ = await ServiceDatabaseModelReader.ReadServiceDatabaseModelsFromSqlDataReaderAsync<ServiceDatabaseGoveeLight>(
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
                    $"{_ = nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListGoveeLights)}() " +
                    $"EXCEPTION: {_ = exception.Message}"
            );
        }
    }
    
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
    
    internal static async Task HandleExecuteQueryAsyncRetrievedListJoystickLatestFollowers(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedListJoystickLatestFollowers?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedListJoystickLatestFollowers(
                    result: _ = await ServiceDatabaseModelReader.ReadServiceDatabaseModelsFromSqlDataReaderAsync<ServiceDatabaseJoystickLatestFollower>(
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
                    $"{_ = nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickLatestFollowers)}() " +
                    $"EXCEPTION: {_ = exception.Message}"
            );
        }
    }
    
    internal static async Task HandleExecuteQueryAsyncRetrievedListJoystickLatestSubscribers(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedListJoystickLatestSubscribers?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedListJoystickLatestSubscribers(
                    result: _ = await ServiceDatabaseModelReader.ReadServiceDatabaseModelsFromSqlDataReaderAsync<ServiceDatabaseJoystickLatestSubscriber>(
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
                    $"{_ = nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickLatestSubscribers)}() " +
                    $"EXCEPTION: {_ = exception.Message}"
            );
        }
    }
    
    internal static async Task HandleExecuteQueryAsyncRetrievedListJoystickLatestTippers(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedListJoystickLatestTippers?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedListJoystickLatestTippers(
                    result: _ = await ServiceDatabaseModelReader.ReadServiceDatabaseModelsFromSqlDataReaderAsync<ServiceDatabaseJoystickLatestTipper>(
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
                    $"{_ = nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickLatestTippers)}() " +
                    $"EXCEPTION: {_ = exception.Message}"
            );
        }
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