// Waving Particles shader by Brian Smith (steampunkdemon.itch.io)
// MIT licence

shader_type canvas_item;

// Comment out the following line if you are not applying the shader to a ColorRect:
uniform vec2 dimensions = vec2(1152.0, 648.0); // Resolution of ColorRect in pixels

uniform float rows = 20.0;
uniform float columns = 5.0;
uniform float vertical_scroll : hint_range(-1.0, 1.0, 0.01) = 0.5;
uniform float horizontal_scroll : hint_range(-1.0, 1.0, 0.01) = 0.1;
uniform float size_min : hint_range(0.1, 1.0, 0.1) = 0.3;
uniform float size_max : hint_range(0.1, 1.0, 0.1) = 0.7; // If 'size_max' is larger than 0.7 the corners of the particles will be clipped if they are rotating.
uniform float wave_min : hint_range(0.0, 1.0, 0.1) = 0.1;
uniform float wave_max : hint_range(0.0, 1.0, 0.1) = 1.0; // If the particles are waving into the neighbouring columns reduce 'wave_max'.
uniform float wave_speed : hint_range(0.0, 2.0, 0.1) = 0.5;
uniform float wave_rotation : hint_range(-1.0, 1.0, 0.1) = 1.0;
uniform vec4 color : source_color = vec4(1.0, 1.0, 1.0, 1.0);

uniform sampler2D particle_texture : source_color, filter_nearest_mipmap;

float greater_than(float x, float y) {
	return max(sign(x - y), 0.0);
}

void fragment() {
	float time = 10.0 + TIME;

	float row_rn = fract(sin(floor((UV.y - vertical_scroll * time) * rows)));
	float column_rn = fract(sin(floor((UV.x + row_rn - horizontal_scroll * time) * columns)));
	float wave = sin(wave_speed * time + column_rn * 90.0);
	vec2 uv = (vec2(fract((UV.x + row_rn - horizontal_scroll * time + (wave * (wave_min + (wave_max - wave_min) * column_rn) / columns / 2.0)) * columns), fract((UV.y - vertical_scroll * time) * rows)) * 2.0 - 1.0) * vec2(dimensions.x / dimensions.y * rows / columns, 1.0);
	float size = size_min + (size_max - size_min) * column_rn;
	vec4 outputColor = mix(color * 0.85f, color, column_rn);
	
// Comment out the below two lines if you do not need or want the particles to rotate:
	float a = ((column_rn + wave) * wave_rotation) * PI;
	uv *= mat2(vec2(sin(a), -cos(a)), vec2(cos(a), sin(a)));

	vec4 particle_texture_pixel = texture(particle_texture, (uv / size + 1.0) / 2.0) * outputColor;
	COLOR = particle_texture_pixel;
}