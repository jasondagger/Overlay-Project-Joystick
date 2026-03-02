
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseGoveeLight() :
	ServiceDatabaseModel()
{
	[Key]
	internal int    GoveeLight_Id			{ get; set; } = 0;
	
	internal string GoveeLight_Hardware_Id { get; set; } = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerId		 = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseGoveeLight.GoveeLight_Id	       )}"];
		var readerHardwareId = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseGoveeLight.GoveeLight_Hardware_Id )}"];
		
		this.GoveeLight_Id			= readerId;
		this.GoveeLight_Hardware_Id = readerHardwareId;
	}
}