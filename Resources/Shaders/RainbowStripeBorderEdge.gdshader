shader_type canvas_item;

varying float world_y;
uniform vec4 color : source_color = vec4(1.0);
uniform vec4 alt_color : source_color = vec4(1.0, 0.0, 0.0, 1.0);
uniform sampler2D textureNoise : repeat_enable;
uniform sampler2D textureBackground;
uniform float radius : hint_range(0.0, 1.0) = 0.459;
uniform float effectControl : hint_range(0.0, 1.0) = 0.309;
uniform float shape : hint_range(0.0, 1.0) = 1.0;
uniform vec2 tiling_factor = vec2(1.0, 1.0);
uniform vec2 noise_offset;
uniform float feather : hint_range(0.0, 100.0) = 50.0;

void vertex() {
    world_y = (MODEL_MATRIX * vec4(VERTEX, 0.0, 1.0)).y;
}

void fragment() {
    // Blends alt_color and color smoothly at the 572 and 1588 marks
    float mask = smoothstep(572.0 - feather, 572.0 + feather, world_y) * (1.0 - smoothstep(1588.0 - feather, 1588.0 + feather, world_y));
    vec4 active_color = mix(alt_color, color, mask);

    vec2 tiled_uv = fract(UV * tiling_factor);
    vec2 centerDistVec = vec2(0.5) - tiled_uv;
    float distToCircleEdge = length(centerDistVec) * radius;
    float distToSquareEdge = 0.5 * (0.5 - min(min(tiled_uv.x, 1.0 - tiled_uv.x), min(tiled_uv.y, 1.0 - tiled_uv.y)));
    float distToEdge = mix(distToCircleEdge, distToSquareEdge, shape);
    float gradient = smoothstep(0.5, 0.5 - radius, distToEdge);
    float noiseValue = texture(textureNoise, tiled_uv + noise_offset).r;
    float opacity = step(radius, mix(gradient, noiseValue, effectControl) - distToEdge);

    COLOR = texture(TEXTURE, UV) * texture(textureBackground, UV) * vec4(active_color.rgb, opacity);
}