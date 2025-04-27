
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseGoveeData() :
	ServiceDatabaseModel()
{
	[Key]
	internal int    GoveeData_Id	  { get; set; } = _ = 0;
	
	internal string GoveeData_Api_Key { get; set; } = _ = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerId	 = _ = (int)    npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseGoveeData.GoveeData_Id		)}"];
		var readerApiKey = _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseGoveeData.GoveeData_Api_Key )}"];
		
		_ = this.GoveeData_Id	   = _ = readerId;
		_ = this.GoveeData_Api_Key = _ = readerApiKey;
	}
}