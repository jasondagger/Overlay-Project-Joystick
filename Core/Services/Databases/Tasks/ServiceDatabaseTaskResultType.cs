
namespace Overlay.Core.Services.Databases.Tasks;

internal enum ServiceDatabaseTaskResultType :
    uint
{
    Success = 0U,
    NoRowsAffected,
    Failed,
}