
using Godot;
using Overlay.Core.Services;
using Overlay.Core.Services.Databases;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Https;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Overlay.Core.Contents.Effects;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.Godots.Inputs;
using Overlay.Core.Services.Govee.Payloads;
using Overlay.Core.Services.Joysticks;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;
using Overlay.Core.Tools;

namespace Overlay;

internal sealed partial class Main() :
	Node()
{
	public override async void _EnterTree()
	{
#if DEBUG
		if (_ = Main.Node is not null)
		{
			throw _ = new Exception(
				message: _ =
					$"EXCEPTION: " +
					$"{_ = nameof(Main)}." +
					$"{_ = nameof(Main.Node)} - " +
					$"Duplicate '{_ = nameof(Main)}' detected."
			);
		}
#endif

		_ = Main.Node = _ = this;
		
		await Services.Start();
		
		// ServiceJoystickWebSocketPayloadStreamEvents.ChatTimerStarted += T;
		// ServiceDatabaseTaskEvents.RetrievedListJoystickUsers += TestDatabaseRead;
		// ServiceDatabaseTaskEvents.RetrievedJoystickData += TestDatabaseRead;
		TestHttp();
		// TestDatabaseWrite();
		
		// each letter & symbol as a .res file
	}

	public override void _Process(
		double delta
	)
	{
		Main.ProcessNodeTargets();
	}

	internal static Node Node { get; private set; } = null;

	internal static void AddNodeToNode(
		Node node,
		Node target
	)
	{
		lock (_ = Main.s_lock)
		{
			Main.s_nodeTargets.Enqueue(
				item: _ = new NodeTarget(
					node:   _ = node,
					target: _ = target
				)
			);
		}
	}

	private sealed class NodeTarget(
		Node node,
		Node target
	)
	{
		internal Node Node   { get; set; } = _ = node;
		internal Node Target { get; set; } = _ = target;
	}

	private static readonly Queue<NodeTarget> s_nodeTargets = new();
	private static readonly object            s_lock        = new();

	private static void ProcessNodeTargets()
	{
		NodeTarget nodeTarget;
		lock (_ = Main.s_lock)
		{
			if (_ = Main.s_nodeTargets.Count > 0U)
			{
				_ = nodeTarget = _ = Main.s_nodeTargets.Dequeue();
			}
			else
			{
				return;
			}
		}
		
		var node   = _ = nodeTarget.Node;
		var target = _ = nodeTarget.Target;
		
		target.AddChild(
			node: _ = node
		);
	}
	
	private static void TestDatabaseWrite()
	{
		ServiceDatabase.ExecuteTaskNonQuery(
			ServiceDatabaseTaskNonQueryType.AddJoystickUser,
			[
				new ServiceDatabaseTaskNpgsqlParameter(
					"joystickuser_customchattextcolor",
					"#FF2CFF"
				),
				new ServiceDatabaseTaskNpgsqlParameter(
					"joystickuser_username",
					"SmoothDagger"
				),
			]
		);
	}

	private static void TestDatabaseRead(
		ServiceDatabaseTaskRetrievedJoystickData retrievedJoystickData 
	)
	{
		var i = 0;
		i++;
	}

	private static void TestHttp()
	{
		var serviceGodots    = _ = Services.GetService<ServiceGodots>();
		var serviceGodotHttp = _ = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
		
		serviceGodotHttp.SendHttpRequest(
			url: "https://openapi.api.govee.com/router/api/v1/device/control",
			headers: [
				$"Govee-API-Key: 8ac3d53e-b861-4b94-a59b-461918168427",
				$"Content-Type: application/json",
			],
			method: HttpClient.Method.Post,
			json: "{\n  \"requestId\": \"10000000000000000000\",\n  \"payload\": {\n    \"sku\": \"H607C\",\n    \"device\": \"2A:A8:D8:7C:B4:47:41:D6\",\n    \"capability\": {\n      \"type\": \"devices.capabilities.segment_color_setting\",\n      \"instance\": \"segmentedColorRgb\",\n      \"value\": {\n      \t\"segment\":[0,1,2,3,4,5,6,7,8],\n        \"rgb\":0x00FF00\n      }\n    }\n  }\n}",
			requestCompletedHandler: (
				long     result,
				long     responseCode,
				string[] headers,
				byte[]   body
			) =>
			{
				var bodyAsString = Encoding.UTF8.GetString(
					body
				);
				
				// H607C
				// 2A:A8:D8:7C:B4:47:41:D6
				// 2A:E6:FA:FB:20:2E:71:B1
				//var json = JsonHelper.Deserialize<ServiceGoveePayload>(bodyAsString);
				
				int i = 0;
				i++;
				
				//

			}
		);
	}
}