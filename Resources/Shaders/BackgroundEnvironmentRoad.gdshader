shader_type canvas_item;

uniform float battery : hint_range(0.0, 1.0) = 1.0;
uniform float anchor : hint_range(-10.0, 10.0) = 0.0; 
uniform float fov : hint_range(0.01, 1.0) = 0.2;
uniform vec4 color : source_color = vec4(1.0, 0.5, 1.0, 1.0);
uniform vec4 alt_color : source_color = vec4(1.0, 0.5, 1.0, 1.0);
uniform float audio_offset; 
uniform float audio_time; 

uniform float grid_size : hint_range(0.1, 40.0) = 15.0; 
uniform float base_scroll_speed : hint_range(0.0, 5.0) = 1.2; 
uniform float line_thickness : hint_range(0.01, 1.0) = 0.15;
uniform vec4 background_color : source_color = vec4(0.0, 0.05, 0.1, 1.0);

uniform int current_slot_0;
uniform int current_slot_1; 
uniform int current_slot_2;
uniform int current_slot_3;
uniform int current_slot_4;
uniform int next_slot_0;
uniform int next_slot_1; 
uniform int next_slot_2;
uniform int next_slot_3;
uniform int next_slot_4;
uniform float transition_weight : hint_range(0.0, 1.0) = 0.0;
uniform float music_intensity;
uniform int model; 


// --- UTILS ---
float hash_stable(vec2 uv) { return fract(sin(dot(uv, vec2(12.9898, 78.233))) * 43758.5453123); }
float smin(float a, float b, float k) {
    float h = clamp(0.5 + 0.5 * (b - a) / k, 0.0, 1.0);
    return mix(b, a, h) - k * h * (1.0 - h);
}

float get_pattern_mask(int id, vec2 p, float time) {
    vec2 uv = fract(p * 0.5); 
    vec2 p_center = fract(p) - 0.5;
    float r = length(p_center);
    float a = atan(p_center.y, p_center.x);
    
    float t = mod(time, 62.83);
    float t_fast = mod(time * 4.0, 62.83);
    float t_slow = mod(time * 0.5, 62.83);
    float t_sin = sin(t * 1.5);
    
    switch (id) {
        case 0: { return step(0.3 + t_sin * 0.1, r); }
        case 1: { return step(abs(r - mod(t * 0.3, 0.7)), 0.08); }
        case 2: { vec2 q = fract(p * 0.8 + vec2(t * 0.5, 0)); return step(length(q - 0.5), 0.3); }
        case 3: { return step(0.15, abs(sin(r * 12.0 - t_fast))); }
        case 4: { float v = step(0.5, sin(p.x * 4.0 + t) * sin(p.y * 4.0 + t)); return v; }
        case 5: { vec2 off = vec2(cos(t), sin(t)) * 0.3; return step(length(p_center - off), 0.2); }
        case 6: { float v = sin(p.x * 6.0 + t) + cos(p.y * 6.0 + t); return step(0.8, v); }
        case 7: { float v = sin(p.x * 3.0 - t_fast) * cos(p.y * 5.0 + t); return step(0.2, v); }
        case 8: { vec2 q = p * 2.0; return step(0.6, sin(q.x + t + sin(q.y + t))); }
        case 9: { float v = r + sin(a * 4.0 + t_fast) * 0.2; return step(0.4, v); }
        case 10: { return step(0.5, sin(p.x * 8.0 + sin(p.y * 6.0 + t_fast))); }
        case 11: { float rot = t; mat2 m = mat2(vec2(cos(rot), sin(rot)), vec2(-sin(rot), cos(rot)));
                   vec2 q = m * p_center; return step(max(abs(q.x)*0.866 + q.y*0.5, -q.y), 0.25); }
        case 12: { vec2 g = fract(p * 3.0 + t * 0.4); return step(g.x, g.y); }
        case 13: { return step(max(abs(p_center.x), abs(p_center.y)), 0.3 + t_sin * 0.1); }
        case 14: { vec2 q = fract(p * 2.0 + t * 0.2); return step(0.5, abs(q.x - 0.5) + abs(q.y - 0.5)); }
        case 15: { float d = abs(p_center.x) + abs(p_center.y); return step(abs(d - mod(t * 0.5, 1.0)), 0.1); }
        case 16: { vec2 g = step(0.15, fract(p * 4.0 + t * 0.2)); return 1.0 - (g.x * g.y); }
        case 17: { vec2 g = fract(p * 6.0 + vec2(0, t_fast)); return step(0.85, g.y); }
        case 18: { return step(0.4, sin(p.x * 12.0 + t) * sin(p.y * 12.0 + t)); }
        case 19: { vec2 q = floor(p * 5.0 + t * 0.2); return step(0.8, hash_stable(q + floor(t_fast))); }
        case 20: { return step(0.5, mod(floor(p.x * 2.0 + t) + floor(p.y * 2.0), 2.0)); }
        case 21: { return step(0.7, sin(p.x * 15.0 + t_fast)); }
        case 22: { return step(0.7, sin((p.x + p.y) * 10.0 - t_fast)); }
        case 23: { return step(abs(p_center.x - sin(t)*0.35), 0.1); }
        case 24: { return step(0.9, fract(p.y * 6.0 + t_fast)); }
        case 25: { return step(0.8, sin(p.y * 25.0 + t_fast)); }
        case 26: { return step(0.0, sin(a * 4.0 + t_fast)); }
        case 27: { return step(0.85, sin(a * 2.0 - r * 10.0 + t_fast)); }
        case 28: { return step(0.5, sin(6.0 / (r + 0.02) + t_fast)); }
        case 29: { return step(0.9, sin(a * 10.0 + t_slow)); }
        case 30: { return step(0.7, cos(a * 6.0 + t) * sin(r * 12.0 - t)); }
        case 31: { return step(0.95, hash_stable(p + mod(t, 1.0))); }
        case 32: { return step(0.7, hash_stable(vec2(floor(p.y * 10.0), floor(t_fast)))); }
        case 33: { return step(0.8, fract(p.x * 10.0 + t_fast)); }
        case 34: { float v = sin(p.x * 30.0 + t_fast) * hash_stable(floor(p.yy * 10.0)); return step(0.3, v); }
        case 35: { return step(0.8, sin(p.x * 15.0 + t) * sin(p.y * 15.0 - t)); }
        case 36: { return step(r, 0.35 + t_sin * 0.1); }
        case 37: { vec2 q = fract(p * 1.0 + t * 0.1); return step(length(q - 0.5), 0.45); }
        case 38: { vec2 q = fract(p * 8.0 - t * 0.2); return step(length(q - 0.5), 0.2); }
        case 39: { return step(abs(p_center.x * p_center.y), 0.01 + abs(t_sin) * 0.02); }
        case 40: { return step(0.8, fract(r * 3.0 - t) + fract(a * 1.0 + t)); }
        case 41: { return step(0.7, sin(p.x*8.0+t)*sin(p.y*8.0-t)*sin(r*15.0)); }
        case 42: { vec2 q = floor(p * 6.0 + t * 0.2); return step(0.5, hash_stable(q)); }
        case 43: { return step(r, hash_stable(p + t_fast) * 0.5); }
        case 44: { return step(0.8, sin(r * 40.0 - t_fast)); }
        case 45: { vec2 rot_p = vec2(p_center.x*cos(t)-p_center.y*sin(t), p_center.x*sin(t)+p_center.y*cos(t)); 
                   return step(max(abs(rot_p.x), abs(rot_p.y)), 0.2); }
        case 46: { float v = mod(floor(p.x*1.5+t)+floor(p.y*1.5), 2.0); return v * step(0.3, r); }
        case 47: { return step(0.7, sin(a * 3.0 + t) * sin(r * 20.0 - t_fast)); }
        case 48: { vec2 g = abs(fract(p*1.5 + vec2(t * 0.1, 0)) - 0.5); return step(0.4, max(g.x, g.y)); }
        case 49: { vec2 q = p_center; q.x /= (1.2+t_sin*0.4); return step(length(q), 0.25); }
        default: return 0.0;
    }
}

float noise(vec2 uv) {
    vec2 i = floor(uv);
    vec2 f = fract(uv);
    float a = hash_stable(i);
    float b = hash_stable(i + vec2(1.0, 0.0));
    float c = hash_stable(i + vec2(0.0, 1.0));
    float d = hash_stable(i + vec2(1.0, 1.0));
    vec2 u = f * f * (3.0 - 2.0 * f);
    return mix(a, b, u.x) + (c - a) * u.y * (1.0 - u.x) + (d - b) * u.x * u.y;
}

float smax(float a, float b, float k) {
    return smin(a, b, -k);
}

float map(vec3 p) {
    float scroll_z = p.z + (audio_time * base_scroll_speed * 2.0);
    float winding = sin(scroll_z * 0.05) * 3.0;
    float road_width = 15.5;
    float road_dist = abs(p.x - winding) - road_width;
    float road_surface = p.y - (-4.1); 
    return max(road_surface, road_dist);
}

void fragment() {
    float ratio = SCREEN_PIXEL_SIZE.x / SCREEN_PIXEL_SIZE.y;
    vec2 p_ndc = (UV - 0.5) * 2.0;
    p_ndc.x *= ratio;

    vec3 ro = vec3(anchor, 1.5, -5.0);
    vec3 rd = normalize(vec3(p_ndc.x * 1.5, -p_ndc.y + 0.375, 0.9));

    float t_march = 0.01;
    vec3 hit_p;
    bool hit = false;
    for (int i = 0; i < 160; i++) {
        vec3 p = ro + rd * t_march;
        float d = map(p);
        if (d < 0.001) { hit = true; hit_p = p; break; }
        t_march += d;
        if (t_march > 120.0) break;
    }

    if (hit) {
        float scroll_z = hit_p.z + (audio_time * base_scroll_speed * 2.0);
        float winding = sin(scroll_z * 0.05) * 3.0;
        
        float local_x = hit_p.x - winding;
        vec2 grid_freq = vec2(5.5, 25.0); 
        vec2 grid_uv = vec2(local_x, scroll_z) / grid_freq;
        vec2 f_uv = fract(grid_uv);
        vec2 dist_to_line = abs(f_uv - 0.5);
        
        // 1. Balanced Lighting
        float bottom_flare = pow(UV.y, 3.5) * 2.8; 
        float horizon_fade = pow(UV.y, 1.5); 
        
        vec3 col_a = mix(color.rgb, vec3(1.0), 0.08);
        vec3 col_b = mix(alt_color.rgb, vec3(1.0), 0.08);
        
        // 2. Thickness Split
        float thick_v = 0.35 * line_thickness; 
        float thick_h = 0.12 * line_thickness; 
        
        // CORE: Solid center
        float core = clamp(smoothstep(thick_v, thick_v * 0.8, dist_to_line.x) + 
                           smoothstep(thick_h, thick_h * 0.8, dist_to_line.y), 0.0, 1.0);
        
        // OUTLINE: Reduced from 1.6 to 1.25 for a thinner look
        float outline_w = 1.25; 
        float outline = clamp(smoothstep(thick_v * outline_w, thick_v * 0.9, dist_to_line.x) + 
                              smoothstep(thick_h * outline_w, thick_h * 0.9, dist_to_line.y), 0.0, 1.0);
        
        // GLOW: Restored falloff and visibility
        float glow = clamp(smoothstep(thick_v * 4.5, 0.0, dist_to_line.x) + 
                           smoothstep(thick_h * 4.5, 0.0, dist_to_line.y), 0.0, 1.0);
        
        // 3. Scrolling Pattern
        float pat = 0.0;
        vec2 pattern_uv = (grid_uv * 24.0) + vec2(TIME * 0.2, TIME * 0.1);
        int c_ids[5] = int[](current_slot_0, current_slot_1, current_slot_2, current_slot_3, current_slot_4);
        int n_ids[5] = int[](next_slot_0, next_slot_1, next_slot_2, next_slot_3, next_slot_4);
        for (int j = 0; j < 5; j++) {
            float pa = get_pattern_mask(c_ids[j], pattern_uv + float(j), audio_time);
            float pb = get_pattern_mask(n_ids[j], pattern_uv + float(j), audio_time);
            pat = max(pat, mix(pa, pb, transition_weight));
        }

        // 4. Final Composite
        vec3 glow_base = mix(col_a, col_b, 0.5); 
        vec3 static_glow_rgb = glow_base * bottom_flare * horizon_fade;
        
        vec3 outline_rgb = col_b * (bottom_flare * horizon_fade);
        vec3 core_base = mix(col_a, col_b, pat);
        vec3 pulsing_core_rgb = core_base * (bottom_flare + (music_intensity * 0.9)) * horizon_fade;

        // --- THE FIX: Layering with transparency support ---
        // 1. Start with background
        vec3 final_rgb = background_color.rgb;
        // 2. Add glow (increased mix factor back to 0.35)
        final_rgb = mix(final_rgb, static_glow_rgb, glow * 0.35);
        // 3. Add outline
        final_rgb = mix(final_rgb, outline_rgb, outline);
        // 4. Add core
        final_rgb = mix(final_rgb, pulsing_core_rgb, core);
        
        float sharp_clip = step(t_march, 120.0);
        COLOR = vec4(final_rgb, sharp_clip * battery);
    }
    else {
        discard;
    }
}