
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
            await using var npgsqlConnection = new NpgsqlConnection(
                connectionString: ServiceDatabaseTaskLogic.c_connectionString
            );
            await npgsqlConnection.OpenAsync();

            await ServiceDatabaseTaskLogic.StartTransactionExecuteNonQueryAsync(
                npgsqlConnection:                    npgsqlConnection,
                npgsqlStatement:                     npgsqlStatement,
                serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError: 
                    $"{nameof(ServiceDatabaseTaskLogic)}." +
                    $"{nameof(ServiceDatabaseTaskLogic.ExecuteNonQueryAsync)}() - " +
                    $"EXCEPTION: {exception.Message}"
            );
        }
    }

    internal static async Task ExecuteQueryAsync(
        string                                   npgsqlStatement,
        ExecuteTaskQueryAsyncHandler             executeQueryAsyncHandler,
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters = null
    )
    {
        try
        {
            await using var npgsqlConnection = new NpgsqlConnection(
                connectionString: ServiceDatabaseTaskLogic.c_connectionString
			);
            await npgsqlConnection.OpenAsync();

            await using var npgsqlCommand = new NpgsqlCommand(
                cmdText:    npgsqlStatement,
                connection: npgsqlConnection
            );
            
            if (serviceDatabaseTaskNpgsqlParameters is not null)
            {
                foreach (var parameter in serviceDatabaseTaskNpgsqlParameters)
                {
                    npgsqlCommand.Parameters.AddWithValue(
                        parameterName: parameter.ParameterName, 
                        value:         parameter.Value
                    );
                }
            }
            
            await using var npqsqlDataReader = await npgsqlCommand.ExecuteReaderAsync();
            await executeQueryAsyncHandler.Invoke(
                npgsqlDataReader: npqsqlDataReader
			);
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError:
                    $"{nameof(ServiceDatabaseTaskLogic)}." +
                    $"{nameof(ServiceDatabaseTaskLogic.ExecuteQueryAsync)}() EXCEPTION: - " +
                    $"EXCEPTION: {exception.Message}"
            );
        }
    }

    internal static async Task<bool> TestConnection()
    {
        try
        {
            await using var npgsqlConnection = new NpgsqlConnection(
                connectionString: ServiceDatabaseTaskLogic.c_connectionString
			);
            await npgsqlConnection.OpenAsync();

            return true;
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError:
                    $"{nameof(ServiceDatabaseTaskLogic)}." +
                    $"{nameof(ServiceDatabaseTaskLogic.TestConnection)}() - " +
                    $"EXCEPTION: {exception.Message}"
            );
        }
        return false;
    }

    private const string c_connectionString =
        $"Host=localhost;" +
        $"Port=5432;" +
        $"Database=overlay;" +
        $"Username=postgres;" +
        $"Password=password";

    private static async Task CompleteTransactionExecuteNonQueryAsync(
        NpgsqlTransaction databaseTransaction,
        NpgsqlCommand     npgsqlCommand
    )
    {
        try
        {
            await npgsqlCommand.ExecuteNonQueryAsync();
            await databaseTransaction.CommitAsync();
        }
        catch (Exception exception)
        {
            await databaseTransaction.RollbackAsync();
            ConsoleLogger.LogMessageError(
                messageError:
                    $"{nameof(ServiceDatabaseTaskLogic)}." +
                    $"{nameof(ServiceDatabaseTaskLogic.CompleteTransactionExecuteNonQueryAsync)}() - " +
                    $"EXCEPTION: {exception.Message}"
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
            await using var databaseTransaction = await npgsqlConnection.BeginTransactionAsync();
            await using var npgsqlCommand = new NpgsqlCommand(
                cmdText:     npgsqlStatement,
                connection:  npgsqlConnection,
                transaction: databaseTransaction
            );
            
            foreach (var serviceDatabaseTaskNpgsqlParameter in serviceDatabaseTaskNpgsqlParameters)
            {
                var parameterName = serviceDatabaseTaskNpgsqlParameter.ParameterName;
                var value         = serviceDatabaseTaskNpgsqlParameter.Value;

                npgsqlCommand.Parameters.AddWithValue(
                    parameterName: parameterName,
                    value:         value
                );
            }

            await ServiceDatabaseTaskLogic.CompleteTransactionExecuteNonQueryAsync(
                databaseTransaction: databaseTransaction,
                npgsqlCommand:       npgsqlCommand
            );
        }
        catch (Exception exception)
        {
            ConsoleLogger.LogMessageError(
                messageError:
                    $"{nameof(ServiceDatabaseTaskLogic)}." +
                    $"{nameof(ServiceDatabaseTaskLogic.StartTransactionExecuteNonQueryAsync)}() - " +
                    $"EXCEPTION: {exception.Message}"
            );
        }
    }
}