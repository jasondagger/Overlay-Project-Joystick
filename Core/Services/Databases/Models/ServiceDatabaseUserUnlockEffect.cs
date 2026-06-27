
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseUserUnlockEffect() :
	ServiceDatabaseModel()
{
	[Key]
	internal int    UserUnlockEffects_Id        { get; set; } = 0;
	internal string UserUnlockEffects_Username  { get; set; } = string.Empty;
	internal int    UserUnlockEffects_Effect_Id { get; set; } = 0;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerId       = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Id        )}"];
		var readerUsername = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Username  )}"];
		var readerEffectId = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Effect_Id )}"];

		this.UserUnlockEffects_Id        = readerId;
		this.UserUnlockEffects_Username  = readerUsername;
		this.UserUnlockEffects_Effect_Id = readerEffectId;
	}
}