shader_type canvas_item;

uniform vec4 color : source_color = vec4(1.0);
uniform sampler2D textureImage;

void fragment()
{
	COLOR = color * texture(textureImage, UV);
}