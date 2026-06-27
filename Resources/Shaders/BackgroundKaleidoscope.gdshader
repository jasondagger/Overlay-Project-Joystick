shader_type canvas_item;

// Integrated for Overlay.Core.Contents.Effects
// Balanced Radial Sheen with Music Reactive Intensity

uniform float internal_time = 0.25;
uniform float resolution = 1.0;
uniform vec4 color : source_color;
uniform vec4 alt_color : source_color; 
uniform float bloom_intensity : hint_range(1.0, 5.0) = 2.0;
uniform float transition_weight; // 0.0 to 1.0
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
// Driven by SpectrumMusicAnalyzer.Intensity in C#
uniform float music_intensity; 
uniform float rotation_speed : hint_range(0.0, 5.0) = 0.6; // New Uniform

// --- MATH & UTILITY FUNCTIONS ---

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

vec4 alphaBlend(vec4 back, vec4 front) {
    float w = front.w + back.w * (1.0 - front.w);
    vec3 xyz = (front.xyz * front.w + back.xyz * back.w * (1.0 - front.w)) / w;
    return w > 0.0 ? vec4(xyz, w) : vec4(0.0);
}

mat2 ROT(float a) {
    return mat2(vec2(cos(a), sin(a)), vec2(-sin(a), cos(a)));
}

float PCOS(float x) {
    return 0.5 + 0.5 * cos(x);
}

vec3 alphaBlend34(vec3 back, vec4 front) {
    return mix(back, front.xyz, front.w);
}

float hashf(float co) {
    return fract(sin(co * 12.9898) * 13758.5453);
}

float hashv(vec2 p) {
    float a = dot(p, vec2(127.1, 311.7));
    return fract(sin(a) * 43758.5453123);
}

float tanh_approx(float x) {
    float x2 = x * x;
    return clamp(x * (27.0 + x2) / (27.0 + 9.0 * x2), -1.0, 1.0);
}

float pmin(float a, float b, float k) {
    float h = clamp(0.5 + 0.5 * (b - a) / k, 0.0, 1.0);
    return mix(b, a, h) - k * h * (1.0 - h);
}

float pmax(float a, float b, float k) {
    return -pmin(-a, -b, k);
}

float pabs(float a, float k) {
    return pmax(a, -a, k);
}

vec3 postProcess(vec3 col, vec2 q) {
    col = clamp(col, 0.0, 1.0);
    col = pow(col, vec3(1.0 / 2.2));
    col = col * 0.6 + 0.4 * col * col * (3.0 - 2.0 * col);
    col = mix(col, vec3(dot(col, vec3(0.33))), -0.4);
    col *= 0.5 + 0.5 * pow(19.0 * q.x * q.y * (1.0 - q.x) * (1.0 - q.y), 0.7);
    return col;
}

// --- GEOMETRY FUNCTIONS ---

vec2 toPolar(vec2 p) {
    return vec2(length(p), atan(p.y, p.x));
}

vec2 toRect(vec2 p) {
    return vec2(p.x * cos(p.y), p.x * sin(p.y));
}

float modMirror1(inout float p, float size) {
    float halfsize = size * 0.5;
    float c = floor((p + halfsize) / size);
    p = mod(p + halfsize, size) - halfsize;
    p *= mod(c, 2.0) * 2.0 - 1.0;
    return c;
}

float smoothKaleidoscope(inout vec2 p, float sm, float rep) {
    vec2 hp = p;
    vec2 hpp = toPolar(hp);
    float rn = modMirror1(hpp.y, 2.0 * PI / rep);
    float sa = PI / rep - pabs(PI / rep - abs(hpp.y), sm);
    hpp.y = sign(hpp.y) * (sa);
    hp = toRect(hpp);
    p = hp;
    return rn;
}

vec3 offset(float z) {
    float a = z;
    vec2 p = -0.075 * (vec2(cos(a), sin(a * sqrt(2.0))) + vec2(cos(a * sqrt(0.75)), sin(a * sqrt(0.5))));
    return vec3(p, z);
}

vec3 doffset(float z) {
    float eps = 0.1;
    return 0.5 * (offset(z + eps) - offset(z - eps)) / eps;
}

vec3 ddoffset(float z) {
    float eps = 0.1;
    return 0.125 * (doffset(z + eps) - doffset(z - eps)) / eps;
}

// --- TRUCHET LOGIC ---

vec2 cell_df(float r, vec2 np, vec2 mp, vec2 off) {
    vec2 n0 = normalize(vec2(1.0, 1.0));
    vec2 n1 = normalize(vec2(1.0, -1.0));
    np += off;
    mp -= off;
    float hh = hashv(np);
    float h0 = hh;
    vec2 p0 = mp;
    p0 = abs(p0);
    p0 -= 0.5;
    float d0 = length(p0);
    float d1 = abs(d0 - r);
    float dot0 = dot(n0, mp);
    float dot1 = dot(n1, mp);
    float d2 = abs(dot0);
    float t2 = dot1;
    d2 = abs(t2) > sqrt(0.5) ? d0 : d2;
    float d3 = abs(dot1);
    float t3 = dot0;
    d3 = abs(t3) > sqrt(0.5) ? d0 : d3;
    float d = d0;
    d = min(d, d1);
    if (h0 > 0.85) {
        d = min(d, d2);
        d = min(d, d3);
    } else if (h0 > 0.5) {
        d = min(d, d2);
    } else if (h0 > 0.15) {
        d = min(d, d3);
    }
    return vec2(d, (d0 - r));
}

vec2 truchet_df(float r, vec2 p) {
    vec2 np = floor(p + 0.5);
    vec2 mp = fract(p + 0.5) - 0.5;
    return cell_df(r, np, mp, vec2(0.0));
}

// --- RENDERING ---

vec4 plane(vec3 ro, vec3 rd, vec3 pp, vec3 off, float aa, float n) {
    float h_ = hashf(n);
    float h0 = fract(1777.0 * h_);
    float h1 = fract(2087.0 * h_);
    float h2 = fract(2687.0 * h_);
    float h3 = fract(3167.0 * h_);
    float h4 = fract(3499.0 * h_);
    float l = length(pp - ro);
    vec2 p = (pp - off * vec3(1.0, 1.0, 0.0)).xy;
    p *= ROT(0.5 * (h4 - 0.5) * TIME);
    float rep = 2.0 * round(mix(5.0, 30.0, h2));
    float sm = 0.05 * 20.0 / rep;
    float sn = smoothKaleidoscope(p, sm, rep);
    p *= ROT(2.0 * PI * h0 + 0.025 * TIME);
    float z = mix(0.2, 0.4, h3);
    p /= z;
    p += 0.5 + floor(h1 * 1000.0);
    float tl = tanh_approx(0.33 * l);
    float r = mix(0.30, 0.45, PCOS(0.1 * n));
    vec2 d2 = truchet_df(r, p);
    d2 *= z;
    float d = d2.x;
    float lw = 0.025 * z;
    d -= lw;
    vec3 col = mix(vec3(1.0), vec3(0.0), smoothstep(aa, -aa, d));
    col = mix(col, vec3(0.0), smoothstep(mix(1.0, -0.5, tl), 1.0, sin(PI * 100.0 * d)));
    col = mix(col, vec3(0.0), step(d2.y, 0.0));
    float t = smoothstep(aa, -aa, -d2.y - 3.0 * lw) * mix(0.5, 1.0, smoothstep(aa, -aa, -d2.y - lw));
    return vec4(col, t);
}

vec3 skyColor(vec3 ro, vec3 rd) {
    float d = pow(max(dot(rd, vec3(0.0, 0.0, 1.0)), 0.0), 20.0);
    return vec3(d);
}

vec3 colorize(vec3 ww, vec3 uu, vec3 vv, vec3 ro, vec2 p) {
    float lp = length(p);
    vec2 np = p + 1.0 / vec2(1920.0 * resolution, 1080.0 * resolution);
    float rdd = (2.0 + 1.0 * tanh_approx(lp));
    vec3 rd = normalize(p.x * uu + p.y * vv + rdd * ww);
    vec3 nrd = normalize(np.x * uu + np.y * vv + rdd * ww);
    float planeDist = 0.75;
    int furthest = 6;
    int fadeFrom = max(furthest - 5, 0);
    float nz = floor(ro.z / planeDist);
    vec3 skyCol = skyColor(ro, rd);
    vec4 acol = vec4(0.0);
    float cutOff = 0.95;
    for (int i = 1; i <= furthest; ++i) {
        float pz = planeDist * nz + planeDist * float(i);
        float pd = (pz - ro.z) / rd.z;
        if (pd > 0.0 && acol.w < cutOff) {
            vec3 pp = ro + rd * pd;
            vec3 npp = ro + nrd * pd;
            float aa = 3.0 * length(pp - npp);
            vec3 off = offset(pp.z);
            vec4 pcol = plane(ro, rd, pp, off, aa, nz + float(i));
            float nz1 = pp.z - ro.z;
            float fadeIn = smoothstep(planeDist * float(furthest), planeDist * float(fadeFrom), nz1);
            float fadeOut = smoothstep(0.0, planeDist * 0.1, nz1);
            pcol.xyz = mix(skyCol, pcol.xyz, fadeIn);
            pcol.w *= fadeOut;
            pcol = clamp(pcol, 0.0, 1.0);
            acol = alphaBlend(pcol, acol);
        } else { break; }
    }
    return alphaBlend34(skyCol, acol);
}

vec3 effect(vec2 p, vec2 q) {
    float tm = internal_time;
    vec3 ro = offset(tm);
    vec3 dro = doffset(tm);
    vec3 ddro = ddoffset(tm);
    vec3 ww = normalize(dro);
    vec3 uu = normalize(cross(normalize(vec3(0.0, 1.0, 0.0) + ddro), ww));
    vec3 vv = normalize(cross(ww, uu));
    return colorize(ww, uu, vv, ro, p);
}

void fragment() {
    vec2 q = FRAGCOORD.xy / (1.0 / SCREEN_PIXEL_SIZE).xy;
    vec2 p = -1. + 2. * q;
    p.x *= (1.0 / SCREEN_PIXEL_SIZE).x / (1.0 / SCREEN_PIXEL_SIZE).y;

    // --- SYNCED 3D PROJECTION MATH ---
    float tm = internal_time;
    vec3 ro = offset(tm);
    vec3 dro = doffset(tm);
    vec3 ddro = ddoffset(tm);
    vec3 ww = normalize(dro);
    vec3 uu = normalize(cross(normalize(vec3(0.0, 1.0, 0.0) + ddro), ww));
    vec3 vv = normalize(cross(ww, uu));

    // This vector represents the exact "forward" center of the tunnel
    // By dotting the view with the screen axes, we find the moving 2D center
    vec2 tunnel_pivot = vec2(dot(ww, uu), dot(ww, vv)); 
    vec2 centered_p = p - tunnel_pivot;

    // 2. Base Kaleidoscope Render (Uses original P)
    vec3 base_render = effect(p, q);
    base_render = postProcess(base_render, q);
    float luma = clamp(base_render.r, 0.0, 1.0);

    // 4. Pattern Mapping (Using centered_p)
    float radial_dist = length(centered_p);
    float angle = atan(centered_p.y, centered_p.x);
    float scroll_speed = internal_time * 8.0; 
    float rotation_sync = internal_time * rotation_speed;
    
    float combined_pat = 0.0;
    int current_ids[5] = int[](current_slot_0, current_slot_1, current_slot_2, current_slot_3, current_slot_4);
    int next_ids[5] = int[](next_slot_0, next_slot_1, next_slot_2, next_slot_3, next_slot_4);
    
    for (int i = 0; i < 5; i++) {
        // High resolution for sharp geometric edges
        float res = 32.0 + float(i) * 8.0; 
        
        // Expansion logic: Subtracting time from radial distance forces outward movement
        float expansion = (radial_dist * res) - (scroll_speed * (1.0 + float(i) * 0.1));
        vec2 flow_uv = vec2(expansion, (angle + rotation_sync) * (res / PI));
        
        // Sample patterns for cross-fade
        float pat_a = get_pattern_mask(current_ids[i], flow_uv, internal_time);
        float pat_b = get_pattern_mask(next_ids[i], flow_uv, internal_time);
        
        combined_pat = max(combined_pat, mix(pat_a, pat_b, transition_weight));
    }

    // --- 4. Vanish Point & Masking ---
    float inner_fade = smoothstep(0.0, 0.05, radial_dist); 
    
    // outer_fade: Now pushes the patterns much further out.
    // 1.5 reaches the corners of most screens; 0.6 is where it starts to get transparent.
    float outer_fade = smoothstep(1.5, 0.0, radial_dist); 
    
    // Final mask applied ONLY to the pattern slots
    float final_mask = combined_pat * outer_fade;

    // --- 5. SATURATION-LOCKED COLOR COMPOSITE ---
    // bg_final stays solid (the base kaleidoscope effect)
    vec3 bg_final = mix(vec3(0.0), color.rgb, luma);
    
    // pat_final is what we will fade out
    vec3 pat_final = mix(vec3(0.0), alt_color.rgb, luma);
    
    // We mix the two based on the final_mask. 
    // Since final_mask is 0 at the edges, it will strictly show bg_final there.
    vec3 composite = mix(bg_final, pat_final, final_mask);

    // 6. Non-Washing Brightness Logic
    float safe_intensity = clamp(music_intensity, 0.0, 1.0);
    float glow_falloff = exp(-radial_dist * 2.5); // Concentrated boost in the center

    // We use a power function for the boost instead of a simple multiplier.
    // This makes colors deep and rich rather than washed out to white.
    float exposure = 1.0 + (safe_intensity * 3.0 * glow_falloff);
    vec3 final_rgb = pow(composite, vec3(1.0 / max(0.1, exposure))) * bloom_intensity;
    
    // Final check to pull 'White' components back into the desired hue.
    float brightness = dot(final_rgb, vec3(0.299, 0.587, 0.114));
    if (brightness > 0.9) {
        final_rgb = mix(final_rgb, composite * 2.0, (brightness - 0.9) * 0.5);
    }
    
    // Tight Tone Map (Reinhard-Style)
    final_rgb = final_rgb / (1.0 + final_rgb * 0.1); 

    // 7. Final Output
    float alpha_mix = mix(color.a, alt_color.a, final_mask);
    COLOR = vec4(final_rgb, luma * alpha_mix);
}