shader_type canvas_item;

uniform float battery : hint_range(0.0, 1.0) = 1.0;
uniform float anchor : hint_range(-10.0, 10.0) = 0.0; 
uniform float fov : hint_range(0.01, 1.0) = 0.2;
uniform vec4 background_color : source_color = vec4(0.0, 0.1, 0.2, 1.0);
uniform vec4 color : source_color = vec4(1.0, 0.5, 1.0, 1.0);
uniform vec4 alt_color : source_color = vec4(1.0, 0.5, 1.0, 1.0);
uniform float audio_offset; 

uniform float grid_size : hint_range(0.1, 40.0) = 15.0; 
uniform float line_thickness : hint_range(0.01, 20.0) = 8.0; 
uniform float base_scroll_speed : hint_range(-5, 5.0) = 1.2; 

uniform float audio_time;
uniform float music_intensity;
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

float hash_stable(vec2 uv) {
    return fract(sin(dot(uv, vec2(12.9898, 78.233))) * 43758.5453123);
}

float get_pattern_mask(int id, vec2 p, float time) {
    // We use a slightly larger fractional grid to keep patterns "bold"
    vec2 uv = fract(p * 0.5); 
    vec2 p_center = fract(p) - 0.5;
    float r = length(p_center);
    float a = atan(p_center.y, p_center.x);
    
    // Normalized time for different frequencies
    float t = mod(time, 62.83);
    float t_fast = mod(time * 4.0, 62.83);
	float t_slow = mod(time * 0.5, 62.83);
    float t_sin = sin(t * 1.5);
	
    switch (id) {
        // --- BOLD GEOMETRIC MOVEMENT ---
        case 0: { return step(0.3 + t_sin * 0.1, r); } // Pulsing Void
        case 1: { return step(abs(r - mod(t * 0.3, 0.7)), 0.08); } // Heavy Rings
        case 2: { vec2 q = fract(p * 0.8 + vec2(t * 0.5, 0)); return step(length(q - 0.5), 0.3); } // Big Sliding Orbs
        case 3: { return step(0.15, abs(sin(r * 12.0 - t_fast))); } // High-Contrast Ripples
        case 4: { float v = step(0.5, sin(p.x * 4.0 + t) * sin(p.y * 4.0 + t)); return v; } // Massive Checkers
        case 5: { vec2 off = vec2(cos(t), sin(t)) * 0.3; return step(length(p_center - off), 0.2); } // Heavy Satellite

        // --- LIQUID & OVERDRIVE FLOW ---
        case 6: { float v = sin(p.x * 6.0 + t) + cos(p.y * 6.0 + t); return step(0.8, v); } // Thick Lava
        case 7: { float v = sin(p.x * 3.0 - t_fast) * cos(p.y * 5.0 + t); return step(0.2, v); } // Plasma Globs
        case 8: { vec2 q = p * 2.0; return step(0.6, sin(q.x + t + sin(q.y + t))); } // Heavy Sludge
        case 9: { float v = r + sin(a * 4.0 + t_fast) * 0.2; return step(0.4, v); } // Warping Blob
        case 10: { return step(0.5, sin(p.x * 8.0 + sin(p.y * 6.0 + t_fast))); } // Deep Water

        // --- HARD-EDGE POLYGONS ---
        case 11: { float rot = t; mat2 m = mat2(vec2(cos(rot), sin(rot)), vec2(-sin(rot), cos(rot)));
                   vec2 q = m * p_center; return step(max(abs(q.x)*0.866 + q.y*0.5, -q.y), 0.25); } // Spin Triangle
        case 12: { vec2 g = fract(p * 3.0 + t * 0.4); return step(g.x, g.y); } // Bold Tri-Tiles
        case 13: { return step(max(abs(p_center.x), abs(p_center.y)), 0.3 + t_sin * 0.1); } // Breathing Cube
        case 14: { vec2 q = fract(p * 2.0 + t * 0.2); return step(0.5, abs(q.x - 0.5) + abs(q.y - 0.5)); } // Diamond Plate
        case 15: { float d = abs(p_center.x) + abs(p_center.y); return step(abs(d - mod(t * 0.5, 1.0)), 0.1); } // Heavy Diamond Echo

        // --- GRID & TECH ---
        case 16: { vec2 g = step(0.15, fract(p * 4.0 + t * 0.2)); return 1.0 - (g.x * g.y); } // Bold Wireframe
        case 17: { vec2 g = fract(p * 6.0 + vec2(0, t_fast)); return step(0.85, g.y); } // Heavy Matrix
        case 18: { return step(0.4, sin(p.x * 12.0 + t) * sin(p.y * 12.0 + t)); } // Vibrating Blocks
        case 19: { vec2 q = floor(p * 5.0 + t * 0.2); return step(0.8, hash_stable(q + floor(t_fast))); } // Heavy Tiles
        case 20: { return step(0.5, mod(floor(p.x * 2.0 + t) + floor(p.y * 2.0), 2.0)); } // Big Sliding Checkers

        // --- SCANLINES & ENERGY ---
        case 21: { return step(0.7, sin(p.x * 15.0 + t_fast)); } // Power Bars
        case 22: { return step(0.7, sin((p.x + p.y) * 10.0 - t_fast)); } // Diagonal Energy
        case 23: { return step(abs(p_center.x - sin(t)*0.35), 0.1); } // Heavy Pillar
        case 24: { return step(0.9, fract(p.y * 6.0 + t_fast)); } // Warp Drive
        case 25: { return step(0.8, sin(p.y * 25.0 + t_fast)); } // Blinds

        // --- ROTATION & VORTEX ---
        case 26: { return step(0.0, sin(a * 4.0 + t_fast)); } // Heavy Fan
        case 27: { return step(0.85, sin(a * 2.0 - r * 10.0 + t_fast)); } // Thick Vortex
        case 28: { return step(0.5, sin(6.0 / (r + 0.02) + t_fast)); } // Heavy Wormhole
        case 29: { return step(0.9, sin(a * 10.0 + t_slow)); } // Scanning Beams
        case 30: { return step(0.7, cos(a * 6.0 + t) * sin(r * 12.0 - t)); } // Spiral Shield

        // --- GLITCH & NOISE ---
        case 31: { return step(0.95, hash_stable(p + mod(t, 1.0))); } // Thick Static
        case 32: { return step(0.7, hash_stable(vec2(floor(p.y * 10.0), floor(t_fast)))); } // Block Glitch
        case 33: { return step(0.8, fract(p.x * 10.0 + t_fast)); } // Bold Barcode
        case 34: { float v = sin(p.x * 30.0 + t_fast) * hash_stable(floor(p.yy * 10.0)); return step(0.3, v); } // Tech Circuit
        case 35: { return step(0.8, sin(p.x * 15.0 + t) * sin(p.y * 15.0 - t)); } // Clash Mesh

        // --- VARIED SCALES ---
        case 36: { return step(r, 0.35 + t_sin * 0.1); } // Heartbeat Circle
        case 37: { vec2 q = fract(p * 1.0 + t * 0.1); return step(length(q - 0.5), 0.45); } // Massive Orbs
        case 38: { vec2 q = fract(p * 8.0 - t * 0.2); return step(length(q - 0.5), 0.2); } // Medium Bubbles
        case 39: { return step(abs(p_center.x * p_center.y), 0.01 + abs(t_sin) * 0.02); } // Thick Cross
        case 40: { return step(0.8, fract(r * 3.0 - t) + fract(a * 1.0 + t)); } // Heavy Compass

        // --- SPECIALTY ---
        case 41: { return step(0.7, sin(p.x*8.0+t)*sin(p.y*8.0-t)*sin(r*15.0)); } // Interference Force
        case 42: { vec2 q = floor(p * 6.0 + t * 0.2); return step(0.5, hash_stable(q)); } // Sliding Bit-Blocks
        case 43: { return step(r, hash_stable(p + t_fast) * 0.5); } // Dissolving Sun
        case 44: { return step(0.8, sin(r * 40.0 - t_fast)); } // Concentric Blast
        case 45: { vec2 rot_p = vec2(p_center.x*cos(t)-p_center.y*sin(t), p_center.x*sin(t)+p_center.y*cos(t)); 
                   return step(max(abs(rot_p.x), abs(rot_p.y)), 0.2); } // Spinning Core
        case 46: { float v = mod(floor(p.x*1.5+t)+floor(p.y*1.5), 2.0); return v * step(0.3, r); } // Jumbo Checkers
        case 47: { return step(0.7, sin(a * 3.0 + t) * sin(r * 20.0 - t_fast)); } // Heavy Bloom
        case 48: { vec2 g = abs(fract(p*1.5 + vec2(t * 0.1, 0)) - 0.5); return step(0.4, max(g.x, g.y)); } // Gateways
        case 49: { vec2 q = p_center; q.x /= (1.2+t_sin*0.4); return step(length(q), 0.25); } // Flex-Oval
        
        default: return 0.0;
    }
}

void fragment() {
    float global_fade = smoothstep(1.0, 0.7, UV.y);
    float horizon_intensity = pow(1.0 - UV.y, 2.0) * 2.5 + 1.0;
    
    // Grid Time Calculation
    float newoffset = -audio_offset;
    float grid_time = (-TIME * base_scroll_speed) - (newoffset * 0.1);
    
    float p_y = 4.0 / (abs((1.0 - UV.y) + fov) + 0.02); 
    vec2 grid_uv = vec2((UV.x - 0.5) * (p_y * 1.25) + anchor, p_y);
    
    // 1. Grid Geometry Logic
    // We calculate this early so we can use the same vertical mapping for patterns
    vec2 scaled_grid = grid_uv / vec2(grid_size, grid_size * 2.8);
    
    // 2. Pattern Blending Logic (Synced to Grid)
    float combined_pat = 0.0;
    float base_res = 32.0; 
    int c_ids[5] = int[](current_slot_0, current_slot_1, current_slot_2, current_slot_3, current_slot_4);
    int n_ids[5] = int[](next_slot_0, next_slot_1, next_slot_2, next_slot_3, next_slot_4);

    for (int i = 0; i < 5; i++) {
        float res = base_res + float(i) * 2.0;
        
        // SYNC POINT: Use (scaled_grid.y + grid_time) to match the moving_grid_uv.y
        // This ensures the pattern stays locked to the vertical "cells" of the grid.
        float expansion = (scaled_grid.y + grid_time) * (1.0 + float(i) * 0.1);
        vec2 flow_uv = vec2(grid_uv.x * 0.2, expansion) * res;
        
        float pat_a = get_pattern_mask(c_ids[i], flow_uv, TIME);
        float pat_b = get_pattern_mask(n_ids[i], flow_uv, TIME);
        combined_pat = max(combined_pat, mix(pat_a, pat_b, transition_weight));
    }

    // 3. Grid Rendering
    vec2 moving_grid_uv = scaled_grid + vec2(0.0, grid_time);
    vec2 g_uv = fract(moving_grid_uv);
    vec2 dist_to_line = abs(g_uv - 0.5);
    
    float thick = 0.01 * line_thickness;
    float core = smoothstep(thick, thick * 0.5, dist_to_line.x) + smoothstep(thick, thick * 0.5, dist_to_line.y);
    float glow_edge = smoothstep(thick * 3.0, 0.0, dist_to_line.x) + smoothstep(thick * 3.0, 0.0, dist_to_line.y);
    
    // 4. Composite
    vec3 line_color = mix(color.rgb, alt_color.rgb, combined_pat * clamp(core, 0.0, 1.0));
    float grid_alpha = clamp(core * 0.7 + (glow_edge * 0.25), 0.0, 1.0) * battery;
    
    vec3 final_grid = mix(background_color.rgb, line_color * horizon_intensity, grid_alpha) * global_fade;
    COLOR = vec4(final_grid, global_fade);
}