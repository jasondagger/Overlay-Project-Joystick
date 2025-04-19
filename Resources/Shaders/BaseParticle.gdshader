shader_type spatial;
render_mode unshaded, cull_disabled;

uniform vec4 color : source_color = vec4(1.0);
uniform sampler2D textureParticle;

void fragment()
{
	vec4 particleColor = texture(textureParticle, UV);
	
	ALBEDO = particleColor.rgb * COLOR.rgb;
	ALPHA = particleColor.a * COLOR.a;
}