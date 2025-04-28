
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseLovenseData() :
	ServiceDatabaseModel()
{
	[Key]
	internal int    LovenseData_Id	      { get; set; } = _ = 0;
	
	internal string LovenseData_Api_Iv    { get; set; } = _ = string.Empty;
	internal string LovenseData_Api_Key   { get; set; } = _ = string.Empty;
	internal string LovenseData_Api_Token { get; set; } = _ = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerId	   = _ = (int)    npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseLovenseData.LovenseData_Id		 )}"];
		var readerApiIv    = _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseLovenseData.LovenseData_Api_Iv    )}"];
		var readerApiKey   = _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseLovenseData.LovenseData_Api_Key   )}"];
		var readerApiToken = _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseLovenseData.LovenseData_Api_Token )}"];
		
		_ = this.LovenseData_Id	       = _ = readerId;
		_ = this.LovenseData_Api_Iv    = _ = readerApiIv;
		_ = this.LovenseData_Api_Key   = _ = readerApiKey;
		_ = this.LovenseData_Api_Token = _ = readerApiToken;
	}
}