
using Npgsql;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Tools;
using System;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Databases.Tasks;

internal static class ServiceDatabaseTaskQueryHandlers
{
    internal static Task HandleExecuteQueryAsyncRetrievedGoogleData(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedGoogleData?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedGoogleData(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseGoogleData>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                    $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedGoveeData)}() " +
                    $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedGoveeData(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedGoveeData?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedGoveeData(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseGoveeData>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                    $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedGoveeData)}() " +
                    $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static async Task HandleExecuteQueryAsyncRetrievedListGoveeLights(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedListGoveeLights?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedListGoveeLights(
                    result: await ServiceDatabaseModelReader.ReadServiceDatabaseModelsFromSqlDataReaderAsync<ServiceDatabaseGoveeLight>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                    $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListGoveeLights)}() " +
                    $"EXCEPTION: {exception.Message}"
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
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseJoystickData>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                    $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedJoystickData)}() " +
                    $"EXCEPTION: {exception.Message}"
            );
        }

        return Task.CompletedTask;
    }
    
    internal static async Task HandleExecuteQueryAsyncRetrievedListJoystickLatestFollowers(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedListJoystickLatestFollowers?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedListJoystickLatestFollowers(
                    result: await ServiceDatabaseModelReader.ReadServiceDatabaseModelsFromSqlDataReaderAsync<ServiceDatabaseJoystickLatestFollower>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                    $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickLatestFollowers)}() " +
                    $"EXCEPTION: {exception.Message}"
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
                    result: await ServiceDatabaseModelReader.ReadServiceDatabaseModelsFromSqlDataReaderAsync<ServiceDatabaseJoystickLatestSubscriber>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                    $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickLatestSubscribers)}() " +
                    $"EXCEPTION: {exception.Message}"
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
                    result: await ServiceDatabaseModelReader.ReadServiceDatabaseModelsFromSqlDataReaderAsync<ServiceDatabaseJoystickLatestTipper>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                    $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickLatestTippers)}() " +
                    $"EXCEPTION: {exception.Message}"
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
                    result: await ServiceDatabaseModelReader.ReadServiceDatabaseModelsFromSqlDataReaderAsync<ServiceDatabaseJoystickUser>(
						npgsqlDataReader: npgsqlDataReader
					)
				)
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                    $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickUsers)}() " +
                    $"EXCEPTION: {exception.Message}"
            );
        }
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedLovenseData(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedLovenseData?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedLovenseData(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseLovenseData>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                    $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedLovenseData)}() " +
                    $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedSpotifyData(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedSpotifyData?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedSpotifyData(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseSpotifyData>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                    $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedSpotifyData)}() " +
                    $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }

}