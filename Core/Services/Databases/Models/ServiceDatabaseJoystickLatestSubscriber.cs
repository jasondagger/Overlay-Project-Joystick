
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseJoystickLatestSubscriber() :
	ServiceDatabaseModel()
{
	internal string JoystickLatest_Latest_Subscriber { get; set; } = _ = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerLatestSubscriber = _ = (string) npgsqlDataReader[name: _ = $"{_ = nameof( ServiceDatabaseJoystickLatestSubscriber.JoystickLatest_Latest_Subscriber )}"];

		_ = this.JoystickLatest_Latest_Subscriber = _ = readerLatestSubscriber;
	}
}