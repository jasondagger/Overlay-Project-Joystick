shader_type spatial;
render_mode unshaded;

uniform vec4 textureColor0 : source_color = vec4(1.0);
uniform vec2 scrollSpeed0 = vec2(0.0);
uniform sampler2D textureNoise0;

uniform vec4 textureColor1 : source_color = vec4(1.0);
uniform vec2 scrollSpeed1 = vec2(0.0);
uniform sampler2D textureNoise1;

uniform vec4 textureColor2 : source_color = vec4(1.0);
uniform vec2 scrollSpeed2 = vec2(0.0);
uniform sampler2D textureNoise2;

void fragment()
{
	vec3 color0 = texture(textureNoise0, UV + TIME * scrollSpeed0).rgb * textureColor0.rgb;
	vec3 color1 = texture(textureNoise1, UV + TIME * scrollSpeed1).rgb * textureColor1.rgb;
	vec3 color2 = texture(textureNoise2, UV + TIME * scrollSpeed2).rgb * textureColor2.rgb;

	ALBEDO = color0.rgb + color1.rgb + color2.rgb;
}