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

float sdEllipsoid(vec3 p, vec3 r) {
    float k0 = length(p/r);
    float k1 = length(p/(r*r));
    return k0*(k0-1.0)/k1;
}

float sdCone(vec3 p, float h, float r) {
    vec2 q = vec2(length(p.xz), p.y);
    vec2 mantle = vec2(r, -h);
    float m2 = dot(mantle, mantle);
    float res = dot(q - vec2(0.0, h), mantle) / m2;
    return length(q - vec2(0.0, h) - mantle * clamp(res, 0.0, 1.0));
}

float sdCapsule(vec3 p, vec3 a, vec3 b, float r) {
    vec3 pa = p - a, ba = b - a;
    float h = clamp(dot(pa, ba) / dot(ba, ba), 0.0, 1.0);
    return length(pa - ba * h) - r;
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

// Helper for stylized wood shape
float sdStylizedBox(vec2 p, vec2 b, float r) {
    vec2 d = abs(p) - b;
    return length(max(d, 0.0)) + min(max(d.x, d.y), 0.0) - r;
}

vec2 map(vec3 p, out vec3 local_p) {
    // 1. Restore the winding to match your terrain
    float scroll_z = p.z + (audio_time * base_scroll_speed * 2.0);
    float winding = sin(scroll_z * 0.05) * 3.0; 
    float road_dist = abs(p.x - winding) - 11.0;
    
    float ground_base = -4.5;
    float rail_y = ground_base + 3.0;

    // 2. Thinner horizontal rail
    float rail = length(vec2(road_dist, p.y - rail_y)) - 0.18;

    // 3. Spindly wooden posts
    float post_z = mod(scroll_z + 3.0, 6.0) - 3.0;
    
    // Taper: 0.07 at base (very thin) to 0.3 at top
    float height_factor = clamp((p.y - ground_base) / 4.0, 0.0, 1.0);
    float thickness = mix(0.07, 0.3, height_factor);
    
    // Wonky wood shape
    float wood_lean = sin(p.y * 5.0 + scroll_z) * 0.08;
    float posts = sdStylizedBox(vec2(road_dist + wood_lean, post_z), vec2(thickness), 0.05);
    
    // Make poles taller than the rail (rail_y + 0.8)
    posts = max(posts, p.y - (rail_y + 0.8));
    posts = max(posts, -(p.y - ground_base));

    // 4. Combine with a "molded" look
    float geometry = smin(rail, posts, 0.2);

    local_p = vec3(p.x - winding, p.y, scroll_z) * 5.0;
    return vec2(geometry * 0.5, 2.0);
}

void fragment() {
    float ratio = SCREEN_PIXEL_SIZE.x / SCREEN_PIXEL_SIZE.y;
    vec2 p_ndc = (UV - 0.5) * 2.0;
    p_ndc.x *= ratio;

    vec3 ro = vec3(anchor, 1.5, -5.0); 
    vec3 rd = normalize(vec3(p_ndc.x * 1.5, -p_ndc.y + 0.3725, 0.9)); 

    float t = 0.01;
    float hit_id = -1.0;
    vec3 hit_lp = vec3(0.0);
    bool hit_core = false;
    float min_d = 1000.0;
    float ground_base_y = -4.5;

    for (int i = 0; i < 80; i++) {
        vec3 p = ro + rd * t;
        vec3 lp;
        vec2 d = map(p, lp);
        
        float scroll_z = p.z + (audio_time * base_scroll_speed * 2.0);
        float winding = sin(scroll_z * 0.05) * 3.0; 
        float road_dist = abs(p.x - winding);
        
        // --- PRECISION FIX ---
        // Only track min_d if we are close to a surface. 
        // This prevents the "huge square culling" at the edges.
        if (d.x < 0.2) { 
            min_d = min(min_d, d.x);
        }

        if (d.x < 0.001) { 
            hit_core = true; 
            hit_id = d.y; 
            hit_lp = lp;
            break; 
        }
        
        // Raymarch acceleration: use 0.4 instead of 0.25 to push through 
        // the large empty space of the trees faster.
        t += d.x * 2.0; 
        if (t >= 80.0) break;
    }

    float intensity = clamp(music_intensity, 0.0, 1.0);

    if (hit_core) {
        vec2 pat_uv;
        if (abs(hit_id - 1.0) < 0.1) {
            pat_uv = hit_lp.xz; 
        } else if (hit_id > 1.5) {
            pat_uv = hit_lp.xy + hit_lp.zy; 
        } else {
            pat_uv = hit_lp.xy;
        }

        int c_ids[5] = int[](current_slot_0, current_slot_1, current_slot_2, current_slot_3, current_slot_4);
        int n_ids[5] = int[](next_slot_0, next_slot_1, next_slot_2, next_slot_3, next_slot_4);
        float combined_pat = 0.0;
        for (int j = 0; j < 5; j++) {
            float pa = get_pattern_mask(c_ids[j], pat_uv * 0.25, audio_time); 
            float pb = get_pattern_mask(n_ids[j], pat_uv * 0.25, audio_time);
            combined_pat = max(combined_pat, mix(pa, pb, transition_weight));
        }

        vec3 base_col = mix(alt_color.rgb, color.rgb, combined_pat);
        
        // Instead of multiplying by br, we boost vibrancy
        // This keeps the colors deep while making them glow
        vec3 vibrant = mix(base_col, base_col * base_col, -intensity * 0.5);
        vibrant *= (1.0 + intensity * 0.3); 
        
        COLOR = vec4(vibrant, battery);
    } else if (min_d < 0.04) { 
        // Outlines get a slight white-mix instead of just getting brighter
        vec3 edge_col = mix(alt_color.rgb, vec3(1.0), intensity * 0.4);
        COLOR = vec4(edge_col, battery);
    } else {
        discard;
    }
}