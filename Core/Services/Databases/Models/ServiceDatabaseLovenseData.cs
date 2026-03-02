
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseLovenseData() :
	ServiceDatabaseModel()
{
	[Key]
	internal int    LovenseData_Id	      { get; set; } = 0;
	
	internal string LovenseData_Api_Iv    { get; set; } = string.Empty;
	internal string LovenseData_Api_Key   { get; set; } = string.Empty;
	internal string LovenseData_Api_Token { get; set; } = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerId	   = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseLovenseData.LovenseData_Id		 )}"];
		var readerApiIv    = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseLovenseData.LovenseData_Api_Iv    )}"];
		var readerApiKey   = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseLovenseData.LovenseData_Api_Key   )}"];
		var readerApiToken = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseLovenseData.LovenseData_Api_Token )}"];
		
		this.LovenseData_Id	       = readerId;
		this.LovenseData_Api_Iv    = readerApiIv;
		this.LovenseData_Api_Key   = readerApiKey;
		this.LovenseData_Api_Token = readerApiToken;
	}
}