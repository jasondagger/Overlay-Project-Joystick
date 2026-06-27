 shader_type canvas_item;

varying vec2 world_pos;
uniform vec4 palette[6];
uniform vec4 palette_alt[6];
uniform float global_phase; 
uniform int band_id;
uniform int effect_slot_0 : hint_range(0, 49) = 1;
uniform int effect_slot_1 : hint_range(0, 49) = 0;
uniform int effect_slot_2 : hint_range(0, 49) = 0;
uniform int effect_slot_3 : hint_range(0, 49) = 0;
uniform int effect_slot_4 : hint_range(0, 49) = 0;
uniform float music_intensity;

const float ZOOM = 0.002;
const float FEATHER = 50.0;
const float SHEEN_SCALE = 0.00008; 
const float SHEEN_SPEED = 0.5;

void vertex() {
    world_pos = (MODEL_MATRIX * vec4(VERTEX, 0.0, 1.0)).xy;
}

// Simple hash to generate a pseudo-random float from an int
float hash(int seed) {
    float s = float(seed);
    return fract(sin(s * 12.9898) * 43758.5453);
}

vec3 neon_boost(vec3 color, float amount) {
    float max_channel = max(color.r, max(color.g, color.b));
    // Ensure we never divide by zero, even if the color is black
    vec3 saturated = color / (max_channel + 0.0001); 
    return mix(color, saturated, clamp(amount, 0.0, 1.0));
}

float hash(float n) { 
    return fract(sin(n) * 43758.5453123); 
}

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
    // --- 1. THE ENGINE (STRICTLY LINEAR) ---
    // We remove the music_intensity addition here. 
    // This stops rubberbanding because movement now strictly follows the C# clock.
    float movement_engine = global_phase * 2.5; 

    // --- 2. Rainbow Logic ---
    float diagonal_pos = (world_pos.x - world_pos.y) * 0.005; 
    float direction = (band_id % 2 == 0) ? 1.0 : -1.0;
    
    float wrapped_pos = mod(diagonal_pos + (movement_engine * 0.5 * direction), 6.0);
    if (wrapped_pos < 0.0) wrapped_pos += 6.0;
    
    int idx_a = int(floor(wrapped_pos)) % 6; 
    int idx_b = (idx_a + 1) % 6;
    float t_grad = fract(wrapped_pos);

    vec3 grad_normal = mix(palette[idx_a], palette[idx_b], t_grad).rgb;
    vec3 grad_alt    = mix(palette_alt[idx_a], palette_alt[idx_b], t_grad).rgb;
    
    // --- 3. Mask Logic ---
    float m_mid = smoothstep(286.0 - FEATHER, 286.0 + FEATHER, world_pos.y) 
                * (1.0 - smoothstep(794.0 - FEATHER, 794.0 + FEATHER, world_pos.y));

    // --- 4. Pattern System ---
    vec2 p = world_pos * 0.04; 
    p += vec2(movement_engine * 0.5 * direction, movement_engine * 0.3);
    
    float final_pat = 0.0;
    int slots[5] = int[](effect_slot_0, effect_slot_1, effect_slot_2, effect_slot_3, effect_slot_4);
    float pattern_time = movement_engine * 0.8;

    for (int i = 0; i < 5; i++) {
        if (slots[i] > 0 && final_pat < 0.001) { 
            vec2 slot_p = p * (1.0 + float(i) * 0.2);
            float p_mask = clamp(get_pattern_mask(slots[i], slot_p, pattern_time), 0.0, 1.0);
            if (p_mask > 0.01) { final_pat = p_mask; }
        }
    }

    // --- 5. Composite ---
    float hard_pat = step(0.1, final_pat);
    vec3 final_fill = mix(mix(grad_alt, grad_normal, hard_pat), mix(grad_normal, grad_alt, hard_pat), m_mid);

   	// --- 6. GLOBAL X-AXIS SWEEP (RANDOMIZED) ---
    // Generate a unique seed for this band to randomize timing and speed
    float band_seed = hash(band_id); 
    
    // Use the seed to give each band a different starting point in the timeline
    float random_offset = band_seed * 10.0;
    
    // Vary the speed slightly per band (between 0.04 and 0.08) so they don't stay in sync
    float random_speed = 0.04 + (band_seed * 0.04);
    
    // Wide horizontal sweep coordinates
    float global_x_pos = world_pos.x * 0.00008 + world_pos.y * 0.00002;
    
    // Combine everything into the timer
    float sheen_timer = (movement_engine * random_speed) + random_offset;
    float sheen_val = fract(global_x_pos - sheen_timer);
    
    // Sharpen the beam
    float ray_mask = exp(-(pow(sheen_val - 0.5, 2.0)) / 0.00001); 
    
    // Apply Glow (Music only affects brightness)
    float dynamic_glow = ray_mask * (1.3 + music_intensity * 2.5);
    vec3 neon_color = neon_boost(final_fill, dynamic_glow);
    vec3 final_rgb = neon_color + (neon_color * dynamic_glow * 0.7);
    
    COLOR = vec4(final_rgb, 1.0);
}