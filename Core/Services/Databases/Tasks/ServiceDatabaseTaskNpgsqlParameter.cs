
using System.Data;

namespace Overlay.Core.Services.Databases.Tasks;

internal sealed class ServiceDatabaseTaskNpgsqlParameter
{
    internal ServiceDatabaseTaskNpgsqlParameter(
        string    parameterName,
        object    value
    )
    {
        _ = this.ParameterName = _ = parameterName;
		_ = this.Value         = _ = value;
	}

	internal string    ParameterName { get; } = _ = string.Empty;
    internal object    Value         { get; } = null;
}