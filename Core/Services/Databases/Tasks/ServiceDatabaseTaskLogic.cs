
using Npgsql;
using Overlay.Core.Tools;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Databases.Tasks;

internal static class ServiceDatabaseTaskLogic
{
    internal delegate Task ExecuteTaskQueryAsyncHandler(
        NpgsqlDataReader npgsqlDataReader
    );

    internal static async Task ExecuteNonQueryAsync(
        string                                   npgsqlStatement,
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        try
        {
            await using var npgsqlConnection = _ = new NpgsqlConnection(
                connectionString: _ = ServiceDatabaseTaskLogic.c_connectionString
            );
            await npgsqlConnection.OpenAsync();

            await ServiceDatabaseTaskLogic.StartTransactionExecuteNonQueryAsync(
                npgsqlConnection:                    _ = npgsqlConnection,
                npgsqlStatement:                     _ = npgsqlStatement,
                serviceDatabaseTaskNpgsqlParameters: _ = serviceDatabaseTaskNpgsqlParameters
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{_ = nameof(ServiceDatabaseTaskLogic)}." +
                    $"{_ = nameof(ServiceDatabaseTaskLogic.ExecuteNonQueryAsync)}() - " +
                    $"EXCEPTION: {_ = exception.Message}"
            );
        }
    }

    internal static async Task ExecuteQueryAsync(
        string                       npgsqlStatement,
        ExecuteTaskQueryAsyncHandler executeQueryAsyncHandler
    )
    {
        try
        {
            await using var npgsqlConnection = _ = new NpgsqlConnection(
                connectionString: _ = ServiceDatabaseTaskLogic.c_connectionString
			);
            await npgsqlConnection.OpenAsync();

            await using var npgsqlCommand = _ = new NpgsqlCommand(
                cmdText:    _ = npgsqlStatement,
                connection: _ = npgsqlConnection
            );
            await using var npqsqlDataReader = _ = await npgsqlCommand.ExecuteReaderAsync();
            await executeQueryAsyncHandler.Invoke(
                npgsqlDataReader: _ = npqsqlDataReader
			);
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{_ = nameof(ServiceDatabaseTaskLogic)}." +
                    $"{_ = nameof(ServiceDatabaseTaskLogic.ExecuteQueryAsync)}() EXCEPTION: - " +
                    $"EXCEPTION: {_ = exception.Message}"
            );
        }
    }

    internal static async Task<bool> TestConnection()
    {
        try
        {
            await using var npgsqlConnection = _ = new NpgsqlConnection(
                connectionString: _ = ServiceDatabaseTaskLogic.c_connectionString
			);
            await npgsqlConnection.OpenAsync();

            return _ = true;
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{_ = nameof(ServiceDatabaseTaskLogic)}." +
                    $"{_ = nameof(ServiceDatabaseTaskLogic.TestConnection)}() - " +
                    $"EXCEPTION: {_ = exception.Message}"
            );
        }
        return _ = false;
    }

    private const string c_connectionString =
        $"Host=localhost;" +
        $"Port=5432;" +
        $"Database=overlay;" +
        $"Username=postgres;" +
        $"Password=postgres";

    private static async Task CompleteTransactionExecuteNonQueryAsync(
        NpgsqlTransaction databaseTransaction,
        NpgsqlCommand     npgsqlCommand
    )
    {
        try
        {
            _ = await npgsqlCommand.ExecuteNonQueryAsync();
            await databaseTransaction.CommitAsync();
        }
        catch (Exception exception)
        {
            await databaseTransaction.RollbackAsync();
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{_ = nameof(ServiceDatabaseTaskLogic)}." +
                    $"{_ = nameof(ServiceDatabaseTaskLogic.CompleteTransactionExecuteNonQueryAsync)}() - " +
                    $"EXCEPTION: {_ = exception.Message}"
            );
        }
    }

    private static async Task StartTransactionExecuteNonQueryAsync(
        NpgsqlConnection                         npgsqlConnection,
        string                                   npgsqlStatement,
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        try
        {
            await using var databaseTransaction = _ = await npgsqlConnection.BeginTransactionAsync();
            await using var npgsqlCommand = _ = new NpgsqlCommand(
                cmdText:     _ = npgsqlStatement,
                connection:  _ = npgsqlConnection,
                transaction: _ = databaseTransaction
            );
            
            foreach (var serviceDatabaseTaskNpgsqlParameter in _ = serviceDatabaseTaskNpgsqlParameters)
            {
                var parameterName = _ = serviceDatabaseTaskNpgsqlParameter.ParameterName;
                var value         = _ = serviceDatabaseTaskNpgsqlParameter.Value;

                _ = npgsqlCommand.Parameters.AddWithValue(
                    parameterName: _ = parameterName,
                    value:         _ = value
                );
            }

            await ServiceDatabaseTaskLogic.CompleteTransactionExecuteNonQueryAsync(
                databaseTransaction: _ = databaseTransaction,
                npgsqlCommand:       _ = npgsqlCommand
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{_ = nameof(ServiceDatabaseTaskLogic)}." +
                    $"{_ = nameof(ServiceDatabaseTaskLogic.StartTransactionExecuteNonQueryAsync)}() - " +
                    $"EXCEPTION: {_ = exception.Message}"
            );
        }
    }
}