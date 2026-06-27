shader_type canvas_item;

varying float world_y;
uniform vec4 color : source_color = vec4(1.0);
uniform vec4 alt_color : source_color = vec4(1.0, 0.0, 0.0, 1.0);
uniform sampler2D background;
uniform float feather : hint_range(0.0, 100.0) = 50.0;

void vertex() {
    world_y = (MODEL_MATRIX * vec4(VERTEX, 0.0, 1.0)).y;
}

void fragment() {
    // Creates a soft mask that is 1.0 between the two points and 0.0 outside
    float mask = smoothstep(572.0 - feather, 572.0 + feather, world_y) * (1.0 - smoothstep(1588.0 - feather, 1588.0 + feather, world_y));
    
    vec4 active_color = mix(alt_color, color, mask);
    COLOR = active_color * texture(background, UV);
}