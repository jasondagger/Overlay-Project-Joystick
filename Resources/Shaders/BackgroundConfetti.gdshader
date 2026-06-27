shader_type canvas_item;

uniform sampler2D confetti_texture : source_color, filter_linear;
uniform vec4 confetti_color : source_color = vec4(1.0, 1.0, 1.0, 1.0);
uniform vec4 alt_color : source_color = vec4(1.0, 0.0, 0.0, 1.0);

uniform float audio_offset; 
uniform float base_fall_speed : hint_range(0.0, 10.0) = 2.0;

uniform float atlas_columns : hint_range(1.0, 16.0) = 6.0;
uniform float atlas_rows : hint_range(1.0, 16.0) = 6.0;

uniform float particle_density : hint_range(10.0, 200.0) = 45.0;
uniform float base_size : hint_range(0.01, 2.0) = 0.35; 
uniform float rotation_speed : hint_range(0.0, 20.0) = 4.0;
uniform float glow_intensity : hint_range(1.0, 10.0) = 2.5; 

uniform float stutter_intensity : hint_range(0.0, 1.0) = 0.4;
uniform float stutter_speed : hint_range(1.0, 30.0) = 12.0;
uniform float feather : hint_range(0.0, 200.0) = 50.0; 

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
    float top_line = 286.0;
    float bottom_line = 794.0;

    // m_mid is 1.0 in the center band, 0.0 on TOP and BOTTOM edges.
    float m_mid = smoothstep(top_line - feather, top_line + feather, world_y) 
                * (1.0 - smoothstep(bottom_line - feather, bottom_line + feather, world_y));
    
    // Inverting the logic as requested: 
    // Alt Color on the outside (Top/Bottom), Confetti Color in the middle.
    vec4 active_base_color = mix(confetti_color, alt_color, m_mid);
    
    vec2 g_uv = UV * particle_density;
    vec3 acc = vec3(0.0);
    float alpha_acc = 0.0;

    for (int y = -1; y <= 1; y++) {
        for (int x = -1; x <= 1; x++) {
            vec2 root_id = floor(g_uv) + vec2(float(x), float(y));
            float depth = hash(vec2(root_id.x, 31.4));
            
            float p_speed = (0.6 + hash(vec2(root_id.x, 88.1)) * 0.8) * mix(0.5, 1.2, depth);
            float p_time = (audio_offset * p_speed); 
            float v_move = p_time * -1.0; 
            
            vec2 cell_id = vec2(root_id.x, floor(root_id.y + v_move));
            float n = hash(cell_id);
            
            if (n < 0.75) continue; 

            float t_p = p_time + n * 50.0;
            float stepped_t = floor(t_p * stutter_speed) / stutter_speed;
            float t_m = mix(t_p, stepped_t, stutter_intensity);
            
            // --- DEPTH PULSE LOGIC ---
            // This creates a wave that fluctuates between 0.5 and 1.5
            float pulse = sin(t_m * (0.5 + n) + n * 10.0) * 0.5 + 1.0;
            
            // Apply pulse to size and a subtle brightness boost when "closer"
            float final_size = base_size * (0.6 + hash(cell_id + 21.0) * 0.8) * mix(0.6, 1.1, depth) * pulse;
            float depth_glow = mix(0.7, 1.3, pulse * 0.5); 
            
            float sway = sin(t_m * (1.5 + hash(cell_id + 0.5) * 2.0) + n * 6.28) * (0.1 + hash(cell_id + 0.8) * 0.3);
            vec2 p_center = root_id + 0.5 + vec2(sway, fract(-v_move));
            vec2 local_uv = g_uv - p_center;

            float spin = (n * 6.28) + (t_m * sign(n - 0.5) * (0.5 + hash(cell_id + 0.1) * 1.5) * rotation_speed);
            float f_x = mix(0.4, 1.0, abs(cos(t_m * (1.5 + n))));
            float f_y = mix(0.4, 1.0, abs(sin(t_m * (1.2 + n))));

            vec2 t_uv = rotate(local_uv / vec2(f_x, f_y), spin) / max(final_size, 0.001) + 0.5;

            if(t_uv.x >= 0.0 && t_uv.x <= 1.0 && t_uv.y >= 0.0 && t_uv.y <= 1.0) {
                vec2 atlas_uv = t_uv / vec2(atlas_columns, atlas_rows);
                atlas_uv += vec2(floor(hash(cell_id + 5.6) * atlas_columns) / atlas_columns, 
                                 floor(hash(cell_id + 9.2) * atlas_rows) / atlas_rows);
                
                vec4 samp = texture(confetti_texture, atlas_uv);
                // Glow also reacts to the pulse to sell the 3D effect
                vec3 p_color = active_base_color.rgb * mix(0.3, 1.0, depth) * glow_intensity * depth_glow;
                
                acc = mix(acc, samp.rgb * p_color, samp.a);
                alpha_acc = max(alpha_acc, samp.a);
            }
        }
    }
    COLOR = vec4(acc, alpha_acc);
}