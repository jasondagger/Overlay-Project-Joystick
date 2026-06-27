shader_type canvas_item;

uniform sampler2D particle_texture : source_color, filter_linear;
uniform vec4 color : source_color = vec4(1.0, 1.0, 1.0, 1.0);
uniform vec4 alt_color : source_color = vec4(1.0, 0.0, 0.0, 1.0);
uniform float audio_time; 
// Set this via script (e.g., get_viewport().get_visible_rect().size)
uniform vec2 screen_size = vec2(1920.0, 1080.0); 

varying float world_y;

void vertex() {
    world_y = (MODEL_MATRIX * vec4(VERTEX, 0.0, 1.0)).y;
}

float hash(vec2 p) {
    return fract(sin(dot(p, vec2(12.9898, 78.233))) * 43758.5453);
}

vec2 rotate(vec2 v, float a) {
    float s = sin(a); float c = cos(a);
    return vec2(v.x * c - v.y * s, v.x * s + v.y * c);
}

void fragment() {
    // 1. Calculate aspect ratio to normalize coordinates
    float aspect = screen_size.x / screen_size.y;
    vec2 corrected_uv = UV;
    corrected_uv.x *= aspect; 

    const float particle_density = 25.0; 
    const float base_size = 0.7; 
    const float rotation_speed = 0.8;   
    const float base_fall_speed = 0.5;

    vec4 active_base_color = color;
    vec2 g_uv = corrected_uv * particle_density;
    vec3 acc = vec3(0.0);
    float alpha_acc = 0.0;

    for (int y = -2; y <= 2; y++) {
        for (int x = -2; x <= 2; x++) {
            vec2 root_id = floor(g_uv) + vec2(float(x), float(y));
            float depth = hash(vec2(root_id.x, 31.4));
            
            float p_speed = (0.6 + hash(vec2(root_id.x, 88.1)) * 0.8) * mix(0.5, 1.2, depth) * base_fall_speed;
            float p_time = (audio_time * p_speed); 
            float v_move = p_time * -1.0; 
            
            vec2 cell_id = vec2(root_id.x, floor(root_id.y + v_move));
            float n = hash(cell_id);
            
            if (n < 0.8) continue; 

            float t_p = p_time + n * 50.0;
            float final_size = base_size * mix(0.7, 1.2, depth);
            
            float sway_frequency = 0.3 + depth * 0.2; 
            float sway = sin(t_p * sway_frequency + n * 6.28) * 1.5;
            
            // 2. Position and rotate using the corrected coordinate space
            vec2 p_center = root_id + 0.5 + vec2(sway, fract(-v_move));
            vec2 local_uv = g_uv - p_center;

            float spin = (n * 6.28) + (t_p * sign(n - 0.5) * rotation_speed);
            vec2 t_uv = rotate(local_uv, spin) / max(final_size, 0.001) + 0.5;

            if(t_uv.x >= 0.0 && t_uv.x <= 1.0 && t_uv.y >= 0.0 && t_uv.y <= 1.0) {
                vec4 samp = texture(particle_texture, t_uv);
                
                float dist = length(t_uv - vec2(0.5));
                float glow = exp(-4.5 * dist) * 2.0;
                
                vec3 final_rgb = active_base_color.rgb * (1.0 + glow);
                vec4 p_color = vec4(final_rgb, active_base_color.a) * mix(0.6, 1.0, depth);
                
                acc = mix(acc, samp.rgb * p_color.rgb, samp.a);
                alpha_acc = max(alpha_acc, samp.a);
            }
        }
    }
    COLOR = vec4(acc, alpha_acc);
}