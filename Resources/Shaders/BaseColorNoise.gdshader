shader_type spatial;
render_mode unshaded;

uniform vec4 color : source_color = vec4(1.0);
uniform sampler2D textureNoise;

float remap(float value, float in_min, float in_max, float out_min, float out_max) {
    float t = (value - in_min) / (in_max - in_min);
    return mix(out_min, out_max, t);
}

void fragment()
{
	float noise = texture(textureNoise, UV).r;
	float noiseRemapped = remap(noise, 0.0, 1.0, 0.5, 1.0);
	ALBEDO = noiseRemapped * color.rgb;
}