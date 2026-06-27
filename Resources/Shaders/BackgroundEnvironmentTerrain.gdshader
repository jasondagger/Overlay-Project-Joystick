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

float map(vec3 p) {
    float scroll_z = p.z + (audio_time * base_scroll_speed * 2.0);
    float winding = sin(scroll_z * 0.05) * 3.0;
    
    float road_width = 13.5; 
    float volume_clip = -5.0; 

    float road_dist = abs(p.x - winding) - road_width; 

    // Keep heights modest
    vec2 uv = vec2(p.x - winding, scroll_z) * 0.08;
    float h = noise(uv) * 2.0 + noise(uv * 2.5) * 0.8; 
    
    // Use a smooth mask to ensure the SDF gradient isn't a vertical cliff
    // A cliff of 0 width confuses the raymarcher; 1.0 width is safer.
    float height_mask = clamp(road_dist * 1.0, 0.0, 1.0);
    float terrain_y = volume_clip + (h * height_mask);
    
    // Vertical distance to surface
    float d_surface = p.y - terrain_y;
    // Distance to bottom floor
    float d_floor = -(p.y - volume_clip);
    
    // Combine surface and floor
    float d_volume = max(d_surface, d_floor);

    // Final distance calculation
    // Maxing with -road_dist ensures the road is empty space.
    float d_final = max(d_volume, -road_dist);

    // CRITICAL: Return a fraction of the distance. 
    // This slows the ray down so it cannot "skip" over peaks.
    return d_final * 0.75; 
}

void fragment() {
    float ratio = SCREEN_PIXEL_SIZE.x / SCREEN_PIXEL_SIZE.y;
    vec2 p_ndc = (UV - 0.5) * 2.0;
    p_ndc.x *= ratio;

    vec3 ro = vec3(anchor, 1.5, -5.0); 
    // Restored your exact original ray direction logic
    vec3 rd = normalize(vec3(p_ndc.x * 1.5, -p_ndc.y + 0.365, 0.9)); 

    float t = 0.01;
    vec3 hit_p;
    bool hit = false;
    float m_dist = 1000.0;

    for (int i = 0; i < 256; i++) {
        vec3 p = ro + rd * t;
        float d = map(p);
        m_dist = min(m_dist, abs(d));

        if (d < 0.0001) { 
            // Using your accurate height gate (1.4) to keep geometry from shifting.
            // Strict rd.y check ensures we don't hit the "lid" above the camera.
            if (rd.y < -0.02) { 
                hit = true; 
                hit_p = p; 
                break; 
            } else {
                t += 0.2; 
                continue;
            }
        }
        
        // The fix for expansion clipping: use a smaller step (0.4) for the cliffs 
        // nearby and a full step (1.0) for the distance to prevent alpha gaps.
        t += d * (t < 20.0 ? 0.4 : 1.0); 
        if (t > 100.25) break;
    }

    // --- ANTI-WASHOUT LOGIC ---
    // Clamping intensity ensures a predictable peak for the color math
    float intensity = clamp(music_intensity, 0.0, 1.0);

    if (hit) {
        float scroll_z = hit_p.z + (audio_time * base_scroll_speed * 2.0);
        vec2 p_uv = vec2(hit_p.x - (sin(scroll_z * 0.05) * 3.0), scroll_z) * 0.35;
        
        int c_ids[5] = int[](current_slot_0, current_slot_1, current_slot_2, current_slot_3, current_slot_4);
        int n_ids[5] = int[](next_slot_0, next_slot_1, next_slot_2, next_slot_3, next_slot_4);
        
        float pat = 0.0;
        for (int j = 0; j < 5; j++) {
            float pa = get_pattern_mask(c_ids[j], p_uv * 4.0, audio_time); 
            float pb = get_pattern_mask(n_ids[j], p_uv * 4.0, audio_time);
            pat = max(pat, mix(pa, pb, transition_weight));
        }

        vec3 base_col = mix(color.rgb, alt_color.rgb, pat);
        
        // --- VIBRANCY BOOST ---
        // We "deepen" the colors by mixing with their own square based on intensity.
        // This makes the terrain look "richer" rather than just "brighter".
        vec3 vibrant_col = mix(base_col, base_col * base_col, -intensity * 0.5);
        
        // Final mild brightness lift (0.3 instead of 0.4) to keep it within gamut
        vibrant_col *= (1.0 + intensity * 0.3);
        
        COLOR = vec4(vibrant_col, battery);
    } else if (m_dist < 0.12) { 
        // For the outlines, we mix toward white to create a glow 
        // instead of just scaling the numbers.
        vec3 edge_glow = mix(color.rgb, vec3(1.0), intensity * 0.5);
        COLOR = vec4(edge_glow, battery);
    } else {
        discard;
    }
}