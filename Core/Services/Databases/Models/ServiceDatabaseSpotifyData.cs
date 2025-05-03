
using System;
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseSpotifyData() :
	ServiceDatabaseModel()
{
	[Key]
	internal int      SpotifyData_Id	        { get; set; } = _ = 0;
	
	internal string   SpotifyData_Access_Token  { get; set; } = _ = string.Empty;
	internal string   SpotifyData_Refresh_Token { get; set; } = _ = string.Empty;
	internal DateTime SpotifyData_Timestamp     { get; set; } = _ = DateTime.MinValue;
	internal string   SpotifyData_Client_Id     { get; set; } = _ = string.Empty;
	internal string   SpotifyData_Client_Secret { get; set; } = _ = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerId	       = _ = (int)      npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseSpotifyData.SpotifyData_Id		       )}"];
		var readerAccessToken  = _ = (string)   npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseSpotifyData.SpotifyData_Access_Token  )}"];
		var readerRefreshToken = _ = (string)   npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseSpotifyData.SpotifyData_Refresh_Token )}"];
		var readerTimestamp    = _ = (DateTime) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseSpotifyData.SpotifyData_Timestamp     )}"];
		var readerClientId     = _ = (string)   npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseSpotifyData.SpotifyData_Client_Id     )}"];
		var readerClientSecret = _ = (string)   npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseSpotifyData.SpotifyData_Client_Secret )}"];

		_ = this.SpotifyData_Id	           = _ = readerId;
		_ = this.SpotifyData_Access_Token  = _ = readerAccessToken;
		_ = this.SpotifyData_Refresh_Token = _ = readerRefreshToken;
		_ = this.SpotifyData_Timestamp	   = _ = readerTimestamp;
		_ = this.SpotifyData_Client_Id     = _ = readerClientId;
		_ = this.SpotifyData_Client_Secret = _ = readerClientSecret;
	}
}