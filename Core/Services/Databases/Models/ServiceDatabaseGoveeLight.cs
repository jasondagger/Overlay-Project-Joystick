
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseGoveeLight() :
	ServiceDatabaseModel()
{
	[Key]
	internal int    GoveeLight_Id			{ get; set; } = _ = 0;
	
	internal string GoveeLight_Hardware_Id { get; set; } = _ = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerId		 = _ = (int)    npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseGoveeLight.GoveeLight_Id	       )}"];
		var readerHardwareId = _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseGoveeLight.GoveeLight_Hardware_Id )}"];
		
		_ = this.GoveeLight_Id			= _ = readerId;
		_ = this.GoveeLight_Hardware_Id = _ = readerHardwareId;
	}
}