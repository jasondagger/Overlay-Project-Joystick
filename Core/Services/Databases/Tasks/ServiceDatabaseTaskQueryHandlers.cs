
#nullable enable
using Npgsql;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Databases.Tasks.Validates;
using Overlay.Core.Tools;
using System;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Databases.Tasks;

internal static class ServiceDatabaseTaskQueryHandlers
{
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserCheckedBanks(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserCheckedBanks?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserCheckedBanks)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserCheckedUnlocks(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserCheckedUnlocks?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserCheckedUnlocks)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserClearedNames(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserClearedNames?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserClearedNames)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserClearedTitles(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserClearedTitles?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserClearedTitles)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserCompletedBhopBonuses(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserCompletedBhopBonuses?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserCompletedBhopBonuses)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserCompletedBhopMaps(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserCompletedBhopMaps?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserCompletedBhopMaps)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserCustomizedAvatar(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserCustomizedAvatar?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserCustomizedAvatar)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserCustomizedBadge(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserCustomizedBadge?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserCustomizedBadge)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserCustomizedName(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserCustomizedName?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserCustomizedName)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserCustomizedTitle(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserCustomizedTitle?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserCustomizedTitle)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserLinkedSteams(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserLinkedSteams?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserLinkedSteams)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserMessagesSent(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserMessagesSent?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserMessagesSent)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserMinutesWatched(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserMinutesWatched?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserMinutesWatched)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserPlayedDemomans(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserPlayedDemomans?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPlayedDemomans)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserPlayedEngineers(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserPlayedEngineers?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPlayedEngineers)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserPlayedHeavies(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserPlayedHeavies?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPlayedHeavies)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserPlayedMedics(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserPlayedMedics?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPlayedMedics)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserPlayedPyros(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserPlayedPyros?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPlayedPyros)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserPlayedScouts(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserPlayedScouts?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPlayedScouts)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserPlayedSnipers(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserPlayedSnipers?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPlayedSnipers)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserPlayedSoldiers(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserPlayedSoldiers?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPlayedSoldiers)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserPlayedSpies(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserPlayedSpies?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPlayedSpies)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserPreviewedUnlocks(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserPreviewedUnlocks?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPreviewedUnlocks)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserQuestionsAsked(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserQuestionsAsked?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserQuestionsAsked)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorLose3InARow(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorLose3InARow?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorLose3InARow)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorLosses(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorLosses?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorLosses)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorMatchesPlayed(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorMatchesPlayed?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorMatchesPlayed)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorPapers(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorPapers?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorPapers)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorRocks(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorRocks?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorRocks)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorScissors(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorScissors?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorScissors)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorTie3InARow(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorTie3InARow?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorTie3InARow)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorTies(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorTies?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorTies)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorWin3InARow(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorWin3InARow?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorWin3InARow)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorWins(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorWins?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorWins)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRolled1s(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled1s?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled1s)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRolled42s(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled42s?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled42s)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRolled67s(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled67s?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled67s)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRolled69s(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled69s?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled69s)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRolled100s(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled100s?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled100s)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRolled240s(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled240s?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled240s)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRolled256s(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled256s?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled256s)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRolled420s(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled420s?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled420s)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRolled720s(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled720s?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled720s)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRolled1080s(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled1080s?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled1080s)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRolled1337s(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled1337s?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled1337s)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRolled3840s(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled3840s?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled3840s)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRolled100000s(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled100000s?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled100000s)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRolls(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRolls?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolls)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserRollsMaximum(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserRollsMaximum?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRollsMaximum)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserSmoothDaggerDeaths(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserSmoothDaggerDeaths?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserSmoothDaggerDeaths)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserSmoothDaggerDominatedBys(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserSmoothDaggerDominatedBys?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserSmoothDaggerDominatedBys)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserSmoothDaggerDominations(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserSmoothDaggerDominations?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserSmoothDaggerDominations)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserSmoothDaggerKills(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserSmoothDaggerKills?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserSmoothDaggerKills)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserSmoothDaggerRevenges(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserSmoothDaggerRevenges?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserSmoothDaggerRevenges)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserTimesViewed(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserTimesViewed?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserTimesViewed)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserTitlesUnlocked(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserTitlesUnlocked?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserTitlesUnlocked)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedAchievementUserTracksCompleted(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedAchievementUserTracksCompleted?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedAchievementUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseAchievementUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserTracksCompleted)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedBankUser(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedBankUser?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedBankUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseBankUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                    $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                    $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedBankUser)}() " +
                    $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedBankUserTimeLimit(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedBankUserTimeLimit?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedBankUserTimeLimit(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseBankUserTimeLimit>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedBankUserTimeLimit)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedBankUserTip(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedBankUserTip?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedBankUserTip(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseBankUserTip>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedBankUserTip)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
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
                messageError: 
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
                messageError: 
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
                messageError: 
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
                messageError: 
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
                messageError: 
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
                messageError: 
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
                messageError: 
                    $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                    $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickLatestTippers)}() " +
                    $"EXCEPTION: {exception.Message}"
            );
        }
    }
    
    internal static async Task HandleExecuteQueryAsyncRetrievedListUserBadgeColors(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedListUserBadgeColors?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedListUserBadgeColors(
                    result: await ServiceDatabaseModelReader.ReadServiceDatabaseModelsFromSqlDataReaderAsync<ServiceDatabaseUserBadgeColor>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickLatestTippers)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
    }
    
    internal static async Task HandleExecuteQueryAsyncRetrievedListUserNameColors(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedListUserNameColors?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedListUserNameColors(
                    result: await ServiceDatabaseModelReader.ReadServiceDatabaseModelsFromSqlDataReaderAsync<ServiceDatabaseUserNameColor>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListUserNameColors)}() " +
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
                messageError: 
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
                messageError: 
                    $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                    $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedSpotifyData)}() " +
                    $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedSteamUser(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedSteamUser?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedSteamUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseSteamUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedSteamUser)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }

    internal static Task HandleExecuteQueryAsyncRetrievedUserAvatarSettings(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedUserAvatarSettings?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedUserAvatarSettings(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseUserAvatarSettings>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedUserAvatarSettings)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedUserEnteredSteamUsername(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedUserEnteredSteamUsername?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedSteamUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseSteamUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedUserEnteredSteamUsername)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedUserExitedSteamUsername(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedUserExitedSteamUsername?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedSteamUser(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseSteamUser>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedUserExitedSteamUsername)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedUserTitle(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedUserTitle?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedUserTitle(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseUserTitle>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedUserTitle)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedUserUnlockColors(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedUserUnlockColors?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedUserUnlocks(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseUserUnlocks>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListGoveeLights)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedUserUnlockEffects(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedUserUnlockEffects?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedUserUnlocks(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseUserUnlocks>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListGoveeLights)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedUserUnlockModels(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedUserUnlockModels?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedUserUnlocks(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseUserUnlocks>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedUserUnlockModels)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncRetrievedUserUnlockTitles(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.RetrievedUserUnlockTitles?.Invoke(
                obj: new ServiceDatabaseTaskRetrievedUserUnlocks(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseUserUnlocks>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedUserUnlockTitles)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncValidatedUserHasColor_Avatar(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.ValidatedUserHasColor_Avatar?.Invoke(
                obj: new ServiceDatabaseTaskValidatedUserHasColor(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseValidateUserHas>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncValidatedUserHasColor_Avatar)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncValidatedUserHasColor_Badge(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.ValidatedUserHasColor_Badge?.Invoke(
                obj: new ServiceDatabaseTaskValidatedUserHasColor(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseValidateUserHas>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncValidatedUserHasColor_Avatar)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncValidatedUserHasColor_Name(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.ValidatedUserHasColor_Name?.Invoke(
                obj: new ServiceDatabaseTaskValidatedUserHasColor(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseValidateUserHas>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncValidatedUserHasColor_Name)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncValidatedUserHasEffectAndColor(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.ValidatedUserHasEffectAndColor?.Invoke(
                obj: new ServiceDatabaseTaskValidatedUserHasEffectAndColor(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseValidateUserHas>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncValidatedUserHasEffectAndColor)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncValidatedUserHasModel(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.ValidatedUserHasModel?.Invoke(
                obj: new ServiceDatabaseTaskValidatedUserHasModel(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseValidateUserHas>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncValidatedUserHasModel)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncValidatedUserHasTitle(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.ValidatedUserHasTitle?.Invoke(
                obj: new ServiceDatabaseTaskValidatedUserHasTitle(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseValidateUserHas>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncValidatedUserHasTitle)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    
    internal static Task HandleExecuteQueryAsyncValidatedUserUnlockColor(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.ValidatedUserUnlockColor?.Invoke(
                obj: new ServiceDatabaseTaskValidatedUserUnlockColor(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseValidateUserUnlock>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncValidatedUserHasColor_Avatar)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncValidatedUserUnlockEffect(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.ValidatedUserUnlockEffect?.Invoke(
                obj: new ServiceDatabaseTaskValidatedUserUnlockEffect(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseValidateUserUnlock>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncValidatedUserHasEffectAndColor)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
    
    internal static Task HandleExecuteQueryAsyncValidatedUserUnlockModel(
        NpgsqlDataReader npgsqlDataReader
    )
    {
        try
        {
            ServiceDatabaseTaskEvents.ValidatedUserUnlockModel?.Invoke(
                obj: new ServiceDatabaseTaskValidatedUserUnlockModel(
                    result: ServiceDatabaseModelReader.ReadServiceDatabaseModelFromSqlDataReader<ServiceDatabaseValidateUserUnlock>(
                        npgsqlDataReader: npgsqlDataReader
                    )
                )
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                $"{nameof(ServiceDatabaseTaskQueryHandlers)}." +
                $"{nameof(ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncValidatedUserUnlockModel)}() " +
                $"EXCEPTION: {exception.Message}"
            );
        }
        
        return Task.CompletedTask;
    }
}