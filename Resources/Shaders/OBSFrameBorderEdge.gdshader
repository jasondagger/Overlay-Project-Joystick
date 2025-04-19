shader_type canvas_item;

uniform vec4 color: source_color = vec4(1.0);
uniform sampler2D textureNoise: repeat_enable;
uniform float radius: hint_range(0.0, 1.0) = 0.459;
uniform float effectControl: hint_range(0.0, 1.0) = 0.309;
uniform vec2 burnSpeed = vec2(0.0);
uniform float shape: hint_range(0.0, 1.0) = 1.0;

void fragment() {
    vec2 centerDistVec = vec2(0.5) - UV;

	float distToCircleEdge = length(centerDistVec) * radius;
	float distToSquareEdge = 0.5*(0.5 - min(min(UV.x, 1.0 - UV.x), min(UV.y, 1.0 - UV.y)));
	float distToEdge = mix(distToCircleEdge,distToSquareEdge,shape);

    float gradient = smoothstep(0.5, 0.5 - radius, distToEdge);

    float noiseValue = texture(textureNoise, UV + burnSpeed * TIME).r;

    float opacity = step(radius, mix(gradient, noiseValue, effectControl) - distToEdge);

    COLOR = texture(TEXTURE, UV) * vec4(color.rgb, opacity);
}