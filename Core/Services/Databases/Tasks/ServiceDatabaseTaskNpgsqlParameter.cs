
namespace Overlay.Core.Services.Databases.Tasks;

internal sealed class ServiceDatabaseTaskNpgsqlParameter
{
    internal ServiceDatabaseTaskNpgsqlParameter(
        string    parameterName,
        object    value
    )
    {
        this.ParameterName = parameterName;
		this.Value         = value;
	}

	internal string ParameterName { get; } = string.Empty;
    internal object Value         { get; } = null;
}