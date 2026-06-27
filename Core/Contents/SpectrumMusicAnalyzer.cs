
using Godot;

namespace Overlay.Core.Contents;

internal static class SpectrumMusicAnalyzer
{
	static SpectrumMusicAnalyzer()
	{ 
		SpectrumMusicAnalyzer.s_busIndex = AudioServer.GetBusIndex(
			busName: $"Record"
		);
	}

	internal static float Intensity => SpectrumMusicAnalyzer.s_intensity;
	
	internal static void Update(
		float delta
	)
	{
		var effect = AudioServer.GetBusEffectInstance(
				busIdx:    SpectrumMusicAnalyzer.s_busIndex, 
				effectIdx: 0
		) as AudioEffectSpectrumAnalyzerInstance;

		var isValid = GodotObject.IsInstanceValid(
			instance: effect
		);
		if (isValid is false)
		{
			return;
		}
		
		var magnitude = effect.GetMagnitudeForFrequencyRange(
			fromHz: 20,
			toHz:   20000
		);
		var rawIntensity = magnitude.Length() * SpectrumMusicAnalyzer.c_volumeScalar;
		var interpSpeed = rawIntensity > SpectrumMusicAnalyzer.s_intensity? SpectrumMusicAnalyzer.c_attackSpeed : SpectrumMusicAnalyzer.c_releaseSpeed;
		SpectrumMusicAnalyzer.s_intensity = Mathf.Lerp(
			from:   SpectrumMusicAnalyzer.s_intensity,
			to:     rawIntensity,
			weight: Mathf.Min(
				a: 1.0f,
				b: delta * interpSpeed
			)
		);
	}
	
	private const float           c_volumeScalar = 20f;
	private const float           c_attackSpeed  = 25f;
	private const float           c_releaseSpeed = 4f;
	private static readonly int   s_busIndex     = 0;
	private static volatile float s_intensity    = 0f;
}