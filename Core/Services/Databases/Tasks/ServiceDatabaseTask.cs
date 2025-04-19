
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks;

internal abstract class ServiceDatabaseTask<TServiceDatabaseModel, TResult>
	where TServiceDatabaseModel :
		ServiceDatabaseModel
{
	internal abstract TResult Result { get; set; }
}