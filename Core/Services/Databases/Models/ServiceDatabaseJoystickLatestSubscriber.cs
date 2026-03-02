
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseJoystickLatestSubscriber() :
	ServiceDatabaseModel()
{
	internal string JoystickLatest_Latest_Subscriber { get; set; } = string.Empty;

	internal override void CreateFromNpgsqlDataReader(
		NpgsqlDataReader npgsqlDataReader
	)
	{
		var readerLatestSubscriber = (string) npgsqlDataReader[name: $"{nameof( ServiceDatabaseJoystickLatestSubscriber.JoystickLatest_Latest_Subscriber )}"];

		this.JoystickLatest_Latest_Subscriber = readerLatestSubscriber;
	}
}