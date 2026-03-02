
using System;
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseSpotifyData() :
	ServiceDatabaseModel()
{
	[Key]
	internal int      SpotifyData_Id	        { get; set; } = 0;
	
	internal string   SpotifyData_Access_Token  { get; set; } = string.Empty;
	internal string   SpotifyData_Refresh_Token { get; set; } = string.Empty;
	internal DateTime SpotifyData_Timestamp     { get; set; } = DateTime.MinValue;
	internal string   SpotifyData_Client_Id     { get; set; } = string.Empty;
	internal string   SpotifyData_Client_Secret { get; set; } = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerId	       = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseSpotifyData.SpotifyData_Id		       )}"];
		var readerAccessToken  = (string)   npgsqlDataReader[name: $"{nameof( ServiceDatabaseSpotifyData.SpotifyData_Access_Token  )}"];
		var readerRefreshToken = (string)   npgsqlDataReader[name: $"{nameof( ServiceDatabaseSpotifyData.SpotifyData_Refresh_Token )}"];
		var readerTimestamp    = (DateTime) npgsqlDataReader[name: $"{nameof( ServiceDatabaseSpotifyData.SpotifyData_Timestamp     )}"];
		var readerClientId     = (string)   npgsqlDataReader[name: $"{nameof( ServiceDatabaseSpotifyData.SpotifyData_Client_Id     )}"];
		var readerClientSecret = (string)   npgsqlDataReader[name: $"{nameof( ServiceDatabaseSpotifyData.SpotifyData_Client_Secret )}"];

		this.SpotifyData_Id	           = readerId;
		this.SpotifyData_Access_Token  = readerAccessToken;
		this.SpotifyData_Refresh_Token = readerRefreshToken;
		this.SpotifyData_Timestamp	   = readerTimestamp;
		this.SpotifyData_Client_Id     = readerClientId;
		this.SpotifyData_Client_Secret = readerClientSecret;
	}
}