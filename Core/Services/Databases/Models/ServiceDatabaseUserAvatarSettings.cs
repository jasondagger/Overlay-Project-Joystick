
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseUserAvatarSettings() :
	ServiceDatabaseModel()
{
	[Key]
	internal string UserAvatarSettings_Username          { get; set; } = string.Empty;
	internal int    UserAvatarSettings_Base_Color_Id     { get; set; } = 0;
	internal int    UserAvatarSettings_Outline_Color_Id  { get; set; } = 0;
	internal int    UserAvatarSettings_Model_Id          { get; set; } = 0;
	internal int    UserAvatarSettings_Shader0_Color_Id  { get; set; } = 0;
	internal int    UserAvatarSettings_Shader0_Effect_Id { get; set; } = 0;
	internal int    UserAvatarSettings_Shader1_Color_Id  { get; set; } = 0;
	internal int    UserAvatarSettings_Shader1_Effect_Id { get; set; } = 0;
	internal int    UserAvatarSettings_Shader2_Color_Id  { get; set; } = 0;
	internal int    UserAvatarSettings_Shader2_Effect_Id { get; set; } = 0;
	internal int    UserAvatarSettings_Shader3_Color_Id  { get; set; } = 0;
	internal int    UserAvatarSettings_Shader3_Effect_Id { get; set; } = 0;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerUsername        = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username          )}"];
		var readerBaseColorId     = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Base_Color_Id     )}"];
		var readerOutlineColorId  = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Outline_Color_Id  )}"];
		var readerModelId         = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Model_Id          )}"];
		var readerShader0ColorId  = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader0_Color_Id  )}"];
		var readerShader0EffectId = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader0_Effect_Id )}"];
		var readerShader1ColorId  = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader1_Color_Id  )}"];
		var readerShader1EffectId = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader1_Effect_Id )}"];
		var readerShader2ColorId  = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader2_Color_Id  )}"];
		var readerShader2EffectId = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader2_Effect_Id )}"];
		var readerShader3ColorId  = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader3_Color_Id  )}"];
		var readerShader3EffectId = (int)    npgsqlDataReader[name: $"{nameof( ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader3_Effect_Id )}"];

		this.UserAvatarSettings_Username          = readerUsername;
		this.UserAvatarSettings_Base_Color_Id     = readerBaseColorId;
		this.UserAvatarSettings_Outline_Color_Id  = readerOutlineColorId;
		this.UserAvatarSettings_Model_Id          = readerModelId;
		this.UserAvatarSettings_Shader0_Color_Id  = readerShader0ColorId;
		this.UserAvatarSettings_Shader0_Effect_Id = readerShader0EffectId;
		this.UserAvatarSettings_Shader1_Color_Id  = readerShader1ColorId;
		this.UserAvatarSettings_Shader1_Effect_Id = readerShader1EffectId;
		this.UserAvatarSettings_Shader2_Color_Id  = readerShader2ColorId;
		this.UserAvatarSettings_Shader2_Effect_Id = readerShader2EffectId;
		this.UserAvatarSettings_Shader3_Color_Id  = readerShader3ColorId;
		this.UserAvatarSettings_Shader3_Effect_Id = readerShader3EffectId;
	}
}