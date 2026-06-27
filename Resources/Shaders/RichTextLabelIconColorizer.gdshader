shader_type canvas_item;
uniform vec4 target_color : source_color;

void fragment() {
    vec4 tex = texture(
		TEXTURE, 
		UV
	);
    // If the pixel is white (or very close to it), apply the target color
    if (tex.r == 1.0 && tex.g == 1.0 && tex.b == 1.0) {
        COLOR = vec4(
			target_color.rgb, 
			tex.a
		);
    } else {
        COLOR = tex;
    }
}