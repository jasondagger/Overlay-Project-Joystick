shader_type canvas_item;

// --- GENERIC CONTROLS ---
uniform int effect_slot_0 : hint_range(0, 49) = 0;
uniform int effect_slot_1 : hint_range(0, 49) = 0;
uniform int effect_slot_2 : hint_range(0, 49) = 0;
uniform int effect_slot_3 : hint_range(0, 49) = 0;
uniform int model : hint_range(0, 50) = 0;
uniform vec4 color_base : source_color = vec4(1.0, 0.5, 1.0, 1.0);
uniform vec4 color_outline : source_color = vec4(1.0, 0.5, 1.0, 1.0);
uniform vec4 color_effect_slot_0 : source_color = vec4(1.0);
uniform vec4 color_effect_slot_1 : source_color = vec4(1.0);
uniform vec4 color_effect_slot_2 : source_color = vec4(1.0);
uniform vec4 color_effect_slot_3 : source_color = vec4(1.0);
uniform float audio_time;
uniform float music_intensity;
uniform float uv_scale : hint_range(0.0, 1000.0) = 2.0;

varying float v_stable_zoom;
varying float v_y_pos; 
varying float v_world_alpha; 
varying float v_auto_seed;
varying float v_size_scalar;
varying float v_world_y;
varying float v_punch_variation;
varying vec2 v_world_pos;

float hash(float s) { return fract(sin(s * 12.345) * 43758.5453); }

float sdCapsule(vec3 p, vec3 a, vec3 b, float r) {
    vec3 pa = p - a, ba = b - a;
    float h = clamp(dot(pa, ba) / (dot(ba, ba) + 0.0001), 0.0, 1.0);
    return length(pa - ba * h) - r;
}

float sdEllipsoid(vec3 p, vec3 r) {
    float k0 = length(p/r);
    float k1 = length(p/(r*r));
    return k0*(k0-1.0)/k1;
}

float smin(float a, float b, float k) {
    float h = clamp(0.5 + 0.5 * (b - a) / (k + 0.00001), 0.0, 1.0);
    return mix(b, a, h) - k * h * (1.0 - h);
}

float smax(float a, float b, float k) {
    return -smin(-a, -b, k);
}

float remap(float value, float low1, float high1, float low2, float high2) {
    return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
}

float sdCappedCone(vec3 p, float h, float r1, float r2) {
    vec2 q = vec2(length(p.xz), p.y);
    vec2 k1 = vec2(r2, h);
    vec2 k2 = vec2(r2 - r1, 2.0 * h);
    vec2 ca = vec2(q.x - min(q.x, (q.y < 0.0) ? r1 : r2), abs(q.y) - h);
    vec2 cb = q - k1 + k2 * clamp(dot(k1 - q, k2) / dot(k2, k2) + 0.00001, 0.0, 1.0);
    float s = (cb.x < 0.0 && ca.y < 0.0) ? -1.0 : 1.0;
    return s * sqrt(min(dot(ca, ca), dot(cb, cb)));
}

vec2 map_master(vec3 p, float current_seed, int type, vec2 uv, out vec3 local_p, out float sub_d) {
    float res_id = 0.0;
    p *= 1.2;
    float t = (audio_time * 1.0) + (current_seed * 42.0);
    vec3 pel_p = vec3(0.0, 0.15, 0.0);
    vec3 ch_p = vec3(0.0, 0.35, 0.0);
    
    // Default transforms
    vec3 p_mod = p;
    mat3 rotX = mat3(1.0);
    mat3 rotZ = mat3(1.0);
    float d = 1000.0;

	switch (model) {
		case 0: { // Human
            float t = audio_time * 1.5;
            
            // Bring back the growth with a smoother, clamped multiplier
            float growth = music_intensity * 0.0035;
            float s = 0.725 - growth; 
            
            // 1. RANDOM STATE LOGIC - Fixed stutter by quantizing the state selection
            float cycle_duration = 12.0; 
            // This ensures current_state only changes when cycle_id increments
            float quantized_t = floor(t / cycle_duration) * cycle_duration;
            float cycle_id = floor(t / cycle_duration);
            float cycle_fract = fract(t / cycle_duration);
            
            int current_state = int(hash(cycle_id) * 5.0);
            
            // Wider blend window to hide the transition between states
            float blend = smoothstep(0.0, 0.2, cycle_fract) * (1.0 - smoothstep(0.8, 1.0, cycle_fract));

            // 2. ROOT TRANSFORM
            vec3 p_local = p - ch_p;
            vec3 p_h = (p_local - vec3(0.0, 0.15, 0.0)) * s; 
            
            float v_sway = sin(t * 1.2) * 0.08;
            p_h.y -= v_sway + cos(t * 1.0) * 0.05;
            p_h.x -= sin(t * 0.5) * 0.12;
            p_h.z += sin(t * 0.8) * 0.05;

            float roll = sin(t * 0.5) * 0.1 * smoothstep(-0.8, 0.5, p_h.y);
            float cr = cos(roll), sr = sin(roll);
            p_h.xy *= mat2(vec2(cr, sr), vec2(-sr, cr));
            local_p = p_h;

            // 3. SUBTLE BEAT PULSE
            float pulse = music_intensity * 0.01;

            // 4. ARM ANIMATIONS
            float swing = sin(t * 1.5) * 0.08;
            vec3 el_l, ha_l, el_r, ha_r;
            el_l = vec3(-0.15, -0.02 + swing, 0.05); ha_l = el_l + vec3(0.02, -0.16, 0.02);
            el_r = vec3(0.15, -0.02 - swing, 0.05); ha_r = el_r + vec3(-0.02, -0.16, 0.02);

            if (current_state == 1) { // WAVE
                float wave = sin(t * 4.0) * 0.15;
                el_l = mix(el_l, vec3(-0.18 + wave, 0.15, 0.1), blend);
                ha_l = mix(ha_l, el_l + vec3(0.05, 0.15, 0.1), blend);
                el_r = mix(el_r, vec3(0.18 + wave, 0.15, 0.1), blend);
                ha_r = mix(ha_r, el_r + vec3(-0.05, 0.15, 0.1), blend);
            } 
            else if (current_state == 2) { // JUMP
                float pump = max(0.0, sin(t * 5.0)) * 0.2;
                el_l = mix(el_l, vec3(-0.12, 0.25 + pump, 0.02), blend);
                ha_l = mix(ha_l, el_l + vec3(-0.02, 0.22, -0.02), blend);
                el_r = mix(el_r, vec3(0.12, 0.25 + pump, 0.02), blend);
                ha_r = mix(ha_r, el_r + vec3(0.02, 0.22, -0.02), blend);
                p_h.y -= pump * 0.4;
            }
            else if (current_state == 3) { // OVERHEAD CROSS
                float bob = sin(t * 3.0) * 0.05;
                el_l = mix(el_l, vec3(-0.05, 0.3, 0.05), blend);
                ha_l = mix(ha_l, vec3(0.15, 0.45 + bob, 0.1), blend);
                el_r = mix(el_r, vec3(0.05, 0.3, 0.05), blend);
                ha_r = mix(ha_r, vec3(-0.15, 0.45 + bob, 0.1), blend);
            }
            else if (current_state == 4) { // TWIRL
                float rot_t = t * 6.0;
                el_l = mix(el_l, vec3(-0.2, 0.1 + sin(rot_t)*0.1, cos(rot_t)*0.1), blend);
                ha_l = mix(ha_l, el_l + vec3(-0.1, sin(rot_t + 0.5)*0.2, cos(rot_t + 0.5)*0.2), blend);
                el_r = mix(el_r, vec3(0.2, 0.1 + sin(rot_t + 3.1)*0.1, cos(rot_t + 3.1)*0.1), blend);
                ha_r = mix(ha_r, el_r + vec3(0.1, sin(rot_t + 3.6)*0.2, cos(rot_t + 3.6)*0.2), blend);
            }

            // 5. LEG MOVEMENT
            float walk_t = t * 3.5;
            float move_mask = (current_state == 0) ? 0.2 : 1.0;
            float lift_l = max(0.0, sin(walk_t)) * 0.2 * move_mask * blend;
            float lift_r = max(0.0, sin(walk_t + 3.14)) * 0.2 * move_mask * blend;
            vec3 kn_l = vec3(-0.08, -0.32 + lift_l*0.5, cos(walk_t)*0.08);
            vec3 kn_r = vec3(0.08, -0.32 + lift_r*0.5, -cos(walk_t)*0.08);

            // 6. GEOMETRY
            float body = sdCapsule(p_h, vec3(0, 0.08, 0), vec3(0, -0.15, 0), 0.11 + pulse);
            float head = length(p_h - vec3(0, 0.28, 0.02)) - (0.14 + pulse);
            float arms = min(min(sdCapsule(p_h, vec3(-0.1, 0.13, 0), el_l, 0.028 + pulse), sdCapsule(p_h, el_l, ha_l, 0.028 + pulse)),
                             min(sdCapsule(p_h, vec3(0.1, 0.13, 0), el_r, 0.035 + pulse), sdCapsule(p_h, el_r, ha_r, 0.035 + pulse)));
            float legs = min(smin(sdCapsule(p_h, vec3(-0.06, -0.15, 0), kn_l, 0.038 + pulse), sdCapsule(p_h, kn_l, vec3(-0.08, -0.55+lift_l, 0), 0.038 + pulse), 0.04),
                             smin(sdCapsule(p_h, vec3(0.06, -0.15, 0), kn_r, 0.038 + pulse), sdCapsule(p_h, kn_r, vec3(0.08, -0.55+lift_r, 0), 0.038 + pulse), 0.04));

            // Combine geometry
            float d_all = smin(smin(head, body, 0.08), smin(arms, legs, 0.05), 0.05);
            
            // --- THE FIX ---
            // Normalize by local scale 's' AND the global '1.2' scale
            float final_d = (d_all / s) / 1.2;
            
            sub_d = final_d; 
            return vec2(final_d * s, 0.0);
        }
		
	    case 1: { // Airplane
		    float speed = audio_time * 2.0;
		    
		    // --- 1. SCALE & EFFECT SPACE ---
		    // Changed 0.35 to 0.45 to move the model down slightly
		    vec3 p_raw = (p - vec3(0.0, -0.15, 0.0)) * 0.6; 
		    local_p = p_raw; 

		    // --- 2. MOTION CONTROLS ---
		    float horizontal_sway = sin(audio_time * 1.5) * 0.05; 
		    float vertical_bob = cos(audio_time * 1.2) * 0.06;
		    float banking = sin(speed) * 0.2; 
		    float pitch = cos(speed * 0.5) * 0.08;
		    
		    mat3 rotZ_jet = mat3(vec3(cos(banking),-sin(banking),0), vec3(sin(banking),cos(banking),0), vec3(0,0,1));
		    mat3 rotX_jet = mat3(vec3(1,0,0), vec3(0,cos(pitch),-sin(pitch)), vec3(0,sin(pitch),cos(pitch)));
		    
		    // Position and rotation applied to the new pivot
		    vec3 p_jet = ((p_raw - (ch_p + vec3(horizontal_sway, vertical_bob, 0.0))) * rotZ_jet * rotX_jet) + ch_p;
		    p_jet.z += sin(audio_time * 60.0) * 0.006 * music_intensity;

		    // ... (rest of geometry assembly remains the same) ...
		    float body = sdCapsule(p_jet, ch_p - vec3(0,0,0.3), ch_p + vec3(0,0,0.25), 0.11);
		    vec3 p_wing = p_jet - ch_p;
		    p_wing.x = abs(p_wing.x); 
		    float wings = sdCapsule(p_wing, vec3(0.0, -0.02, -0.05), vec3(0.45, 0.02, -0.25), 0.02);
		    vec3 p_eng = p_jet - ch_p;
		    p_eng.x = abs(p_eng.x) - 0.22;
		    p_eng.y += 0.06;
		    float engines = sdCapsule(p_eng, vec3(0,0,-0.15), vec3(0,0,0.1), 0.05);
		    vec3 p_prop = p_eng - vec3(0.0, 0.0, 0.12); 
		    float prop_speed = audio_time * 25.0; 
		    float angle = atan(p_prop.y, p_prop.x) + prop_speed;
		    float blades = sin(angle * 3.0); 
		    float prop_geo = max(length(p_prop.xy) - 0.18, abs(p_prop.z) - 0.01); 
		    prop_geo = max(prop_geo, -blades + 0.8); 
		    float hub = length(p_prop - vec3(0,0,0.02)) - 0.03;
		    float tail = sdCapsule(p_jet, ch_p + vec3(0, 0.05, -0.25), ch_p + vec3(0, 0.2, -0.4), 0.015);
		    float canopy = sdEllipsoid(p_jet - (ch_p + vec3(0, 0.08, 0.1)), vec3(0.06, 0.04, 0.12));

		    d = smin(body, wings, 0.06);
		    d = smin(d, engines, 0.04);
		    d = smin(d, canopy, 0.03);
		    d = smin(d, hub, 0.02);
		    d = min(d, tail);
		    d = min(d, prop_geo);
		}
		break;
		
		case 2: { // UFO
            float t = audio_time * 1.2;
            
            // --- 1. SCALE & EFFECT SPACE ---
            float s = 0.5; // Scale factor
            vec3 p_raw = (p - vec3(0.0, -0.15, 0.0)) * s; 
            local_p = p_raw; 

            // --- 2. MOTION VECTORS ---
            float hover = sin(t) * 0.08;
            float rock = sin(t * 0.6) * 0.06; 
            float drift_x = cos(t * 0.5) * 0.15; 
            
            mat3 rotZ_ufo = mat3(vec3(cos(rock),-sin(rock),0), vec3(sin(rock),cos(rock),0), vec3(0,0,1));
            
            // Apply drift and hover
            vec3 p_ufo = (p_raw - ch_p);
            p_ufo.x -= drift_x; 
            p_ufo.y -= hover;
            p_ufo = (p_ufo * rotZ_ufo) + ch_p;

            // --- 3. GEOMETRY ---
            float saucer = sdEllipsoid(p_ufo - ch_p, vec3(0.38, 0.07, 0.38));
            float dome = length(p_ufo - (ch_p + vec3(0, 0.06, 0))) - 0.14;
            dome = max(dome, -(p_ufo.y - (ch_p.y + 0.06)));

            // --- 4. THE BEAM ---
            vec3 p_beam = p_ufo - (ch_p - vec3(0.0, 0.55, 0.0));
            float beam_height = 0.55;
            float beam_raw = sdCappedCone(p_beam, beam_height, 0.3, 0.01);

			float beam_floor = -(p_ufo.y - (ch_p.y - 0.5)); 
			float beam = max(beam_raw, beam_floor);
            
            float visibility = smoothstep(0.1, 0.4, sin(audio_time * 1.5));
            
            float d_all = smin(saucer, dome, 0.06);
            
            // Use a separate distance for the beam so it doesn't get the "body" outline
            d = min(d_all, mix(10.0, beam, visibility));
            
            // --- THE FIX ---
            // 1. Normalize distance by scale 's'
            // 2. Return a vec2 with an ID (0.0 for body, 2.0 for beam/alt)
            sub_d = d;
            return vec2(d / s, (d == beam) ? 2.0 : 0.0);
        }
			
		case 3: { // Spider 
            float t = audio_time * 1.2;

            // --- 1. SCALE & EFFECT SPACE ---
            // Matches the .6 multiplier and the lowered .45 pivot
            vec3 p_raw = (p - vec3(0.0, -0.55, 0.0)) * 0.6; 
            local_p = p_raw;

            // --- 2. MOTION VECTORS ---
            float body_bob = sin(audio_time * 4.0) * 0.03;
            vec3 p_spider = p_raw - ch_p + vec3(0.0, body_bob, 0.0);
			
			// --- 2. MOTION VECTORS (Added Sway) ---
			float side_sway = sin(audio_time * 2.0) * 0.08; // Gentle side-to-side
			float pivot_roll = cos(audio_time * 1.5) * 0.05; // Slight tilt
			p_spider.x -= side_sway;
			p_spider.xy *= mat2(vec2(cos(pivot_roll), -sin(pivot_roll)), vec2(sin(pivot_roll), cos(pivot_roll)));
            
            // --- 3. GEOMETRY ---
            float body = sdEllipsoid(p_spider, vec3(0.15, 0.1, 0.2));
            float head = sdEllipsoid(p_spider - vec3(0.0, 0.05, 0.18), vec3(0.08, 0.07, 0.08));
            d = smin(body, head, 0.05);

            for(int i = 0; i < 8; i++) {
                float side = (i < 4) ? 1.0 : -1.0;
                float off = float(i % 4);
                
                float leg_seed = fract(sin(float(i) * 12.9898) * 43758.5453);
                float leg_speed = 5.0 + (leg_seed * 4.0);
                float leg_offset = leg_seed * 6.28; 
                float up_down = sin(audio_time * leg_speed + leg_offset) * 0.15;
                
                vec3 knee = vec3(side * (0.25 + off * 0.05), 0.2 + up_down, 0.3 - off * 0.2);
                vec3 foot = vec3(side * (0.4 + off * 0.05), -0.3 + up_down, 0.4 - off * 0.2);
                
                float leg = min(sdCapsule(p_spider, vec3(0,0,0), knee, 0.02), 
                                sdCapsule(p_spider, knee, foot, 0.015));
                d = smin(d, leg, 0.03);
            }
        }
        break;
		
		case 4: { // Dinosaur
            float t = audio_time * 1.8; 
            float phase = v_auto_seed * 6.28;
            
            float growth = music_intensity * 0.005; 
            float s = 1.0; 
            vec3 p_h = (p - ch_p) * s;
            local_p = p_h;

            // 1. ORGANIC CORE MOTION
            float vert_sway = sin(t * 1.5) * 0.05;
            vec3 p_base = p_h - vec3(0.0, vert_sway - 0.1, 0.0);
            float torso_sway = sin(t * 1.1) * 0.15;
            p_base.x += torso_sway;
            
            float tilt = torso_sway * 0.4; 
            float ct = cos(tilt), st = sin(tilt);
            p_base.xy *= mat2(vec2(ct, -st), vec2(st, ct));

            float body = sdEllipsoid(p_base, vec3(0.18 + growth, 0.22 + growth, 0.7 + growth));

            // 2. THINNER NECK & HEAD
            float head_v_bob = cos(t * 2.4) * 0.04 - (pow(max(0.0, sin(t * 0.5)), 10.0) * 0.05);
            vec3 head_target = vec3(sin(t * 1.2) * 0.06, 0.55 + head_v_bob, 0.85);
            vec3 n_mid = vec3(0.0, 0.42, 0.42) + vec3(sin(t * 1.2) * 0.04, 0, 0);
            // Reduced neck thickness: 0.13->0.1 and 0.08->0.06
            float neck_geo = smin(sdCapsule(p_base, vec3(0.0, 0.3, 0.2), n_mid, 0.1 + growth), 
                                 sdCapsule(p_base, n_mid, head_target, 0.06 + growth), 0.1);
            float head = sdEllipsoid(p_base - head_target, vec3(0.12 + growth, 0.14 + growth, 0.28 + growth));

            // 3. MAGNETIC GAIT
            vec3 p_leg = p_base; p_leg.x = abs(p_leg.x);
            float side_p = (p_base.x > 0.0) ? (t * 3.5) : (t * 3.5 + 3.1415);
            float raw_lift = max(0.0, sin(side_p)) * 0.3;
            float lift = raw_lift * smoothstep(0.05, 0.15, raw_lift);
            float l_push = cos(side_p) * 0.3;
            
            vec3 hip = vec3(0.14, 0.05, -0.1);
            vec3 knee = hip + vec3(0.06, -0.22 + lift * 0.6, 0.25 + l_push * 0.4);
            vec3 ankle = knee + vec3(-0.02, -0.32 - lift * 0.2, -0.25 + l_push * 0.1);
            vec3 foot = ankle + vec3(0.0, -0.05 + lift, 0.22);
            
            float leg_geo = smin(sdCapsule(p_leg, hip, knee, 0.072 + growth), 
                            smin(sdCapsule(p_leg, knee, ankle, 0.042 + growth), 
                                sdCapsule(p_leg, ankle, foot, 0.032 + growth), 0.05), 0.05);

            // 4. TIGHTENED UNIFIED TAIL
            float v_sway = sin(t * 2.2) * 0.12;
            vec3 t0 = vec3(0.0, 0.05, -0.35); 
            // Reduced t1 x-sway: 0.35 -> 0.15
            vec3 t1 = vec3(sin(t * 1.1) * 0.15, 0.15 + v_sway * 0.4, -0.9);
            float tip_whip = cos(t * 1.6) * 0.08;
            // Reduced tip x-sway: 1.4 -> 0.65
            vec3 t2 = vec3(sin(t * 1.1 - 1.2) * 0.65, 0.45 + v_sway + tip_whip, -1.95);
            
            // Re-integrated growth into unified solver
            vec3 ba = t1 - t0; vec3 pa = p_base - t0;
            float h = clamp(dot(pa,ba)/dot(ba,ba), 0.0, 1.0);
            float distA = length(pa - ba*h);
            float thickA = mix(0.12 + growth, 0.08 + growth, h);
            
            vec3 cb = t2 - t1; vec3 pb = p_base - t1;
            float k = clamp(dot(pb,cb)/dot(cb,cb), 0.0, 1.0);
            float distB = length(pb - cb*k);
            float thickB = mix(0.08 + growth, 0.008 + growth, k);
            
            float tail_geo = min(distA - thickA, distB - thickB);

            // 5. CLAWS
            vec3 p_claw = p_leg - foot; p_claw.x = abs(p_claw.x); 
            float claws = min(sdCapsule(p_claw, vec3(0), vec3(0, -0.02, 0.1), 0.012), 
                              sdCapsule(p_claw - vec3(0.04, 0, -0.02), vec3(0), vec3(0.01, -0.01, 0.07), 0.01));

            // 6. FINAL SMOOTH BLEND
            d = smin(body, neck_geo, 0.15);
            d = smin(d, head, 0.12);
            d = smin(d, tail_geo, 0.2); 
            d = smin(d, leg_geo, 0.08);
            d = smin(d, claws, 0.02);
            
            return vec2(d / s * 0.6, 0.0); 
        }
		
		case 5: { // Hatsune Miku
            float t = audio_time * 2.0;
            
            // Subtle growth multiplier clamped for stability
            float growth = music_intensity * 0.004; 
            float pulse = growth * 1.5; 

            // --- 1. STATE MACHINE ---
            float cycle_duration = 10.0; 
            float cycle_id = floor(t / cycle_duration);
            float cycle_fract = fract(t / cycle_duration);
            int miku_state = int(hash(cycle_id) * 4.0);
            float blend = smoothstep(0.0, 0.1, cycle_fract) * (1.0 - smoothstep(0.9, 1.0, cycle_fract));

            // --- 2. ROOT TRANSFORM ---
            float s = 0.9; 
            vec3 p_h = (p - ch_p - vec3(0.0, 0.08, 0.0)) * s; 
            
            float body_sway = sin(t * 0.4) * 0.1; 
            float body_bounce = abs(sin(t * 1.2)) * 0.04;
            p_h -= vec3(body_sway, body_bounce, 0.0);
            local_p = p_h;

            // --- 3. NEW ANIMATION SET (No Jitter) ---
            float swing = sin(t * 2.5) * 0.2;
            vec3 el_l = vec3(-0.25, -0.05 + swing, 0.05); vec3 ha_l = el_l + vec3(0.02, -0.2, 0.02);
            vec3 el_r = vec3(0.25, -0.05 - swing, 0.05); vec3 ha_r = el_r + vec3(-0.02, -0.2, 0.02);
            float pigtail_mod = 1.0;

            if (miku_state == 0) { // NEW: Flowing Wave
                float wave = sin(t * 1.5) * 0.25;
                el_l = mix(el_l, vec3(-0.35, 0.1 + wave, 0.1), blend);
                ha_l = mix(ha_l, el_l + vec3(0.1, 0.2, 0.1), blend);
                el_r = mix(el_r, vec3(0.35, 0.1 - wave, 0.1), blend);
                ha_r = mix(ha_r, el_r + vec3(-0.1, 0.2, 0.1), blend);
            } 
            else if (miku_state == 1) { // NEW: Twin-Tail Spiral
                float rot_t = t * 3.0;
                pigtail_mod = 1.8; 
                el_l = mix(el_l, vec3(-0.2, 0.3, 0.1), blend);
                ha_l = mix(ha_l, el_l + vec3(sin(rot_t)*0.1, 0.1, cos(rot_t)*0.1), blend);
            }
            else if (miku_state == 2) { // NEW: Heart & Lean
                float lean = cos(t * 1.2) * 0.1;
                p_h.x += lean * blend;
                vec3 heart_pos = vec3(0.0, 0.12, 0.2);
                el_l = mix(el_l, vec3(-0.12, 0.1, 0.15), blend);
                ha_l = mix(ha_l, heart_pos, blend);
                el_r = mix(el_r, vec3(0.12, 0.1, 0.15), blend);
                ha_r = mix(ha_r, heart_pos, blend);
            }
            else if (miku_state == 3) { // NEW: Rhythmic Step
                float step_up = max(0.0, sin(t * 3.0)) * 0.15;
                el_l = mix(el_l, vec3(-0.3, 0.4 - step_up, 0.0), blend);
                ha_l = mix(ha_l, el_l + vec3(0.1, -0.2, 0.1), blend);
                pigtail_mod = 1.3;
            }

            // --- 4. GEOMETRY ---
            float body = sdCapsule(p_h, vec3(0, 0.1, 0), vec3(0, -0.2, 0), 0.12 + pulse);
            float head = length(p_h - vec3(0, 0.32, 0)) - (0.14 + pulse);
            float arms = min(min(sdCapsule(p_h, vec3(-0.12, 0.12, 0), el_l, 0.035 + pulse), sdCapsule(p_h, el_l, ha_l, 0.035 + pulse)),
                             min(sdCapsule(p_h, vec3(0.12, 0.12, 0), el_r, 0.035 + pulse), sdCapsule(p_h, el_r, ha_r, 0.035 + pulse)));

            vec3 p_side = p_h; p_side.x = abs(p_side.x); 
            float hair_phys = clamp(body_sway * 2.0, -0.25, 0.25) * pigtail_mod;
            float hair_wave = sin(t * 0.8 + p_h.y * 5.0) * 0.15;
            float pigtails = sdCapsule(p_side, vec3(0.16, 0.38, -0.05), vec3(0.45 + hair_wave + hair_phys, -0.55, -0.1), 0.045 + pulse);

            float leg_phase = (p_h.x > 0.0) ? t * 4.0 : t * 4.0 + 3.1415;
            float legs = sdCapsule(p_side, vec3(0.09, -0.2, 0), vec3(0.14, -0.65 + max(0.0, sin(leg_phase)) * 0.18, sin(leg_phase) * 0.12), 0.05 + pulse);

            float d_all = smin(smin(smin(smin(body, head, 0.1), arms, 0.06), legs, 0.08), pigtails, 0.08);
            
            // --- THE FIX ---
            // 1. Divide by the local scale 's' (0.9)
            // 2. Divide by the global scale (1.2) from the top of map_master
            // This ensures the distance field is perfectly 1:1 with world space.
            float final_d = (d_all / s) / 1.2;
            
            sub_d = final_d; 
            return vec2(final_d * s, 1.0); 
        }
		
		case 6: { // Robot
            float t = audio_time * 4.0;
            
            // --- 1. SCALE & EFFECT SPACE ---
            vec3 p_raw = (p - vec3(0.0, -0.15, 0.0)) * 0.7; 
            vec3 p_glob = p_raw - ch_p;
            
            // 2. GLOBAL FLOAT & SWAY
            vec3 glob_off = vec3(sin(audio_time * 1.5) * 0.1, cos(audio_time * 1.2) * 0.08, sin(audio_time * 0.9) * 0.05);
            p_glob -= glob_off;

            float bob = abs(cos(t * 1.0)) * 0.05;
            float sway_weight = smoothstep(-0.5, 0.3, p_glob.y); 
            float sway_x = sin(t * 0.8) * 0.08 * sway_weight;
            float sway_z = cos(t * 0.8) * 0.05 * sway_weight;
            
            vec3 p_torso = p_glob - vec3(sway_x, bob, sway_z);
            float roll = sway_x * 0.5;
            float cr = cos(roll), sr = sin(roll);
            p_torso.xy *= mat2(vec2(cr, sr), vec2(-sr, cr));
            
            local_p = p_torso;

            // 3. CORE GEOMETRY
            float body = sdCapsule(p_torso, vec3(0, -0.12, 0), vec3(0, 0.15, 0), 0.14);
            vec3 q_door = abs(p_torso - vec3(0, -0.01, 0.13)) - vec3(0.08, 0.08, 0.01);
            float door = length(max(q_door, 0.0)) + min(max(q_door.x, max(q_door.y, q_door.z)), 0.0);
            
            vec3 p_head = p_torso - vec3(0, 0.26, 0);
            float look_jerk = floor(t * 0.5) + smoothstep(0.0, 0.2, fract(t * 0.5));
            float head_yaw = sin(look_jerk * 1.5) * 0.3;
            p_head.xz *= mat2(vec2(cos(head_yaw), sin(head_yaw)), vec2(-sin(head_yaw), cos(head_yaw)));
            float head_geo = min(sdCapsule(p_head, vec3(0,-0.04,0), vec3(0,0.02,0), 0.12), length(p_head - vec3(0,0.02,0)) - 0.12);
            float visor = length(max(abs(p_head - vec3(0, 0.02, 0.09)) - vec3(0.09, 0.035, 0.04), 0.0));
            float ant = min(sdCapsule(p_head, vec3(0,0.12,0), vec3(sin(t*0.8)*0.02,0.22,0), 0.015), length(p_head - vec3(0,0.24,0)) - 0.035);

            float limbs = 1e5;

            // 4. LIMB LOOP
            for (float s = -1.0; s <= 1.0; s += 2.0) {
                float arm_t = t * 1.1 + (s * 1.5);
                
                // ARM CALCULATIONS
                vec3 shoulder = vec3(0.16 * s, 0.12, 0);
                vec3 elbow = shoulder + vec3(0.06 * s + (sin(arm_t) * 0.03 * s), -0.15, cos(arm_t) * 0.18);
                vec3 hand_pos = elbow + vec3(0.01 * s, -0.16, cos(arm_t - 0.4) * 0.2);
                
                float arm_u = sdCapsule(p_torso, shoulder, elbow, 0.03);
                float arm_l = sdCapsule(p_torso, elbow, hand_pos, 0.03);
                
                // HAND GEOMETRY - Local Directional Fix
				vec3 p_h = p_torso - hand_pos;
				vec3 forw = normalize(hand_pos - elbow); 
				vec3 side = normalize(cross(forw, vec3(0,1,0)));

				// Define fixed-length tip positions relative to the wrist origin
				float prong_len = 0.12;
				float spread = 0.04;
				vec3 tip1 = forw * prong_len + side * spread;
				vec3 tip2 = forw * prong_len - side * spread;

				// Welding to vec3(0) ensures the base of the prongs never leaves the wrist center
				float wrist = length(p_h) - 0.055;
				float p1 = sdCapsule(p_h, vec3(0), tip1, 0.025);
				float p2 = sdCapsule(p_h, vec3(0), tip2, 0.025);
				float hand_d = min(wrist, min(p1, p2));
                
                // LEGS
                float leg_t = t * 1.2 + (s > 0.0 ? 0.0 : 3.1415);
                float lift = pow(max(0.0, sin(leg_t)), 0.7) * 0.18;
                float swg = cos(leg_t) * 0.12;
                vec3 hip = vec3(0.1 * s, -0.1, 0);
                vec3 knee = vec3(0.12 * s, -0.28 + lift, swg);
                vec3 foot_base = vec3(0.12 * s, -0.52 + lift, swg * 1.2);
                
                float leg_up = sdCapsule(p_torso, hip, knee, 0.045);
                float leg_lo = sdCapsule(p_torso, knee, foot_base, 0.045);
                
                vec3 q_f = abs(p_torso - foot_base - vec3(0, -0.04, 0.04)) - vec3(0.06, 0.04, 0.09);
                float foot = length(max(q_f, 0.0)) + min(max(q_f.x, max(q_f.y, q_f.z)), 0.0);
                
                // FIXED MERGE: hand_d (which contains prongs) is now properly welded to the arm chain
                float arm_full = min(min(arm_u, arm_l), hand_d);
                float leg_full = min(leg_up, smin(leg_lo, foot, 0.05));
                
                limbs = min(limbs, min(arm_full, leg_full));
            }

            d = smin(body, door, 0.03);
            d = smin(d, head_geo, 0.06);
            d = min(d, visor); d = min(d, ant);
            d = smin(d, limbs, 0.03);
            res_id = 1.0;
        } 
        break;
		
		case 7: { // Tank
            float t = audio_time * 3.0;
            
            // --- 1. SCALE & EFFECT SPACE ---
            // Matches the .6 multiplier and the lowered .45 pivot
            vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.6; 
            
            // --- 2. MOTION VECTORS ---
            float bounce = abs(sin(t * 2.0)) * 0.05 * music_intensity;
            float horizontal_sway = sin(t * 0.5) * 0.12; 
            float track_rumble = sin(t * 15.0) * 0.008; 
            
            // Apply sway, offset, and rumble to the scaled coordinates
            vec3 p_tank = p_raw - ch_p - vec3(horizontal_sway, -0.25 + track_rumble, 0.0); 
            p_tank.y += bounce;
            p_tank *= (1.0 - bounce * 0.4);
            local_p = p_tank;

            // --- 3. HULL ---
            float waist = 1.0 - 0.15 * exp(-p_tank.z * p_tank.z * 10.0);
            vec3 q_h = abs(p_tank) - vec3(0.2 * waist, 0.16, 0.38); 
            float hull = length(max(q_h, 0.0)) - 0.05;
            
            // --- 4. TREADS ---
            vec3 p_tread = p_tank; p_tread.x = abs(p_tread.x);
            vec3 q_t = abs(p_tread - vec3(0.28, -0.1, 0.0)) - vec3(0.09, 0.12, 0.45);
            float treads = length(max(q_t, 0.0)) - 0.05; 
            
            // --- 5. TURRET & BARREL ---
            float turret_scan = sin(t * 1.2) * 0.5;
            float elevation = sin(t * 0.3) * 0.25 + 0.05; 
            
            vec3 p_turret = p_tank - vec3(0, 0.3, 0.05); 
            p_turret.xz *= mat2(vec2(cos(turret_scan), -sin(turret_scan)), vec2(sin(turret_scan), cos(turret_scan)));
            
            vec3 q_head = abs(p_turret) - vec3(0.12, 0.08, 0.12);
            float t_head = length(max(q_head, 0.0)) - 0.05;
            float cupola = length(p_turret - vec3(0, 0.1, 0)) - 0.06;
            t_head = smin(t_head, cupola, 0.02);
            
            vec3 p_barrel = p_turret;
            p_barrel.yz *= mat2(vec2(cos(elevation), -sin(elevation)), vec2(sin(elevation), cos(elevation)));
            float cannon = sdCapsule(p_barrel, vec3(0, -0.02, 0.05), vec3(0, -0.02, 0.5), 0.06);

            // --- 6. ASSEMBLY ---
            d = smin(hull, treads, 0.015); 
            d = min(d, smin(t_head, cannon, 0.04));
        }
        break;
		
		case 8: { // Gun
            float t = audio_time * 2.0;
            
            // --- 1. SCALE & EFFECT SPACE ---
            // Vertical pivot shifted to 0.0 with .6 multiplier
            vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.6; 
            
            // --- 2. ORGANIC DRIFT ---
            float drift_x = sin(t * 0.4) * 0.12;
            float drift_y = cos(t * 0.3) * 0.08;
            
            vec3 p_m4 = p_raw - ch_p + vec3(drift_x, 0.25 + drift_y, 0.0); 
            vec3 pivot = vec3(0.0, 0.0, -0.05); 
            p_m4 -= pivot;

            // 3. BALANCED SWAY
            float yaw = sin(t * 1.2) * 0.25;   
            float pitch = cos(t * 0.8) * 0.1; 
            p_m4.xz *= mat2(vec2(cos(yaw), -sin(yaw)), vec2(sin(yaw), cos(yaw)));
            p_m4.yz *= mat2(vec2(cos(pitch), -sin(pitch)), vec2(sin(pitch), cos(pitch)));
            p_m4 += pivot;

            // --- 4. SHOOTING LOGIC ---
            float shoot_chance = smoothstep(0.4, 0.8, sin(t * 0.3) * sin(t * 0.17));
            float idle_bob = sin(t * 1.5) * 0.01;
            float fire_shake = sin(t * 60.0) * 0.03 * shoot_chance;
            float fire_kick = music_intensity * 0.15 * shoot_chance;
            
            p_m4.y += idle_bob;
            p_m4.z += fire_kick + fire_shake; 
            local_p = p_m4;

            // --- 5. RIFLE GEOMETRY ---
            float grip = sdCapsule(p_m4, vec3(0, 0, -0.05), vec3(0, 0.3, -0.05), 0.04);
            float mag = length(max(abs(p_m4 - vec3(0, 0.25, 0.25)) - vec3(0.03, 0.15, 0.06), 0.0));
            float upper = length(max(abs(p_m4 - vec3(0, 0.4, 0.1)) - vec3(0.04, 0.06, 0.3), 0.0));
            float bolt = length(max(abs(p_m4 - vec3(0, 0.42, 0.15 - (fire_kick * 2.0))) - vec3(0.03, 0.03, 0.1), 0.0));
            float barrel = sdCapsule(p_m4, vec3(0, 0.4, 0.3), vec3(0, 0.4, 0.9), 0.02);
            float handguard = sdCapsule(p_m4, vec3(0, 0.4, 0.3), vec3(0, 0.4, 0.6), 0.06);
            float stock = sdCapsule(p_m4, vec3(0, 0.4, -0.2), vec3(0, 0.3, -0.6), 0.05);

            d = min(min(grip, mag), min(upper, min(bolt, min(barrel, min(handguard, stock)))));

            if (shoot_chance > 0.1) {
                float b_cycle = fract(t * 10.0); 
                vec3 p_world = p_raw - ch_p + vec3(drift_x, 0.25 + drift_y, 0.0);
                vec3 bullet_p = p_world - vec3(0.0, 0.4, 0.95);
                bullet_p.xz *= mat2(vec2(cos(yaw), -sin(yaw)), vec2(sin(yaw), cos(yaw)));
                bullet_p.yz *= mat2(vec2(cos(pitch), -sin(pitch)), vec2(sin(pitch), cos(pitch)));
                bullet_p.z -= b_cycle * 15.0; 
                
                float bullets = sdCapsule(bullet_p, vec3(0), vec3(0, 0, 0.6), 0.02);
                vec3 flash_p = p_m4 - vec3(0, 0.4, 0.95);
                float pulse = (0.5 + 0.5 * sin(t * 60.0)) * shoot_chance;
                float flash = smin(length(flash_p) - (0.12 * pulse), length(flash_p - vec3(0, 0, 0.3)) - (0.03 * pulse), 0.05);
                
                if (bullets < d) d = bullets; 
                if (flash < d) d = flash;
            }
        }
        break;
		
		case 9: { // Pokeball
            float t = audio_time * 2.5;

            // --- 1. SCALE & EFFECT SPACE ---
            // Pivot Y set to 0.0 with .6 multiplier
            vec3 p_raw = (p - vec3(0.0, -0.2, 0.0)) * 0.6; 
            
            float h_drift = sin(t * 0.7) * 0.12;
            vec3 p_ball = p_raw - ch_p + vec3(h_drift, 0.0, 0.0);
            
            // 2. FLOAT & CLAMPED ROTATION
            p_ball.y += sin(t * 1.2) * 0.15;
            float yaw = (sin(t * 0.5) * 0.5) + 3.14159; 
            p_ball.xz *= mat2(vec2(cos(yaw), -sin(yaw)), vec2(sin(yaw), cos(yaw)));
            
            local_p = p_ball; 

            // 3. THE HINGE
            float open = smoothstep(0.2, 0.8, sin(t * 1.5) * 0.5 + 0.5) * 1.5;
            vec3 p_top = p_ball;
            vec3 hinge = vec3(0.0, 0.0, -0.25);
            p_top -= hinge;
            p_top.yz *= mat2(vec2(cos(open), -sin(open)), vec2(sin(open), cos(open)));
            p_top += hinge;
            
            float top_shell = max(length(p_top) - 0.25, -p_top.y);
            float bot_shell = max(length(p_ball) - 0.25, p_ball.y);
            
            // 4. BIGGER BUTTON
            vec3 p_btn = p_ball - vec3(0, 0, 0.25);
            float button = length(p_btn) - 0.07; 
            
            d = min(top_shell, bot_shell);
            d = smin(d, button, 0.01);
        }
        break;
		
		case 10: { // Jellyfish
	        float t = audio_time * 2.0;
	        
			// 1. BASE SIZE & STABILIZED GROWTH
			// Decreasing the 1.2 to 0.8 makes the base larger; 0.04 tones down the expansion
			float growth = 1.0 + music_intensity * 0.04; 
			vec3 p_raw = (p - vec3(0.0, -0.2, 0.0)) * (0.8 / growth); 
			vec3 p_jelly = p_raw - ch_p;

			// 2. UNIQUE ROTATION & PERMANENT TILT
			// Hash-based direction (-1.0 or 1.0) and unique speed per instance
			float seed = fract(sin(dot(ch_p.xz, vec2(12.9898, 78.233))) * 43758.5453);
			float unique_dir = (seed > 0.5) ? 1.0 : -1.0;
			float unique_speed = 0.2 + seed * 0.3; 

			float spin = t * 0.15 * unique_dir; // Slower rotation
			float tilt = 0.2 + sin(t * 0.4 + seed * 6.28) * 0.15; // Randomized phase

			p_jelly.yz *= mat2(vec2(cos(tilt), sin(tilt)), vec2(-sin(tilt), cos(tilt)));
			p_jelly.xz *= mat2(vec2(cos(spin), sin(spin)), vec2(-sin(spin), cos(spin)));

	        // 3. ORGANIC PULSE
			p_jelly.x += sin(t * 0.5 + seed * 6.28) * 0.15;
			p_jelly.y += cos(t * 0.4 + seed * 6.28) * 0.1;
			p_jelly.z += sin(t * 0.3 + seed * 6.28) * 0.12;

	        float pulse_sin = sin(t * 1.2);
	        float throb = pow(0.5 + 0.5 * pulse_sin, 1.5) * 0.22;
	        throb += music_intensity * 0.01;

	        float sc_xz = 1.0 - (throb * 0.5);
	        float sc_y = 1.0 + throb;

	        vec3 p_pulsed = p_jelly;
	        p_pulsed.xz /= sc_xz;
	        p_pulsed.y /= sc_y;
	        local_p = p_pulsed;

	        // 4. JELLYFISH BELL
	        float bell_shape = sdEllipsoid(p_pulsed - vec3(0, 0.24, 0), vec3(0.32, 0.18, 0.32));
	        float cavity = length(p_pulsed - vec3(0, 0.1, 0)) - 0.18;
	        float bell = max(bell_shape, -cavity);
	        bell *= min(sc_xz, sc_y); 
			
			// 4.5 TENTACLE ROOT (Synchronized Sway)
			vec3 p_root = p_pulsed;
			float root_pulse = 1.0 + throb * 0.5;

			// Define the tip's dynamic motion
			float tip_sway_x = sin(t * 2.0 + 4.0) * 0.15;
			float tip_sway_z = cos(t * 1.5 + 3.5) * 0.1;

			// Points for the capsule
			vec3 a_pt = vec3(0, 0.15, 0); 
			// The tip now moves, preventing the "static spike" look
			vec3 b_pt = vec3(tip_sway_x, -0.4 * root_pulse, tip_sway_z); 

			// Warping the sampling space along the curve for extra floppiness
			float h_val = clamp(dot(p_root - a_pt, b_pt - a_pt) / dot(b_pt - a_pt, b_pt - a_pt), 0.0, 1.0);
			p_root.x += sin(t * 2.0 + h_val * 4.0) * (0.1 * h_val);
			p_root.z += cos(t * 1.5 + h_val * 3.5) * (0.08 * h_val);

			float tapered_rad = mix(0.1, 0.015, h_val);
			float root = sdCapsule(p_root, a_pt, b_pt, tapered_rad);
			bell = smin(bell, root, 0.05);

	        // 5. LOCKED TENTACLES
	        float tentacles = 1000.0;
	        float tentacle_drag = pulse_sin * 0.04; 

	        for(int i = 0; i < 4; i++) {
			    float fi = float(i);
			    float ang = fi * 1.5708;
			    vec3 p_h = p_jelly;
			    p_h.y += tentacle_drag;

			    // Vary the attachment radius and length using the index
			    float var_r = 0.18 + (sin(fi * 1.3) * 0.05); 
			    float attach_r = var_r * sc_xz; 
			    vec3 t_start = vec3(cos(ang) * attach_r, 0.12 * sc_y, sin(ang) * attach_r);
			    
			    // Independent sway and unique lengths (-0.3 to -0.6)
			    float wave_x = sin(t * 1.5 + p_h.y * 8.0 + fi * 2.0) * (0.08 + throb * 0.1);
			    float wave_z = cos(t * 1.2 + p_h.y * 6.0 + fi * 1.5) * (0.05 + throb * 0.08);
			    // Thinner radius: changed from 0.018 to 0.01
			    float strand_rad = 0.01;
			    
			    // Shorter length: changed range to be closer to the bell (-0.1 to -0.3)
			    float unique_len = -0.2 - (sin(fi * 2.5) * 0.1); 
			    
			    vec3 t_end = vec3(t_start.x + wave_x, unique_len, t_start.z + wave_z);
			    
			    float strand = sdCapsule(p_h, t_start, t_end, strand_rad);
			    tentacles = min(tentacles, strand);
			}

	        d = smin(bell, tentacles, 0.08);
	    }
	    break;
		
		case 11: { // X-Wing
            float t = audio_time * 3.0;
            
            // --- 1. SCALE & EFFECT SPACE ---
            // Pivot Y set to 0.0 with .6 multiplier
            vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.6; 
            vec3 p_ship = p_raw - ch_p;
            
            float dx = sin(t * 0.5) * 0.08;
            float dy = sin(t * 0.8) * 0.05;
            float dx_next = sin((t+0.1) * 0.5) * 0.08;
            float dy_next = sin((t+0.1) * 0.8) * 0.05;
            
            float target_yaw = (dx_next - dx) * 5.0; 
            float target_pitch = (dy_next - dy) * 5.0;
            float banking_roll = target_yaw * -2.5; 

            float cp = cos(target_pitch), sp = sin(target_pitch);
            p_ship.yz *= mat2(vec2(cp, sp), vec2(-sp, cp)); 
            float cy = cos(target_yaw), sy = sin(target_yaw);
            p_ship.xz *= mat2(vec2(cy, sy), vec2(-sy, cy)); 

            float roll_phase = t * 0.03;
            float roll_trig = smoothstep(0.9, 1.0, fract(roll_phase)); 
            float roll_ang = ((floor(roll_phase) + roll_trig) * (6.0 * 3.14159)) + banking_roll; 
            p_ship.xy *= mat2(vec2(cos(roll_ang), -sin(roll_ang)), vec2(sin(roll_ang), cos(roll_ang)));

            p_ship.x += dx; p_ship.y += dy;
            local_p = p_ship;
            
            // 2. GEOMETRY
            float body = sdCapsule(p_ship, vec3(0,0,-0.38), vec3(0,0,0.2), 0.14); 
            float nose = sdCapsule(p_ship, vec3(0,0,0.2), vec3(0,-0.02,0.65), 0.045);
            body = smin(body, nose, 0.12);
     
            float rare_trigger = sin(t * 0.2) * sin(t * 0.11);
            float wing_activation = 1.0 - smoothstep(0.3, 0.9, rare_trigger);
            float wing_angle = mix(0.08, 0.45, wing_activation);

            float items = 1e5;
            float trails = 1e5; 
            
            // 3. WING & ENGINE LOOP
            for(float i = -1.0; i <= 1.0; i += 2.0) {
                for(float j = -1.0; j <= 1.0; j += 2.0) {
                    vec3 p_w = p_ship;
                    float cur_ang = wing_angle * j; 
                    p_w.xy *= mat2(vec2(cos(cur_ang), -sin(cur_ang)), vec2(sin(cur_ang), cos(cur_ang)));
                    
                    vec3 eng_pos = vec3(0.18 * i, 0, -0.2);
                    float engine = length(p_w - eng_pos) - 0.085;
                    
                    // Trail logic
                    vec3 trail_p = p_w - (eng_pos - vec3(0, 0, 0.2)); 
                    float pulse = (0.6 + 0.4 * sin(t * 40.0)) * music_intensity * 0.35;
                    float f1 = length(trail_p - vec3(0, 0, -0.1)) - (0.07 * pulse);
                    float f2 = length(trail_p - vec3(0, 0, -0.3)) - (0.04 * pulse);
                    float f3 = length(trail_p - vec3(0, 0, -0.5)) - (0.02 * pulse);
                    float engine_trail = smin(f1, smin(f2, f3, 0.05), 0.05);
                    trails = min(trails, engine_trail);

                    vec3 q_w = abs(p_w - vec3(0.4 * i, 0, -0.1)) - vec3(0.35, 0.01, 0.22);
                    float w = length(max(q_w, 0.0)) + min(max(q_w.x, max(q_w.y, q_w.z)), 0.0);
                    float weapon = sdCapsule(p_w - vec3(0.65 * i, 0, 0.1), vec3(0), vec3(0, 0, 0.25), 0.025);
                    
                    items = min(items, min(w, min(engine, weapon)));
                }
            }
            
            d = smin(body, items, 0.06);
            if (trails < d) { d = trails; res_id = 3.0; }
        }
        break;
		
		case 12: { // Tie Fighter
            float t = audio_time * 3.0;
            
            // --- 1. SCALE & EFFECT SPACE ---
            // Pivot Y set to 0.0 with .6 multiplier
            vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.6; 
            vec3 p_tie = p_raw - ch_p;
            
            float dx = cos(t * 0.7) * 0.08;
            float dy = sin(t * 0.5) * 0.05;
            float dx_next = cos((t+0.2) * 0.7) * 0.08;
            float dy_next = sin((t+0.2) * 0.5) * 0.05;
            
            float target_yaw = (dx_next - dx) * 2.5; 
            float target_pitch = (dy_next - dy) * 2.5;
            float banking_roll = target_yaw * -3.0; 

            float cp = cos(target_pitch), sp = sin(target_pitch);
            p_tie.yz *= mat2(vec2(cp, sp), vec2(-sp, cp)); 
            float cy = cos(target_yaw), sy = sin(target_yaw);
            p_tie.xz *= mat2(vec2(cy, sy), vec2(-sy, cy)); 

            float roll_phase = t * 0.025;
            float roll_trig = smoothstep(0.92, 1.0, fract(roll_phase)); 
            float roll_ang = ((floor(roll_phase) + roll_trig) * (6.0 * 3.14159)) + banking_roll;
            p_tie.xy *= mat2(vec2(cos(roll_ang), -sin(roll_ang)), vec2(sin(roll_ang), cos(roll_ang)));

            p_tie.x += dx; p_tie.y += dy;
            local_p = p_tie;

            // 2. GEOMETRY
            float cockpit = length(p_tie) - 0.14; 
            vec3 p_side = p_tie; p_side.x = abs(p_side.x);
            float pylon = sdCapsule(p_side, vec3(0,0,0), vec3(0.4, 0, 0), 0.06); 
            vec3 p_wing = p_side;
            p_wing.x += pow(abs(p_wing.y), 3.0) * 1.5; 
            vec3 q_p = abs(p_wing - vec3(0.4, 0, 0.05)) - vec3(0.03, 0.42, 0.3);
            float panel = length(max(q_p, 0.0)) + min(max(q_p.x, max(q_p.y, q_p.z)), 0.0);
            float rockets = sdCapsule(p_side - vec3(0.2, -0.06, -0.1), vec3(0), vec3(0, 0, 0.22), 0.045);

            // 3. TWIN ION ENGINE TRAILS
            float trails = 1e5;
            float pulse = (0.7 + 0.3 * sin(t * 50.0)) * music_intensity * 0.75;
            
            for(float i = -1.0; i <= 1.0; i += 2.0) {
                vec3 trail_p = p_tie - vec3(0.06 * i, -0.02, -0.15);
                float f1 = length(trail_p) - (0.05 * pulse);
                float f2 = length(trail_p - vec3(0, 0, -0.2)) - (0.03 * pulse);
                float f3 = length(trail_p - vec3(0, 0, -0.4)) - (0.01 * pulse);
                float single_trail = smin(f1, smin(f2, f3, 0.04), 0.04);
                trails = min(trails, single_trail);
            }

            d = min(cockpit, smin(pylon, panel, 0.08));
            d = min(d, rockets);
            
            if (trails < d) { d = trails; res_id = 3.0; }
        }
        break;
		
		case 13: { // Squid
	        float t = audio_time * 2.0;
	        
	        // --- 1. SCALE & EFFECT SPACE ---
	        vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.7; 
	        vec3 p_sq = p_raw - ch_p;

	        // 2. DRIFT MOVEMENT - Gliding with Subtle Tilt
			float travel_x = sin(t * 0.8) * 0.15;
			float vertical_drift = sin(t * 0.4) * 0.08; // Slower, lazier vertical drift

			p_sq.x += travel_x;
			p_sq.y += vertical_drift;

			// Subtle randomized tilt (Pitch and Roll)
			float roll = sin(t * 0.5) * 0.06;
			float pitch = cos(t * 0.6) * 0.04;
			float cr = cos(roll), sr = sin(roll);
			float cp = cos(pitch), sp = sin(pitch);

			p_sq.xy *= mat2(vec2(cr, -sr), vec2(sr, cr)); // Side-to-side roll
			p_sq.yz *= mat2(vec2(cp, -sp), vec2(sp, cp)); // Forward/back pitch

			local_p = p_sq;

	        // 3. MANTLE BREATHING
	        float breathe = 1.0 + sin(t * 2.5) * 0.15;
	        float taper = smoothstep(-0.2, 0.4, p_sq.y); 
	        vec3 p_head = p_sq - vec3(0, 0.15, 0);
	        
	        float head = sdCapsule(p_head, vec3(0, -0.1, 0), vec3(0, 0.22, 0), 0.08 + (0.12 * taper * breathe));
	        
			// 4. TENTACLE LOOP - Horizontal Flare & Sway
			float tentacles = 1e5;
			// Global side-sway for the entire tentacle cluster
			float cluster_sway = sin(t * 1.2) * 0.15; 

			for(float i = 0.0; i < 16.0; i++) {
			    float angle = i * (3.14159 / 8.0);
			    vec3 p_tent = p_sq;
			    p_tent.xz *= mat2(vec2(cos(angle), -sin(angle)), vec2(sin(angle), cos(angle)));
			    
			    float dist_from_base = max(0.0, -p_tent.y);
			    
			    // Add the horizontal cluster sway to the tentacle space
			    p_tent.x += cluster_sway * dist_from_base;

			    float twist = dist_from_base * 2.5; 
			    float ct = cos(twist), st = sin(twist);
			    p_tent.xz *= mat2(vec2(ct, -st), vec2(st, ct));

			    float ripple = sin(t * 3.5 - dist_from_base * 10.0 + i) * 0.06 * dist_from_base;
			    p_tent.x += ripple;
			    p_tent.z += cos(t * 2.5 + i * 1.5) * 0.04 * dist_from_base;

			    // Spaced out X with horizontal drift influence
			    float len = 0.45 + sin(i * 1.5) * 0.15;
			    float r_outer = 0.02 * (1.0 - dist_from_base * 0.4);
			    float tent_outer = sdCapsule(p_tent, vec3(0.12, 0, 0), vec3(0.25, -len, 0), r_outer);
			    
			    // Inner Ring
			    vec3 p_in = p_sq;
			    float in_ang = (i + 0.5) * (3.14159 / 8.0);
			    p_in.xz *= mat2(vec2(cos(in_ang), -sin(in_ang)), vec2(sin(in_ang), cos(in_ang)));
			    p_in.x += cluster_sway * dist_from_base + sin(t * 5.0 - dist_from_base * 12.0) * 0.02 * dist_from_base;
			    
			    float tent_inner = sdCapsule(p_in, vec3(0.06, 0, 0), vec3(0.1, -0.28, 0), 0.012);
			    
			    tentacles = min(tentacles, min(tent_outer, tent_inner));
			}
			
	        d = smin(head, tentacles, 0.08);
	    }
	    break;
		
		case 14: { // Banana
	        float t = audio_time * 2.5;
	        
	        // --- 1. SCALE & EFFECT SPACE ---
	        vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.75; 
	        
	        float growth = 1.0 + (music_intensity * 0.03);
	        float s = 1.3 / growth; 
	        
	        // 2. GLOBAL XY TRANSLATION
	        float swayX = sin(audio_time * 1.5) * 0.15;
	        float swayY = cos(audio_time * 1.2) * 0.1;
	        
	        vec3 p_ban = (p_raw - ch_p - vec3(swayX, swayY, 0.0)) * s; 
	        
	        // 3. LONG-PERIOD GLOBAL Y ROTATION
	        float yaw = sin(t * 0.12) * 4.0 + cos(t * 0.07) * 3.0; 
	        float cy = cos(yaw), sy = sin(yaw);
	        p_ban.xz *= mat2(vec2(cy, sy), vec2(-sy, cy));

	        // 4. RANDOMIZED TILT
	        float tiltX = sin(t * 0.3) * 0.3 + cos(t * 0.17) * 0.2 + sin(t * 0.8) * 0.05;
	        float tiltZ = cos(t * 0.23) * 0.25 + sin(t * 0.11) * 0.15 + cos(t * 0.9) * 0.04;
	        
	        float cx = cos(tiltX), sx = sin(tiltX);
	        p_ban.yz *= mat2(vec2(cx, sx), vec2(-sx, cx));
	        
	        float cz = cos(tiltZ), sz = sin(tiltZ);
	        p_ban.xy *= mat2(vec2(cz, sz), vec2(-sz, cz));

	        // 5. RARE ROLL
	        float r_phase = t * 0.04;
	        float r_trig = smoothstep(0.9, 1.0, fract(r_phase));
	        float r_ang = (floor(r_phase) + r_trig) * 18.84955;
	        p_ban.xy *= mat2(vec2(cos(r_ang), -sin(r_ang)), vec2(sin(r_ang), cos(r_ang)));

	        local_p = p_ban;

	        // 6. CURVED GEOMETRY
	        vec3 p_curv = p_ban;
	        p_curv.x += pow(p_curv.y, 2.0) * (0.8 * growth);
	        
	        float fruit = sdCapsule(p_curv, vec3(0, -0.4, 0), vec3(0, 0.4, 0), 0.12);
	        float stem = sdCapsule(p_curv, vec3(0, 0.4, 0), vec3(0, 0.5, 0), 0.04);
	        
	        d = smin(fruit, stem, 0.05) / s;
	        res_id = 1.0;
	    } 
		break;
		
		case 15: { // Penis
	        float t = audio_time * 3.0;
	        
	        // --- 1. SCALE & EFFECT SPACE ---
	        vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.6; 
	        
	        // 2. GLOBAL DRIFT
	        float driftX = sin(audio_time * 1.5) * 0.15;
	        float driftY = cos(audio_time * 1.2) * 0.1;
	        vec3 p_world = p_raw - ch_p - vec3(driftX, -0.2 + driftY, 0.0);
	        
	        // 3. SHARED PHYSICS PARAMETERS
	        float sway_angle = sin(t * 0.8) * 0.1; 
	        float s_z = sin(sway_angle), c_z = cos(sway_angle);
	        float flop_intensity = 0.06 + (music_intensity * 0.05);
	        float pulse_pos = pow(abs(sin(t * 0.5)), 3.0) * 0.12;

	        // 4. DEFINE THE BODY
	        vec3 p_stat = p_world;
	        p_stat.xy *= mat2(vec2(c_z, -s_z), vec2(s_z, c_z)); 
	        float y_weight = clamp(p_stat.y, 0.0, 0.4);
	        p_stat.x += sin(t * 1.2 + p_stat.y * 1.5) * flop_intensity * y_weight; 
	        p_stat.z += cos(t * 1.2 + p_stat.y * 1.5) * (flop_intensity * 1.2) * y_weight;
	        p_stat.y += pulse_pos;
	        p_stat.x += pow(max(0.0, p_stat.y + 0.1), 2.0) * 0.4;
	        local_p = p_stat;

	        // 5. GEOMETRY
	        float shaft_throb = 1.0 + sin(t * 2.0) * 0.05 * (1.0 + music_intensity);
	        float shaft = sdCapsule(p_stat, vec3(0, -0.1, 0), vec3(0, 0.25, 0), 0.1 * shaft_throb);
	        float top = length(p_stat - vec3(0, 0.32, 0)) - (0.13 * shaft_throb);
	        vec3 p_base = p_stat; p_base.x = abs(p_base.x);
	        float base = length(p_base - vec3(0.12, -0.12, 0)) - (0.14 * (1.0 + sin(t * 1.5) * 0.04));
	        d = smin(shaft, top, 0.05);
	        d = smin(d, base, 0.1);

	        // 6. RANDOMIZED LOW EXPLOSION
	        float burst_timer = audio_time * 0.12; 
	        float burst_cycle = fract(burst_timer); 
	        float burst_id = floor(burst_timer);
	        float chance = fract(sin(burst_id * 43758.5453));
	        
	        if (chance < 0.2 && burst_cycle < 0.15) { 
	            float p_time = burst_cycle * 6.66; 
	            vec3 tip_origin = vec3(0.0, 0.35 + pulse_pos, 0.0);
	            float bend_tw = clamp(0.35, 0.0, 0.4);
	            tip_origin.x += sin(t * 1.2 + 0.35 * 1.5) * flop_intensity * bend_tw;
	            tip_origin.z += cos(t * 1.2 + 0.35 * 1.5) * (flop_intensity * 1.2) * bend_tw;
	            tip_origin.x += pow(max(0.0, 0.35 + 0.1), 2.0) * 0.4;
	            float tx = tip_origin.x * c_z - tip_origin.y * s_z;
	            float ty = tip_origin.x * s_z + tip_origin.y * c_z;
	            tip_origin = vec3(tx, ty, tip_origin.z);

	            for (int i = 0; i < 12; i++) {
	                float fi = float(i);
	                float rand_seed = burst_id + fi * 4.56;
	                vec3 dir = vec3(sin(rand_seed * 13.0) * 1.2, 3.0 + cos(rand_seed * 9.0) * 0.8, cos(rand_seed * 11.0) * 1.2);
	                vec3 p_pos = tip_origin + (dir * p_time);
	                p_pos.y -= p_time * p_time * 8.0; 
	                float jot = length(p_world - p_pos) - (0.022 * (1.0 - p_time));
	                d = min(d, jot);
	            }
	        }
	        res_id = 1.0;
	    } 
	    break;
		
		case 16: { // Ghost
	        float t = audio_time * 2.0;
	        
	        // --- 1. SCALE & EFFECT SPACE ---
	        vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.6; 
	        vec3 p_gh = p_raw - ch_p;
	        
	        // 2. BASE FLOAT & DRIFT
	        p_gh.y += sin(t) * 0.1;
			p_gh.x += cos(t * 0.5) * 0.05;

			// Random multi-axis tilt
			float tiltX = sin(t * 0.6) * 0.15 + cos(t * 1.3) * 0.05;
			float tiltZ = cos(t * 0.7) * 0.15 + sin(t * 1.1) * 0.05;
			float cx = cos(tiltX), sx = sin(tiltX);
			float cz = cos(tiltZ), sz = sin(tiltZ);
			p_gh.yz *= mat2(vec2(cx, -sx), vec2(sx, cx));
			p_gh.xy *= mat2(vec2(cz, -sz), vec2(sz, cz));

	        // 3. SEAMLESS HEAD TILT
	        float tilt_weight = smoothstep(-0.25, 0.1, p_gh.y); 
	        float head_tilt = sin(t * 1.5) * 0.08 * tilt_weight; 
	        
	        float h_c = cos(head_tilt), h_s = sin(head_tilt);
	        p_gh.xz *= mat2(vec2(h_c, h_s), vec2(-h_s, h_c)); // Y-Yaw
	        p_gh.xy *= mat2(vec2(h_c, -h_s), vec2(h_s, h_c)); // Side-tilt

	        // 4. TAPERED BODY WAVE
	        float wave_envelope = smoothstep(0.2, -0.6, p_gh.y);
	        float body_sway = sin(t * 2.5 + p_gh.y * 4.0) * 0.07 * wave_envelope;
	        p_gh.x += body_sway;

	        local_p = p_gh;

	        // 5. GEOMETRY
	        float body = sdCapsule(p_gh, vec3(0.0, 0.12, 0.0), vec3(0.0, -0.45, 0.0), 0.22);
	        
	        // Scrolling Lip
	        float lip_strength = smoothstep(0.0, -0.6, p_gh.y);
	        p_gh.z += lip_strength * 0.18;
	        
	        // Bottom Ripple
	        float wave = sin(p_gh.x * 16.0 + t * 4.0) * 0.035;
	        float cut = p_gh.y + 0.45 + wave;
	        
	        // Hollow Eyes
	        vec3 p_e = p_gh; 
	        p_e.x = abs(p_e.x) - 0.08;
	        float eyes = sdCapsule(p_e, vec3(0.0, 0.12, 0.1), vec3(0.0, 0.12, 0.3), 0.038);

	        // Assembly
	        d = max(body, -cut);
	        d = max(d, -eyes);
	    }
	    break;
		
		case 17: { // Deep Sea Jellyfish
	        float t = audio_time * 0.7; 
	        
	        // --- 1. SCALE & EFFECT SPACE ---
	        vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.7; 
	        vec3 p_med = p_raw - ch_p;
	        
	        // 2. FLOATING DRIFT
	        p_med.x -= cos(t * 0.6) * 0.15;
			p_med.y -= sin(t * 0.8) * 0.12 + 0.25;

			// Lazy rolling tilt
			float roll = sin(t * 0.3) * 0.05;
			float pitch = cos(t * 0.4) * 0.04;
			float cr = cos(roll), sr = sin(roll);
			float cp = cos(pitch), sp = sin(pitch);
			p_med.xy *= mat2(vec2(cr, -sr), vec2(sr, cr)); // Roll
			p_med.yz *= mat2(vec2(cp, -sp), vec2(sp, cp)); // Pitch

	        // 3. ORGANIC PULSE
	        float pulse_t = audio_time * 2.2;
	        float pulse_sin = sin(pulse_t);
	        float throb = pow(0.5 + 0.5 * pulse_sin, 1.5) * 0.22;
	        throb += music_intensity * 0.05;

	        float sc_xz = 1.0 - (throb * 0.6);
	        float sc_y = 1.0 + throb; 

	        vec3 p_pulsed = p_med;
	        p_pulsed.xz /= sc_xz;
	        p_pulsed.y /= sc_y; 

	        local_p = p_pulsed;

	        // 4. BELL GEOMETRY
	        float bell = length(p_pulsed * vec3(0.6, 1.2, 0.6)) - 0.28;
	        bell = max(bell, -p_pulsed.y); 
	        float d_bell = bell * min(sc_xz, sc_y); 

	        // 5. TENTACLES WITH UNIQUE PER-STRAND MOTION
			float hair = 1e5;
			float tentacle_y_offset = pulse_sin * 0.08; 

			for(float i = 0.0; i < 16.0; i++) {
			    // Generate unique seeds based on the index
			    float seed = fract(sin(i * 437.12) * 98.43);
			    float ang = i * (3.14159 / 8.0);
			    vec3 p_h = p_med; 
			    
			    // Unique lateral sway per tentacle
			    float depth = clamp(-p_h.y * 1.5, 0.0, 1.0);
			    float unique_sway_x = sin(audio_time * (1.0 + seed) + seed * 6.28) * 0.15;
			    float unique_sway_z = cos(audio_time * (0.8 + seed * 0.5) + i) * 0.1;
			    p_h.x += unique_sway_x * depth;
			    p_h.z += unique_sway_z * depth;

			    p_h.y += tentacle_y_offset;
			    p_h.xz *= mat2(vec2(cos(ang), -sin(ang)), vec2(sin(ang), cos(ang)));
			    
			    // Individualized wave frequency and phase
			    float wave_speed = audio_time * (4.0 + seed * 3.0);
			    float dynamic_wave = (0.05 + throb * 0.2) * (0.8 + seed * 0.4);
			    float wave = sin(wave_speed - p_h.y * (6.0 + seed * 4.0) + i) * dynamic_wave;
			    
			    // Vary the length slightly for a more organic silhouette
			    float unique_len = -0.6 - (seed * 0.3);
			    float strand = sdCapsule(p_h, vec3(0.18, 0, 0), vec3(0.2 + wave, unique_len, 0), 0.01 + (seed * 0.005));
			    hair = min(hair, strand);
			}
	        
	        d = smin(d_bell, hair, 0.1);
	    }
	    break;
		
		case 18: { // Sentry
	        float t = audio_time * 1.2;
	        
	        // --- 1. SCALE & EFFECT SPACE ---
			vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.6; 

			// Added: Large horizontal drift and layered vertical bob
			float driftX = sin(t * 0.75) * 0.075;
			float driftZ = cos(t * 0.5) * 0.15;
			float bob = (sin(t * 2.0) * 0.03) + (sin(t * 0.3) * 0.05);

			vec3 p_sen = p_raw - ch_p - vec3(driftX, -0.2 + bob, driftZ);
			local_p = p_sen;

	        // 2. CYLINDRICAL LOWER PIVOT
	        vec3 p_piv = p_sen - vec3(0, -0.15, 0);
	        float base_pivot = max(length(p_piv.xz) - 0.12, abs(p_piv.y) - 0.05) - 0.01;
	        
	        float legs = 1e5;
	        for(float i = 0.0; i < 3.0; i++) {
	            float ang = i * (6.28318 / 3.0) + (3.14159 / 6.0);
	            vec3 p_leg = p_sen;
	            p_leg.xz *= mat2(vec2(cos(ang), -sin(ang)), vec2(sin(ang), cos(ang)));
	            
	            float stomp_cycle = fract(audio_time * 0.75 - i * 0.33);
	            float lift = smoothstep(0.0, 0.5, stomp_cycle) * smoothstep(1.0, 0.5, stomp_cycle) * 0.08;
	            
	            vec3 leg_end = vec3(0.32, -0.32 + lift, 0.0);
	            vec3 knee_pos = vec3(0.25, -0.25 + lift * 0.5, 0.0);
	            
	            float leg_main = sdCapsule(p_leg, vec3(0.08, -0.15, 0), knee_pos, 0.025);
	            float foot_seg = sdCapsule(p_leg, knee_pos, leg_end, 0.02);
	            
	            vec3 p_pad = p_leg - (leg_end - vec3(0, 0.02, 0));
	            float anchor = max(length(p_pad.xz) - 0.07, abs(p_pad.y) - 0.02) - 0.01;
	            
	            legs = min(legs, min(leg_main, min(foot_seg, anchor)));
	        }

	        // 3. THE NECK
	        float neck = sdCapsule(p_sen, vec3(0, -0.15, 0), vec3(0, 0.1, 0), 0.045);

	        // 4. THE ROTATING TURRET (Horizontal & Vertical Aim)
			float scan = (sin(t * 0.4) * 1.2) + 3.14159; // Yaw
			float aim = sin(t * 0.25) * 0.2; // Pitch (Up/Down)

			vec3 p_turret = p_sen - vec3(0, 0.12, 0);

			// Horizontal Rotation (Yaw)
			float cy = cos(scan), sy = sin(scan);
			p_turret.xz *= mat2(vec2(cy, -sy), vec2(sy, cy));

			// Vertical Rotation (Pitch)
			float cp = cos(aim), sp = sin(aim);
			p_turret.yz *= mat2(vec2(cp, -sp), vec2(sp, cp));
	        
	        vec3 b_size = vec3(0.15, 0.1, 0.15);
	        vec3 q_b = abs(p_turret) - b_size;
	        float body = length(max(q_b, 0.0)) + min(max(q_b.x, max(q_b.y, q_b.z)), 0.0) - 0.02;
	        
	        // 5. TOP SENSOR & BARRELS (Deliberate Firing)
			vec3 p_top = p_turret - vec3(0.0, 0.12, 0.0);
			vec3 t_size = vec3(0.06, 0.04, 0.06);
			vec3 q_t = abs(p_top) - t_size;
			float top_box = length(max(q_t, 0.0)) + min(max(q_t.x, max(q_t.y, q_t.z)), 0.0);

			vec3 p_bar = p_turret;
			p_bar.x = abs(p_bar.x) - 0.07;

			float barrels = sdCapsule(p_bar, vec3(0, 0, 0.1), vec3(0, 0, 0.4), 0.04);

	        d = min(base_pivot, legs);
	        d = min(d, neck);
	        d = smin(d, body, 0.02);
	        d = min(d, top_box);
	        d = min(d, barrels);
	    }
	    break;
		
		case 19: { // Frying Pan
	        float t = audio_time * 3.5;

	        // --- 1. SCALE & EFFECT SPACE ---
			vec3 p_raw = (p - vec3(0.0, -0.15, 0.0)) * 0.6; 

			// Added: Lazy floating translation
			float driftX = sin(t * 0.4) * 0.075;
			float driftY = cos(t * 0.5) * 0.1;
			float driftZ = sin(t * 0.3) * 0.1;

			// 2. PIVOT AT THE BOTTOM OF THE HANDLE
			vec3 p_pan = p_raw - ch_p - vec3(driftX, -0.4 + driftY, driftZ);

	        // 3. FLUID SWINGING ANIMATION
	        float swing_x = sin(t) * 0.5;      
	        float swing_z = cos(t * 0.8) * 0.3; 
	    
	        p_pan.xz *= mat2(vec2(cos(swing_x), -sin(swing_x)), vec2(sin(swing_x), cos(swing_x)));
	        p_pan.yz *= mat2(vec2(cos(swing_z), -sin(swing_z)), vec2(sin(swing_z), cos(swing_z)));
	        local_p = p_pan;

	        // 4. GEOMETRY
	        float handle = sdCapsule(p_pan, vec3(0, 0, 0), vec3(0, 0.5, 0), 0.025);    

	        // The Bowl (Attached to the end of the longer handle)
	        vec3 p_bowl = p_pan - vec3(0, 0.55, 0); 
	        float bowl = max(length(p_bowl.xy) - 0.25, abs(p_bowl.z) - 0.03) - 0.01;
	        
	        d = smin(bowl, handle, 0.02);
	    } 
	    break;
		
		case 20: { // GLaDOS
	        float t = audio_time * 1.5;
	        
	        // --- 1. SCALE & EFFECT SPACE ---
	        vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.6; 
	        vec3 p_gl = p_raw - ch_p;
	        
	        // 2. Ceiling Mount (Fixed)
	        float arm_mount = sdCapsule(p_gl, vec3(0, 0.6, -0.1), vec3(0, 0.2, 0), 0.05);

	        // 3. Global Body Bob
	        p_gl.y += sin(t * 2.0) * 0.05;
	        
	        // RANDOMIZED DIRECTION LOGIC
	        float torso_rot = sin(t * 0.4) * 2.0 + sin(t * 0.15) * 1.5 + cos(t * 0.7) * 0.5;
	        p_gl.xz *= mat2(vec2(cos(torso_rot), -sin(torso_rot)), vec2(sin(torso_rot), cos(torso_rot)));
	        local_p = p_gl;

	        // 4. CHASSIS
	        float chassis = length(p_gl * vec3(1.1, 0.8, 1.1) - vec3(0, 0.1, 0)) - 0.16;
	        
	        // 5. HINGED APPENDAGES
	        float plates = 1e5;
	        for(float i = 0.0; i < 4.0; i++) {
	            float side = (mod(i, 2.0) == 0.0) ? 1.0 : -1.0;
	            float height = (i < 2.0) ? 0.2 : 0.0;
	            
	            vec3 pivot = vec3(0.13 * side, height, 0.0);
	            vec3 p_arm = p_gl - pivot;
	            
	            float flare = (0.2 + sin(t * 2.5 + i) * 0.15) * music_intensity;
	            float tilt = cos(t * 1.5 + i) * 0.1;
	            
	            p_arm.xy *= mat2(vec2(cos(flare * side), -sin(flare * side)), vec2(sin(flare * side), cos(flare * side)));
	            p_arm.yz *= mat2(vec2(cos(tilt), -sin(tilt)), vec2(sin(tilt), cos(tilt)));
	            
	            float plate = sdCapsule(p_arm, vec3(0), vec3(0.2 * side, -0.1, 0.05), 0.022);
	            plates = min(plates, plate);
	        }
	        
	        // 6. THE HEAD
	        float head_wander = sin(t * 1.1) * 0.4 + cos(t * 0.3) * 0.2;
	        vec3 p_head = p_gl - vec3(0, -0.12, 0);
	        p_head.xz *= mat2(vec2(cos(head_wander), -sin(head_wander)), vec2(sin(head_wander), cos(head_wander)));
	        float head = sdCapsule(p_head, vec3(0, 0, 0), vec3(0, 0, 0.18), 0.07);
	        float eye = length(p_head - vec3(0, 0, 0.2)) - 0.025;

	        d = min(min(arm_mount, chassis), min(plates, head));
	        d = smin(d, eye, 0.01);
	    }
	    break;
		
		case 21: { // Potato
            float t = audio_time * 0.7;
            vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.6; 
            vec3 p_pot = p_raw - ch_p;
            
            // Suble drift and sway
            p_pot.x -= cos(t * 0.6) * 0.15;
            p_pot.y -= sin(t * 0.8) * 0.12;
            float sway_x = sin(t * 0.4) * 0.4, sway_z = cos(t * 0.5) * 0.3;
            p_pot.xz *= mat2(vec2(cos(sway_x), -sin(sway_x)), vec2(sin(sway_x), cos(sway_x)));
            p_pot.yz *= mat2(vec2(cos(sway_z), -sin(sway_z)), vec2(sin(sway_z), cos(sway_z)));
            
            p_pot.y -= music_intensity * 0.03;
            float c_b = cos(0.4 * p_pot.y), s_b = sin(0.4 * p_pot.y);
            p_pot.xy *= mat2(vec2(c_b, -s_b), vec2(s_b, c_b));
            local_p = p_pot;

            // --- BREATHING LOGIC ---
            float breath = sin(audio_time * 2.0) * 0.5 + 0.5;
            float p_scale = 1.0 - (breath * 0.03 + music_intensity * 0.02);

            // GEOMETRY (Applied p_scale to the length calculations)
            vec3 p_lump = p_pot;
            float noise = sin(p_lump.x * 10.0 + 1.2) * cos(p_lump.y * 14.0 + 0.5) * sin(p_lump.z * 8.0);
            p_lump += noise * 0.04; 
            
            float potato = length(p_lump * vec3(1.2, 0.9, 1.5)) - (0.22 / p_scale);
            
            vec3 p_core = p_pot - vec3(0.05, 0.05, 0.2);
            float core = length(p_core) - (0.07 / p_scale);
            float eye = length(p_core - vec3(0, 0, 0.06)) - 0.015;
            
            float wires = min(sdCapsule(p_pot, vec3(0.1, 0.15, 0.1), vec3(0.02, -0.15, 0.22), 0.012),
                              sdCapsule(p_pot, vec3(-0.12, 0.05, 0.05), vec3(-0.05, 0.1, 0.2), 0.01));

            d = smin(potato, core, 0.03);
            d = min(d, min(wires, eye));
            d *= p_scale; // Correct distance field scaling
        }
        break;
		
	    case 22: { // Companion Cube
            float t = audio_time;
            vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.6; 
            vec3 p_cube = (p_raw - ch_p) / 0.8; 
            
            // --- DRIFT & ROTATION ---
            p_cube.y += sin(t * 0.8) * 0.1;
            p_cube.x += cos(t * 0.5) * 0.15;
            float rot = t * 0.6;
            p_cube.xz *= mat2(vec2(cos(rot), -sin(rot)), vec2(sin(rot), cos(rot)));
            p_cube.yz *= mat2(vec2(cos(rot*0.6), -sin(rot*0.6)), vec2(sin(rot*0.6), cos(rot*0.6)));
            local_p = p_cube;

            // --- BREATHING & PULSE ---
            float breath = sin(t * 2.5) * 0.5 + 0.5;
            float body_pulse = breath * 0.01 + (music_intensity * 0.01);
            float heart_pulse = clamp(breath * 0.02 + (music_intensity * 0.06), 0.0, 0.04);

            // --- 3. ROUNDED BOX BODY ---
            // 'rad' defines the curvature of the surfaces
            float rad = 0.03; 
            vec3 b = vec3(0.20 + body_pulse); // Shrink base size to accommodate radius
            vec3 q = abs(p_cube) - b;
            float base = length(max(q, 0.0)) + min(max(q.x, max(q.y, q.z)), 0.0) - rad;
            d = base; res_id = 1.0; 
            
            vec3 q_g = abs(p_cube) - (0.22 + body_pulse);
            float guards = length(max(q_g + 0.02, 0.0)) - rad;
            d = min(d, guards);

            // --- 4. CIRCLE HOLDER ---
            vec3 p_h = abs(p_cube);
            float max_ax = max(p_h.x, max(p_h.y, p_h.z));
            float circle_dist = min(min(length(p_cube.yz), length(p_cube.xz)), length(p_cube.xy)) - (0.14 + body_pulse);
            float holder = max(circle_dist, max_ax - (0.25 + body_pulse));
            if (holder < d) { d = holder; res_id = 2.0; }

            // --- 5. HEART ---
            vec2 uv = (p_h.x > p_h.y && p_h.x > p_h.z) ? p_cube.yz : (p_h.y > p_h.z ? p_cube.xz : p_cube.xy);
            uv.y -= 0.025; 
            float x_side = abs(uv.x) * 1.4; 
            float y_side = uv.y * 1.8; 
            
            float h_v = max(x_side + y_side, x_side - y_side * 0.4) - (0.07 + heart_pulse); 
            float h_top = length(vec2(x_side - 0.04, y_side - 0.03)) - (0.04 + heart_pulse); 
            float h_shape = min(h_v, h_top);
            
            // Maintain a slight offset (0.261) to keep the heart above the rounded face
            float heart = max(h_shape, max_ax - (0.261 + body_pulse));
            if (heart < d) { d = heart; res_id = 3.0; }
            
            d *= 0.8;
        }
        break;
		
		case 23: { // Bread
            float t = audio_time;
            vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.6; 
            float growth = 1.0 + (music_intensity * 0.04); 
            vec3 p_toast = (p_raw - ch_p) / (growth * 1.25); 
            
            // --- SUBTLE DYNAMIC SWAY ---
            p_toast.x -= sin(t * 1.5) * 0.04;
            p_toast.y -= cos(t * 1.2) * 0.03;
            
            // Lower base sway and capped music influence
            // The 0.04 multiplier keeps the movement grounded even during loud peaks
            float music_sway = min(music_intensity * 0.04, 0.03);
            float bread_tilt = sin(t * 1.2) * (0.05 + music_sway); 
            
            p_toast.xy *= mat2(vec2(cos(bread_tilt), -sin(bread_tilt)), 
                               vec2(sin(bread_tilt), cos(bread_tilt)));
            
            local_p = p_toast; 

            // BREAD GEOMETRY
            vec3 b_base = vec3(0.13, 0.09, 0.04); 
            vec3 q_b = abs(p_toast - vec3(0.0, -0.04, 0.0)) - b_base;
            float base = length(max(q_b, 0.0)) + min(max(q_b.x, max(q_b.y, q_b.z)), 0.0) - 0.02;
            float hump_l = length(p_toast - vec3(-0.06, 0.07, 0.0)) - 0.12;
            float hump_r = length(p_toast - vec3(0.06, 0.07, 0.0)) - 0.12;
            float crown = smin(hump_l, hump_r, 0.08);
            float d_bread = smin(base, crown, 0.05);

            d = d_bread;
            res_id = 1.0; 

            // --- LOW FREQUENCY KNIFE CHANCE ---
            float knife_cycle = sin(t * 0.15 + (v_auto_seed * 13.45));
            
            if (knife_cycle > 0.85) { 
                float spread_t = t * 2.5; 
                float vertical_bob = sin(t * 3.1) * 0.02 + cos(t * 1.8) * 0.01;
                
                vec3 k_p = p_toast - vec3(sin(spread_t) * 0.1, -0.12 + vertical_bob, 0.1); 
                
                float k_roll = 0.1 + sin(spread_t) * 0.15;
                k_p.xy *= mat2(vec2(cos(k_roll), -sin(k_roll)), 
                               vec2(sin(k_roll), cos(k_roll)));

                vec3 blade_size = vec3(0.0075, 0.22, 0.005); 
                vec3 q_k = abs(k_p) - blade_size;
                float d_knife = length(max(q_k, 0.0)) + min(max(q_k.x, max(q_k.y, q_k.z)), 0.0);

                if (d_knife < d) {
                    d = d_knife;
                    res_id = 1.0; 
                }
            }
        } 
        break;
		
		case 24: { // Sticky Bomb
            float t = audio_time;
            float t_rot = t * 0.6;
            
            // --- 1. SCALE & EFFECT SPACE ---
            vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.6; 
            vec3 p_bomb = p_raw - ch_p;
            
            // 2. SWAYING & BOBBING
            p_bomb.x -= sin(t * 1.2) * 0.15;
            p_bomb.y -= cos(t * 1.2) * 0.08;
            
            // 3. SLOW TUMBLE ROTATION
            float s = sin(t_rot); float c = cos(t_rot);
            p_bomb.xz *= mat2(vec2(c, -s), vec2(s, c));
            p_bomb.yz *= mat2(vec2(c, -s), vec2(s, c));
            
            local_p = p_bomb;

            // 4. TUNED STEPPED ANIMATION
            float anim = sin(t * 3.0);
            float dart_stay = sign(anim) * pow(abs(anim), 0.3); 
            
            // Increased base to 0.17 and travel range to 0.03
            float music_kick = min(music_intensity * 0.05, 0.04);
            float spike_ext = 0.17 + (dart_stay * 0.03 + 0.03) + music_kick;

            // 5. GEOMETRY
            float core = length(p_bomb) - 0.14;
            vec3 p_spike = abs(p_bomb);
            
            // Tapered Pointy Tips
            float r_base = 0.04;
            float r_tip = 0.0005; 
            
            float s1 = sdCapsule(p_spike, vec3(0.0), vec3(spike_ext, 0.0, 0.0), mix(r_base, r_tip, clamp(p_spike.x / spike_ext, 0.0, 1.0)));
            float s2 = sdCapsule(p_spike, vec3(0.0), vec3(0.0, spike_ext, 0.0), mix(r_base, r_tip, clamp(p_spike.y / spike_ext, 0.0, 1.0)));
            float s3 = sdCapsule(p_spike, vec3(0.0), vec3(0.0, 0.0, spike_ext), mix(r_base, r_tip, clamp(p_spike.z / spike_ext, 0.0, 1.0)));

            d = smin(core, s1, 0.02);
            d = smin(d, s2, 0.02);
            d = smin(d, s3, 0.02);
        } 
        break;
		
		case 25: { // Katana
            float t = audio_time * 2.2; 
            vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.6; 
            vec3 p_k_origin = p_raw - ch_p;
            
            d = 1000.0; 
            float motion_speed = abs(cos(t * 2.0));
            float ghost_factor = smoothstep(0.1, 0.9, motion_speed); 
            const int TRAIL_COUNT = 12; 

            // 1. Process all GHOSTS first
            for(int i = TRAIL_COUNT - 1; i > 0; i--) {
                float current_delay = -0.04 * ghost_factor;
                float t_inst = t + float(i) * current_delay;
                
                vec3 p_inst = p_k_origin;
                p_inst.x -= sin(t_inst * 1.0) * 0.08;
                p_inst.y -= cos(t_inst * 1.2) * 0.05;
                p_inst.y += 0.35; 

                float slash_cycle = sin(t_inst * 2.0);
                float slash_angle = sign(slash_cycle) * pow(abs(slash_cycle), 0.15) * 0.4;
                
                float s_rot = sin(slash_angle); float c_rot = cos(slash_angle);
                p_inst.xy *= mat2(vec2(c_rot, -s_rot), vec2(s_rot, c_rot));
                p_inst.y -= 0.35;

                float blade = sdCapsule(p_inst, vec3(0.0, -0.05, 0.0), vec3(0.0, 0.65, 0.0), 0.016);
                float handle = sdCapsule(p_inst, vec3(0.0, -0.45, 0.0), vec3(0.0, -0.05, 0.0), 0.022);
                float guard = length(p_inst - vec3(0.0, -0.05, 0.0)) - 0.045;
                guard = max(guard, abs(p_inst.y + 0.05) - 0.012);
                
                float d_ghost = min(smin(handle, blade, 0.01), guard);
                float vertical_mask = smoothstep(-0.05, 0.05, p_inst.y);

                if (d_ghost < d) {
                    d = smin(d, d_ghost, 0.005);
                    res_id = (1.0 - (float(i) / float(TRAIL_COUNT))) * ghost_factor * vertical_mask;
                }
            }

            // 2. Process the MAIN katana last and FORCE it to overwrite
            vec3 p_main = p_k_origin;
            p_main.x -= sin(t * 1.0) * 0.08;
            p_main.y -= cos(t * 1.2) * 0.05;
            p_main.y += 0.35;
            float m_slash = sin(t * 2.0);
            float m_angle = sign(m_slash) * pow(abs(m_slash), 0.15) * 0.4;
            float ms = sin(m_angle); float mc = cos(m_angle);
            p_main.xy *= mat2(vec2(mc, -ms), vec2(ms, mc));
            p_main.y -= 0.35;

            float m_blade = sdCapsule(p_main, vec3(0.0, -0.05, 0.0), vec3(0.0, 0.65, 0.0), 0.016);
            float m_handle = sdCapsule(p_main, vec3(0.0, -0.45, 0.0), vec3(0.0, -0.05, 0.0), 0.022);
            float m_guard = length(p_main - vec3(0.0, -0.05, 0.0)) - 0.045;
            m_guard = max(m_guard, abs(p_main.y + 0.05) - 0.012);
            float d_main = min(smin(m_handle, m_blade, 0.01), m_guard);

            // If we are anywhere near the main katana, it wins the ID and the distance
            if (d_main < d + 0.02) {
                d = d_main; 
                res_id = 1.0; 
                local_p = p_main;
            }
        }
        break;
		
		case 26: { // Star
            float t = audio_time;
            vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.6; 
            vec3 p_world = p_raw - ch_p;
            
            vec3 p_star = p_world;

            // --- SWAY ---
            p_star.x -= sin(t * 0.8) * 0.15;
            p_star.y -= cos(t * 0.6) * 0.12;

            // --- SPIN & STABILITY ---
            float pulse = sin(t * 0.5); 
            float speed_boost = clamp(pulse * 10.0, -6.0, 6.0); 
            float rot_t = t * 1.4 + speed_boost; 

            float s, c;
            s = sin(rot_t * 0.9); c = cos(rot_t * 0.9);
            p_star.xy *= mat2(vec2(c, -s), vec2(s, c));
            s = sin(rot_t * 1.3); c = cos(rot_t * 1.3);
            p_star.xz *= mat2(vec2(c, -s), vec2(s, c));
            s = sin(rot_t * 0.7); c = cos(rot_t * 0.7);
            p_star.yz *= mat2(vec2(c, -s), vec2(s, c));

            local_p = p_star;

            // --- BREATHING EFFECT ---
            // Subtle expansion and contraction
            float breath = sin(t * 2.0) * 0.02; 

            // --- SMOOTH SHAPE ---
            float a_main = atan(p_star.x, p_star.y) + 3.14159;
            float r_main = length(p_star.xy);
            
            // star_val_main is our base shape
            float star_val_main = 0.6 + 0.4 * cos(5.0 * a_main);
            
            // We apply 'breath' here to the base radius
            float radius_main = (0.14 + breath) + 0.08 * star_val_main; 
			
            // Final smoothing: we subtract 0.03 from the result and add 0.03 to the total
            // This "rounds" the sharp corners of the SDF.
            d = max(r_main - radius_main, abs(p_star.z) - 0.04) - 0.03;
			
			// --- CONTAINED POP-BURST ---
            float burst_timer = t * 0.4; 
            float burst_cycle = fract(burst_timer); 
            float burst_id = floor(burst_timer);

            for (int i = 0; i < 4; i++) {
                float fi = float(i);
                float seed = burst_id + fi * 1.57;
                
                // Reduced travel distance (multiplier 2.5 instead of 6.0)
                vec3 dir = vec3(hash(seed)-0.5, hash(seed+1.1)-0.5, hash(seed+1.2)-0.5) * 2.5;

                float p_life = burst_cycle; 
                vec3 p_pos = dir * p_life;  
                
                vec3 p_mini = p_world - p_pos;
                
                // GROW & POP
                float grow = smoothstep(0.0, 0.2, p_life); // Faster grow
                float shrink = pow(1.0 - p_life, 5.0);    // Even sharper "snap"
                float scale = grow * shrink;
                
                float m_a = atan(p_mini.x, p_mini.y) + (t * 20.0 + fi); 
                float m_r = length(p_mini.xy);
                float m_star_val = 0.5 + 0.5 * cos(5.0 * m_a);
                
                float m_radius = (0.25 + 0.15 * m_star_val) * scale;
                
                if (m_radius > 0.005) {
                    float d_mini = max(m_r - m_radius, abs(p_mini.z) - 0.02) - 0.01;
                    d = smin(d, d_mini, 0.01);
                }
            }
		}
        break;
        
        case 27: { // Die
            float t = audio_time;
            vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.6; 
            vec3 p_die = (p_raw - ch_p) / 0.8; 
            
            p_die.y += cos(t * 0.7) * 0.12;
            p_die.x += sin(t * 0.9) * 0.1;

            float spin_burst = smoothstep(0.75, 1.0, cos(t * 0.35)); 
            float speed = 0.6 + (spin_burst * 10.0); 
            float rot = t * speed;

            p_die.xz *= mat2(vec2(cos(rot), -sin(rot)), vec2(sin(rot), cos(rot)));
            p_die.yz *= mat2(vec2(cos(rot*0.6), -sin(rot*0.6)), vec2(sin(rot*0.6), cos(rot*0.6)));
            local_p = p_die;

            // --- 1. ROUNDED BOX BODY ---
            float rad = 0.03; // The bevel radius
            vec3 b_size = vec3(0.20); // Shrink from 0.22 to account for radius
            vec3 q_box = abs(p_die) - b_size;
            d = length(max(q_box, 0.0)) + min(max(q_box.x, max(q_box.y, q_box.z)), 0.0) - rad;
            res_id = 1.0; 

            // 2. Pip Logic
            float p_r = 0.045; float g = 0.11; float pips = 1e5;
            vec3 a = abs(p_die);
            if (a.z > a.x && a.z > a.y) {
                if (p_die.z > 0.0) { pips = length(p_die.xy) - p_r; } // 1
                else { // 6
                    vec2 f6 = abs(p_die.xy) - vec2(g, g);
                    pips = min(length(f6) - p_r, length(vec2(abs(p_die.x) - g, p_die.y)) - p_r);
                }
            } else if (a.x > a.y && a.x > a.z) {
                if (p_die.x > 0.0) { pips = min(length(p_die.yz - g), length(p_die.yz + g)) - p_r; } // 2
                else { pips = min(length(abs(p_die.yz) - g) - p_r, length(p_die.yz) - p_r); } // 5
            } else {
                if (p_die.y > 0.0) { // 3
                    pips = min(length(p_die.xz) - p_r, min(length(p_die.xz - g) - p_r, length(p_die.xz + g) - p_r));
                } else { pips = length(abs(p_die.xz) - g) - p_r; } // 4
            }

            // --- 3. PIP PROJECTION ---
            // Adjusted depth to 0.245 so pips sit correctly on the rounded surface
            float pip_comp = max(pips, max(a.x, max(a.y, a.z)) - 0.245); 
            if (pip_comp < d) { d = pip_comp; res_id = 2.0; }
            
            d *= 0.8;
        }
        break;
        
        case 28: { // Rocket
            float t = audio_time;
            vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.6; 
            vec3 p_rock = (p_raw - ch_p) / 0.8; 

            // Steering
            float mx = sin(t * 1.2) * 0.4;
            float my = cos(t * 0.8) * 0.3;
            p_rock.x -= mx; p_rock.y -= my;

            float dx = cos(t * 1.2), dy = -sin(t * 0.8);
            float yaw = dx * 0.4, pitch = dy * 0.4;  
            p_rock.xz *= mat2(vec2(cos(yaw), sin(yaw)), vec2(-sin(yaw), cos(yaw)));
            p_rock.yz *= mat2(vec2(cos(pitch), sin(pitch)), vec2(-sin(pitch), cos(pitch)));
            p_rock.xz *= mat2(vec2(-1.0, 0.0), vec2(0.0, -1.0)); // Facing Away
            p_rock.xy *= mat2(vec2(cos(t*2.5), sin(t*2.5)), vec2(-sin(t*2.5), cos(t*2.5)));

            local_p = p_rock;
            vec3 a = abs(p_rock);

            float body = max(length(p_rock.xy) - 0.12, a.z - 0.2);
            float taper = 0.12 - ((p_rock.z - 0.2) * 0.22); 
            float nose = max(length(p_rock.xy) - taper, max(p_rock.z - 0.7, -p_rock.z + 0.2));
            float fins = min(max(a.x - 0.01, max(a.y - 0.22, max(-p_rock.z - 0.25, p_rock.z - 0.0))),
                             max(a.y - 0.01, max(a.x - 0.22, max(-p_rock.z - 0.25, p_rock.z - 0.0))));
            
            d = min(body, min(nose, fins));
            res_id = 1.0;

            vec3 p_fire = p_rock - vec3(0.0, 0.0, -0.22);
            float f_cone = max(length(p_fire.xy) - (0.08 * (1.0 + p_fire.z * 2.0)), max(p_fire.z + 0.05, -p_fire.z - 0.4));
            float exhaust = f_cone + sin(p_fire.z * 100.0 + t * 60.0) * (0.025 * music_intensity);
            
            if (exhaust < d) { d = exhaust; res_id = 2.0; }
            d *= 0.8; 
        }
        break;
        
        case 29: { // Snowman
            float t = audio_time;
            vec3 p_raw = (p - vec3(0.0, -0.25, 0.0)) * 0.5; 
            vec3 p_snow = (p_raw - ch_p) / 0.7; 
            
            // --- JUMP & SWAY ---
            float jump_cycle = sin(t * 3.0);
            float jump_y = max(0.0, jump_cycle) * 0.12; 
            p_snow.y -= jump_y;

            float scoot_x = sin(t * 0.8) * 0.15;
            p_snow.x -= scoot_x;
            float turn_yaw = scoot_x * 0.6;
            float s = sin(turn_yaw), c = cos(turn_yaw);
            p_snow.xz *= mat2(vec2(c, -s), vec2(s, c));

            float pulse = (music_intensity * 0.008) + sin(t * 2.0) * 0.012; 
	            local_p = p_snow;

	            // --- SPRING SEGMENTS ---
	            float lean = scoot_x * 0.25;
	            float mid_hop = max(0.0, sin(t * 3.0 - 0.2)) * 0.08;
	            float head_hop = max(0.0, sin(t * 3.0 - 0.4)) * 0.15;

	            float bottom = length(p_snow - vec3(0.0, -0.3, 0.0)) - (0.25 + pulse);
	            float middle = length(p_snow - vec3(lean, 0.05 + mid_hop, 0.0)) - (0.18 + pulse);
	            vec3 head_pos = vec3(lean * 1.2, 0.32 + head_hop, 0.0);
	            float head = length(p_snow - head_pos) - (0.12 + pulse);
	            
	            d = min(bottom, min(middle, head));

	            // --- STOVE TOP HAT ---
	            vec3 p_hat = p_snow - head_pos - vec3(0.0, 0.1, 0.0);
	            float brim = max(length(p_hat) - 0.16, abs(p_hat.y) - 0.01); 
	            float cylinder = max(length(p_hat.xz) - 0.08, abs(p_hat.y - 0.09) - 0.09);
	            d = min(d, min(brim, cylinder));

	            // --- LOWERED TWIG ARMS ---
	            float arm_speed = 4.0; 
	            float arm_range = 0.15 + music_intensity * 0.2;
	            
	            for(float i = -1.0; i <= 1.0; i += 2.0) {
	                vec3 p_a = p_snow - vec3(lean + (i * 0.16), 0.13 + mid_hop, 0.0);
	                
	                // ADJUSTED ANGLE: Changed 1.2 to 0.4 to point arms down
	                float angle = (0.4 * i) + sin(t * arm_speed) * (arm_range * i);
	                float sa = sin(angle), ca = cos(angle);
	                p_a.xy *= mat2(vec2(ca, sa), vec2(-sa, ca));
	                
	                // Straight Twig
	                float twig = length(p_a - vec3(clamp(p_a.x, min(0.0, i*0.25), max(0.0, i*0.25)), 0.0, 0.0)) - 0.008;
	                
	                // Hand ball follows the lower rotation
	                float hand = length(p_a - vec3(i * 0.25, 0.0, 0.0)) - 0.022;
	                d = min(d, min(twig, hand));
	            }

	            d *= 0.7; 
	            res_id = 1.0; 
	        }
	        break;
	        
	        case 30: { // Asteroid
	            float t = audio_time;
	            float growth = 1.0 + (music_intensity * 0.12); 
	            vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.5; 
	            vec3 p_ast = (p_raw - ch_p) / (0.55 * growth);
	            
	            p_ast.y += sin(t * 0.8) * 0.12;
	            p_ast.x += cos(t * 0.6) * 0.15;

	            float c = cos(t * 0.5), s = sin(t * 0.5);
	            p_ast.xy *= mat2(vec2(c, s), vec2(-s, c));
	            p_ast.yz *= mat2(vec2(c, s), vec2(-s, c));
	            
	            local_p = p_ast;
	            float noise = sin(p_ast.x * 10.0) * sin(p_ast.y * 10.0) * sin(p_ast.z * 10.0);
	            
	            d = (length(p_ast) - 0.30 + noise * 0.05) * (0.55 * growth);
	            res_id = (noise < -0.5) ? 2.0 : 1.0;
	        }
	        break;
			
			case 31: { // Octopus
	            float t = audio_time * 1.3;
	            
	            // --- SCALE & EFFECT SPACE ---
	            vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.6; 
	            float breath = sin(t * 1.8) * 0.02;
	            float pulse = 1.0 + (music_intensity * 0.03) + breath;
	            vec3 p_octo = (p_raw - ch_p - vec3(0.0, -0.2, 0.0)) / (0.7 * pulse);

	            // --- LAYERED GLOBAL SWAY ---
	            // Primary slow drift + Secondary micro-sway
	            float sway_x = sin(t * 0.6) * 0.12 + sin(t * 1.4) * 0.03;
	            float sway_y = cos(t * 0.5) * 0.08 + sin(t * 1.2) * 0.02;
	            p_octo.x += sway_x;
	            p_octo.y += sway_y;
	            
	            // Smoothed rotational breathing
	            float roll = sin(t * 0.4) * 0.08;
	            float yaw  = cos(t * 0.3) * 0.12;
	            
	            p_octo.xz *= mat2(vec2(cos(yaw), sin(yaw)), vec2(-sin(yaw), cos(yaw)));
	            p_octo.xy *= mat2(vec2(cos(roll), sin(roll)), vec2(-sin(roll), cos(roll)));
	            
	            local_p = p_octo;

	            // --- HEAD ---
	            vec3 head_p = p_octo - vec3(0.0, 0.7, 0.0); 
	            float head = smin(length(head_p / vec3(0.7, 0.5, 0.7)) - 0.35, 
	                              sdCappedCone(p_octo - vec3(0.0, 0.35, 0.0), 0.22, 0.15, 0.25), 0.25);
	            
	            float eyes = length(abs(head_p - vec3(0.12, -0.05, 0.22))) - 0.06;
	            float d_val = smin(head, eyes, 0.05);
	            res_id = 1.0; 

	            // --- TENTACLES (Active Independent Motion) ---
	            float tentacles = 1e5; 
	            float sat_length = clamp(music_intensity * 0.18, 0.0, 0.08); 
	            
	            for (float i = 0.0; i < 8.0; i++) {
	                float angle = i * (6.28318 / 8.0);
	                vec3 p_t = p_octo;
	                p_t.xz *= mat2(vec2(cos(angle), sin(angle)), vec2(-sin(angle), cos(angle)));
	                
	                float x_dist = p_t.x - 0.05; 
	                float seed = i * 1.337;
	                
	                // Randomized sine-sum for "irregular" squirming
	                float wave_y = (sin(x_dist * 4.0 - t * 2.0 + seed) + sin(x_dist * 2.0 + t + seed)) * 0.05;
	                float wave_z = (cos(x_dist * 5.0 + t * 1.5 + seed) + cos(x_dist * 3.0 - t * 0.8)) * 0.06;
	                
	                float vertical_lift = x_dist * 0.12 + wave_y; 
	                
	                vec3 p_loop = p_t;
	                p_loop.y -= vertical_lift; 
	                p_loop.z += wave_z;
	                
	                // Balanced reach
	                vec3 start = vec3(0.02, 0.05, 0.0);
	                vec3 end = vec3(0.7 + sat_length, 0.05, 0.0); 
	                
	                float tent = sdCapsule(p_loop, start, end, 0.08 * (1.1 - p_t.x));
	                tentacles = min(tentacles, tent);
	            }

	            d_val = smin(d_val, tentacles, 0.15); 
	            d = d_val * 0.7; 
	        }
	        break;
	        
			case 32: { // Egg
	            float t = audio_time * 2.0;
	            vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.5; 
	            float growth = 0.35 * (1.0 + music_intensity * 0.1);
	            vec3 p_egg = (p_raw - ch_p) / growth;
	            
	            // Floating drift
	            p_egg.y += sin(t * 1.1) * 0.2;
	            p_egg.x += cos(t * 0.9) * 0.2;

	            // --- RANDOMIZED TILT ---
	            // Use different frequencies to make the tilt feel non-linear
	            float tilt_x = sin(t * 0.5) * 0.4; 
	            float tilt_z = cos(t * 0.7) * 0.3;
	            
	            // Apply rotations for the tilt
	            p_egg.yz *= mat2(vec2(cos(tilt_x), sin(tilt_x)), vec2(-sin(tilt_x), cos(tilt_x)));
	            p_egg.xy *= mat2(vec2(cos(tilt_z), sin(tilt_z)), vec2(-sin(tilt_z), cos(tilt_z)));
	            
	            // Spin around the vertical axis
	            p_egg.xz *= mat2(vec2(cos(t * 3.0), sin(t * 3.0)), vec2(-sin(t * 3.0), cos(t * 3.0)));
	            
	            local_p = p_egg; 

	            // Shape Logic
	            float y_f = clamp(p_egg.y + 0.5, 0.0, 1.0);
	            vec3 e_s = max(vec3(1.1 - y_f * 0.4, 1.3, 1.1 - y_f * 0.4), 0.01);
	            d = (length(p_egg / e_s) - 0.4) * min(e_s.x, e_s.z) * growth; 
	            
	            // Pattern/Color logic
	            res_id = (length(fract(p_egg * 1.5 + 0.5) - 0.5) - 0.18 < 0.01) ? 3.0 : 1.0;
	        } 
	        break;
	        
	        case 33: { // Triangle
	            float t = audio_time * 1.5;
	            vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.5; 
	            vec3 p_tri = (p_raw - ch_p) / 0.5;
	            
	            p_tri.y += sin(t * 1.2) * 0.2; 
	            p_tri.x += cos(t * 0.8) * 0.15;  
	            p_tri.xz *= mat2(vec2(cos(t*2.0), sin(t*2.0)), vec2(-sin(t*2.0), cos(t*2.0)));
	            p_tri.yz *= mat2(vec2(cos(t*0.5), sin(t*0.5)), vec2(-sin(t*0.5), cos(t*0.5)));
	            local_p = p_tri;

	            float r = 0.35, h = 0.3;
	            float side1 = sdCapsule(p_tri, vec3(0, h, 0), vec3(r, -h, 0) + normalize(vec3(-2.0*r, 0, 0))*0.05, 0.06 + music_intensity*0.03);
	            float side2 = sdCapsule(p_tri, vec3(r, -h, 0), vec3(-r, -h, 0), 0.06 + music_intensity*0.03);
	            float side3 = sdCapsule(p_tri, vec3(-r, -h, 0), vec3(0, h, 0), 0.06 + music_intensity*0.03);
	            
	            d = min(side1, min(side2, side3)) * 0.5;
	            res_id = 1.0;
	        }
	        break;
	        
	        case 34: { // Tree
	            float t = audio_time * 2.0;
	            float sq = max(1.0 + cos(t) * 0.1, 0.01);
	            vec3 p_raw = (p - vec3(0.0, -0.25, 0.0)) * 0.55; 
	            vec3 p_tree = (p_raw - ch_p) / (0.6 * (1.0 + music_intensity * 0.1));
	            
	            p_tree.x += sin(t * 0.8) * 0.2; 
	            p_tree.y -= (abs(sin(t * 0.5)) * 0.25 - 0.4);
	            p_tree.y /= sq; p_tree.xz *= sq;
	            p_tree.x += sin(t + p_tree.y * 2.0) * 0.15 * (p_tree.y + 0.5);
	            local_p = p_tree; 

	            float trunk = sdCappedCone(p_tree - vec3(0.0, -0.1, 0.0), 0.2, 0.041, 0.04);
	            float foliage = 1e5;
	            for(float i = 0.0; i < 3.0; i++) {
	                foliage = min(foliage, sdCappedCone(p_tree - vec3(0, i*0.22 + 0.2, 0), 0.25, max(1.0-i*0.25, 0.01)*0.28, 0.001));
	            }
	            d = smin(trunk, foliage, 0.1) * 0.6 * min(1.0, sq);
	            res_id = (p_tree.y > 0.1) ? 1.0 : 2.0; 
	        }
	        break;
	        
			case 35: { // Cloud
	            float t = audio_time * 1.5; 
	            vec3 p_raw = (p - vec3(0.0, -0.15, 0.0)) * 0.5; 
	            
	            // --- SMOOTHED DYNAMICS ---
	            float breathe = sin(t * 1.5) * 0.04;
	            float intensity_sat = clamp(music_intensity, 0.0, 1.0);
	            float s = 0.4 * (1.0 + breathe + intensity_sat * 0.1) + 1e-5; 
	            
	            // Dampen the boost to prevent rapid position "snapping"
	            float m_boost = 1.0 + intensity_sat * 1.2; 
	            float cloud_tilt = sin(t * 0.5) * (0.15 * m_boost);
	            float drift_x = sin(t * 0.4) * (0.05 * m_boost);
	            vec3 cloud_off = vec3(drift_x, 0.15 + cos(t * 0.25) * 0.04 + sin(t * 0.8) * 0.06, 0.0);
	            
	            mat2 rot = mat2(vec2(cos(cloud_tilt), sin(cloud_tilt)), vec2(-sin(cloud_tilt), cos(cloud_tilt)));
	            
	            // --- 1. CLOUD GEOMETRY ---
	            vec3 p_cloud = (p_raw - (ch_p + cloud_off)) / s;
	            p_cloud.xy *= rot;
	            local_p = p_cloud; 

	            // ANTI-JITTER: Use a lower frequency for the displacement noise
	            float noise = sin(p_cloud.x * 4.0 + t) * cos(p_cloud.z * 4.0 + t * 0.5);
	            float noise_pulse = 0.025 + (intensity_sat * 0.03); // Tighter range to stop "shaking"
	            
	            float d_cloud = smin(smin((length((p_cloud - vec3(0, 0.15, 0)) / vec3(1.2, 1, 0.9)) - 0.35)*0.8, 
	                                     length(p_cloud - vec3(-0.4, -0.05, 0)) - 0.25, 0.22), 
	                                     length(p_cloud - vec3(0.4, 0.05, 0)) - 0.28, 0.22);
	            
	            // Apply noise as a gentle surface offset
	            d = (d_cloud - noise * noise_pulse) * s;
	            res_id = 1.0;

	            // --- 2. DETACHED RAIN (Same Logic) ---
	            vec3 p_rain = p_raw - (ch_p + cloud_off);
	            p_rain.xy *= rot; 

	            float rain_cycle = fract(t * 0.1); 
	            if (rain_cycle < 0.2) {
	                float fall_speed = 0.5;
	                float spacing = 0.11;
	                vec2 g_id = floor(p_rain.xz / spacing + 0.5);
	                
	                if (abs(g_id.x) <= 1.0 && abs(g_id.y) <= 1.0) {
	                    float stagger = sin(g_id.x * 45.1 + g_id.y * 92.7) * 4.0;
	                    float py = p_rain.y + (t * fall_speed) + stagger;
	                    float v_phase = fract(py * 1.8); 
	                    float py_mod = (v_phase - 0.5) / 1.8;
	                    
	                    float fade_mask = smoothstep(0.0, 0.1, -p_rain.y) * smoothstep(-0.6, -0.4, p_rain.y);
	                    
	                    if (fade_mask > 0.01) {
	                        float pop_scale = smoothstep(0.0, 0.7, v_phase) * (1.0 - smoothstep(0.7, 1.0, v_phase)) * fade_mask;
	                        vec2 g_pos = (p_rain.xz - g_id * spacing);
	                        vec3 p_pill = vec3(g_pos.x, py_mod, g_pos.y);
	                        float pill = sdCapsule(p_pill, vec3(0, -0.012 * pop_scale, 0), 
	                                                       vec3(0, 0.012 * pop_scale, 0), 0.005 * pop_scale);
	                        if (pill < d) {
	                            d = pill;
	                            res_id = 3.0; 
	                        }
	                    }
	                }
	            }
	        }
	        break;
			
			case 36: { // Donut
	            float t = audio_time * 2.0;
	            vec3 p_raw = (p - vec3(0.0, 0.0, 0.0)) * 0.5; 
	            vec3 p_don = (p_raw - ch_p - vec3(sin(t * 0.8) * 0.15, sin(t * 1.2) * 0.08, 0.0)) / 0.32;
	            
	            // Dynamic Tumble
	            float s_ = t * 2.8, tx = sin(t * 1.1) * 0.95, tz = cos(t * 1.1) * 0.95;
	            p_don.xz *= mat2(vec2(cos(s_), sin(s_)), vec2(-sin(s_), cos(s_)));
	            p_don.yz *= mat2(vec2(cos(tx), sin(tx)), vec2(-sin(tx), cos(tx)));
	            p_don.xy *= mat2(vec2(cos(tz), sin(tz)), vec2(-sin(tz), cos(tz)));
	            local_p = p_don; 

	            vec3 p_s = p_don; p_s.x /= 1.35;
	            float donut = (length(vec2(length(p_s.xz + 0.0001) - 0.32, p_s.y)) - (0.09 + music_intensity * 0.04));
	            d = donut * 0.32 * 0.8; 
	            res_id = (sin(p_don.x * 25.0) * sin(p_don.z * 25.0) > 0.8) ? 3.0 : 1.0;
	        }
	        break;
	        
	        case 37: { // Branch
	            float t = audio_time * 2.0;
	            vec3 p_raw = (p - vec3(0.0, -0.15, 0.0)) * 0.6; 
	            vec3 p_stk = (p_raw - (ch_p + vec3(sin(t * 0.4) * 0.12, sin(t * 0.6) * 0.05, 0.0))) / 0.65;
	            
	            float tilt = cos(t * 0.6) * 0.08;
	            p_stk.yz *= mat2(vec2(cos(tilt), sin(tilt)), vec2(-sin(tilt), cos(tilt)));
	            float twirl = (t * 0.8) + pow(fract(t * 0.1), 8.0) * 6.28318;
	            p_stk.xz *= mat2(vec2(cos(twirl), sin(twirl)), vec2(-sin(twirl), cos(twirl)));

	            p_stk.x -= sin(p_stk.y * 2.0) * 0.08;
	            p_stk.z += cos(p_stk.y * 1.5) * 0.04;
	            local_p = p_stk; 

	            float trunk = sdCapsule(p_stk, vec3(0.0, -0.65, 0.0), vec3(0.0, 0.5, 0.0), 0.045);
	            float k_top = length(p_stk - vec3(-0.05, 0.3, 0.02)) - 0.055;
	            float k_low = length(p_stk - vec3(-0.04, -0.35, -0.05)) - 0.06;
	            vec3 p_br = p_stk - vec3(0.06, 0.0, -0.03);
	            p_br.xy *= mat2(vec2(cos(-1.0), sin(-1.0)), vec2(-sin(-1.0), cos(-1.0)));
	            
	            d = smin(smin(smin(trunk, k_top, 0.05), sdCapsule(p_br, vec3(0), vec3(0, 0.12, 0), 0.03), 0.05), k_low, 0.07) * 0.65;
	            res_id = (k_top < 0.01 || k_low < 0.01 || p_stk.y < -0.6) ? 3.0 : 1.0;
	        }
	        break;
	        
	        case 38: { // Rook
	            float t = audio_time * 1.5;
	            
	            // --- INCREASED SATURATED GROWTH ---
	            float breathing = 0.02 * sin(audio_time * 1.8);
	            float intensity_sat = clamp(music_intensity, 0.0, 1.0);
	            float growth = (0.55 + breathing) * (1.0 + intensity_sat * 0.15);
	            
	            vec3 p_raw = (p - vec3(0.0, -0.15, 0.0)) * 0.72; 
	            vec3 sway = vec3(sin(t * 0.8) * 0.12, cos(t * 0.5) * 0.08, 0.0);
	            vec3 p_rk = (p_raw - ch_p - sway) / growth;
	            p_rk.xz *= mat2(vec2(cos(t * 0.4), sin(t * 0.4)), vec2(-sin(t * 0.4), cos(t * 0.4)));
	            local_p = p_rk; 

	            float r_s = length(p_rk.xz);
	            
	            // --- SIMPLIFIED SMOOTH BASE ---
	            // Replaced the complex double-ring with a single rounded base transition
	            float base = max(r_s - 0.38, abs(p_rk.y + 0.5) - 0.1);
	            
	            // --- SHAFT ---
	            float shaft = max(r_s - mix(0.18, 0.12, clamp((p_rk.y + 0.2)/0.5, 0.0, 1.0)), abs(p_rk.y + 0.05) - 0.35);
	            
	            // Use a larger smoothing factor here to melt the shaft into the base
	            float body = smin(base, shaft, 0.15);
	            
	            // --- THE DECK ---
	            float deck = max(r_s - 0.28, abs(p_rk.y - 0.4) - 0.02);
	            
	            // --- FLARED BATTLEMENTS ---
	            vec3 tp = p_rk - vec3(0.0, 0.52, 0.0); 
	            vec3 box_p = abs(tp);
	            box_p.xz -= 0.18; 
	            
	            float taper = clamp(tp.y * 0.5, 0.0, 1.0);
	            float tooth_w = 0.08 + taper * 0.04; 
	            float teeth = length(max(abs(box_p) - vec3(tooth_w, 0.1, tooth_w), 0.0)) - 0.01;

	            // Composite
	            float rook_sdf = min(body, min(deck, teeth));
	            d = rook_sdf * growth * 0.8; 
	            
	            res_id = (teeth < 0.02) ? 3.0 : 1.0;
	        }
	        break;
	        
	        case 39: { // Flask
	            float t = audio_time * 2.0;
	            vec3 p_raw = (p - vec3(0.0, -0.2, 0.0)) * 0.55; 
	            vec3 p_bt = (p_raw - ch_p - vec3(sin(t * 0.7) * 0.05, sin(t * 1.0) * 0.05, 0.0)) / 0.65;
	            p_bt.xz *= mat2(vec2(cos(t*1.5), sin(t*1.5)), vec2(-sin(t*1.5), cos(t*1.5)));
	            p_bt.zy *= mat2(vec2(cos(0.43), sin(0.43)), vec2(-sin(0.43), cos(0.43)));
	            local_p = p_bt; 

	            float r_r = length(p_bt.xz + 0.0001);
	            float neck = length(max(abs(vec2(r_r, p_bt.y - 0.25)) - vec2(0.06, 0.18), 0.0));
	            float cork = length(max(abs(vec2(r_r, p_bt.y - 0.45)) - vec2(0.05, 0.06), 0.0));
	            d = smin(smin(length(p_bt + vec3(0, 0.12, 0)) - 0.28, neck, 0.08), length(vec2(r_r - 0.08, p_bt.y - 0.4)) - 0.03, 0.02);
	            d = min(d, cork) * 0.65;
	            res_id = (p_bt.y < -0.02 || cork < 0.005) ? 3.0 : 1.0;
	        }
	        break;
	        
	        case 40: { // Mushroom
	            float t = audio_time * 1.5;
	            
	            // --- RELAXED DYNAMICS ---
	            float hop = sin(t * 1.2) * 0.07; 
	            float breathe = sin(t * 1.2) * 0.03; 
	            float intensity_sat = clamp(music_intensity, 0.0, 1.0);
	            float s = 0.5 + breathe + (intensity_sat * 0.04);
	            
	            // --- POSITIONING ---
	            vec3 p_raw = (p - vec3(0.0, -0.3, 0.0)) * 0.55; 
	            vec3 p_m = (p_raw - ch_p - vec3(sin(t * 0.8) * 0.08, 0.05 + hop, 0.0)) / s;
	            
	            // --- 3D SWAY ---
	            float roll = sin(t * 0.7) * 0.15;
	            float pitch = cos(t * 1.1) * 0.1;
	            p_m.xy *= mat2(vec2(cos(roll), sin(roll)), vec2(-sin(roll), cos(roll)));
	            p_m.zy *= mat2(vec2(cos(pitch), sin(pitch)), vec2(-sin(pitch), cos(pitch)));
	            
	            p_m.yz *= mat2(vec2(cos(0.1), sin(0.1)), vec2(-sin(0.1), cos(0.1)));
	            p_m.xz *= mat2(vec2(cos(t*1.2), sin(t*1.2)), vec2(-sin(t*1.2), cos(t*1.2)));
	            local_p = p_m; 

	            // --- REFINED LONG & THIN STALK ---
	            float stem_top = 0.2;
	            float stem_bot = -0.45;
	            float h = clamp(p_m.y, stem_bot, stem_top);
	            float flare = 0.06 * pow(smoothstep(stem_top, stem_bot, p_m.y), 4.0);
	            float stem_radius = 0.04 + flare;
	            float stem = length(p_m - vec3(0.0, h, 0.0)) - stem_radius;
	            
	            // --- SLIGHTLY SHRUNG CAP & LOWER LIP ---
	            vec3 p_c = p_m - vec3(0.0, 0.18, 0.0);
	            // Reduced radius slightly from 0.4 to 0.38
	            float cap = max(length(vec3(p_c.x, p_c.y * 1.5, p_c.z)) - 0.38 - sin(p_m.x * 10.0) * cos(p_m.z * 10.0) * 0.03, -p_c.y - 0.08);
	            
	            // --- RESTORED SPOT OFFSETS ---
	            // Reverted spot offsets from 0.2 back to 0.18
	            float spot_size = 0.07 + breathe * 0.1;
	            float spot = length(vec3(abs(p_c.x) - 0.18, p_c.y - 0.15, abs(p_c.z) - 0.18)) - spot_size;

	            d = smin(stem, cap, 0.15) * s;
	            res_id = (spot < 0.0 && cap < 0.02) ? 2.0 : 1.0;

	            // --- SPORES ---
	            float burst_gate = smoothstep(0.7, 0.98, sin(t * 0.4)); 
	            if (burst_gate > 0.01) {
	                float smoke_t = t * 1.0;
	                for(int i=0; i<8; i++) {
	                    float fi = float(i);
	                    float phase = fract(smoke_t * 0.7 + fi * 0.12);
	                    float wind = sin(smoke_t * 1.5 + fi) * 0.15 * phase;
	                    float rnd_angle = sin(fi * 437.1) * 6.28 + (smoke_t * 0.3);
	                    float radius = 0.1 * (1.0 + phase * 3.5);
	                    vec3 spore_pos = vec3(cos(rnd_angle) + wind, 0.25 + phase * 1.8, sin(rnd_angle) + wind) * radius;
	                    vec3 p_spore = p_m - spore_pos;
	                    float ragged = sin(p_spore.x * 50.0) * cos(p_spore.y * 50.0) * 0.008;
	                    float spore_size = 0.05 * (1.0 - phase) * smoothstep(0.0, 0.2, phase) * burst_gate;
	                    float spore_d = length(p_spore / vec3(1.2, 0.7, 1.2)) - spore_size + ragged;
	                    if (spore_d < d) { d = spore_d; res_id = 3.0; }
	                }
	            }
	        } 
	        break;

	        case 41: { // Heart
	            float t = audio_time * 1.5;
	            
	            // --- CORE HEART THUMP ---
	            float thump_cycle = t * 4.0;
	            float lub = pow(max(0.0, sin(thump_cycle)), 10.0) * 0.12;
	            float dub = pow(max(0.0, sin(thump_cycle - 0.6)), 8.0) * 0.05;
	            float thump = lub + dub;
	            float base_s = 0.75 + thump + music_intensity * 0.05; 

	            // Shared positioning
	            vec3 p_shared = p_mod - ch_p - vec3(sin(t * 0.7) * 0.15, cos(t * 0.8) * 0.1, 0.0);
	            float sa = sin(t * 0.8), ca = cos(t * 0.8), rock = sin(t * 1.2) * 0.2;
	            mat2 rot_xz = mat2(vec2(ca, sa), vec2(-sa, ca));
	            
	            // --- MAIN HEART ---
	            vec3 p_h = p_shared / base_s;
	            p_h.xz *= rot_xz;
	            p_h.xy *= mat2(vec2(cos(rock), sin(rock)), vec2(-sin(rock), cos(rock)));
	            p_h.y += 0.1;
	            local_p = p_h;

	            float x = abs(p_h.x), y = p_h.y, z = p_h.z;
	            float lobes = length(vec3(x - 0.22, y - 0.1, z)) - (0.35 + abs(sa) * 0.05);
	            vec2 a = vec2(0.22, 0.1), b = vec2(0.0, -0.6);
	            float h = clamp(dot(vec2(x,y)-a, b-a)/dot(b-a, b-a), 0.0, 1.0);
	            float body = length(vec3(vec2(x,y)-(a+(b-a)*h), z)) - (mix(0.35, 0.02, h) + abs(sa) * 0.05);
	            float d_main = smax(smin(lobes, body, 0.25), abs(z) - 0.15, 0.08) * base_s;
	            
	            // --- INFREQUENT SLENDER PULSE ---
	            // Only trigger pulse every 3rd cycle to reduce frequency
	            float pulse_gate = step(0.66, fract(thump_cycle / (3.14159 * 3.0)));
	            float facing = smoothstep(0.6, 1.0, abs(ca));
	            
	            float pulse_phase = fract(thump_cycle / 6.283);
	            // Growth is strictly tied to the expansion phase
	            float grow = 1.0 + pulse_phase * (1.2 + music_intensity); 
	            
	            vec2 p_2d = p_shared.xy;
	            p_2d *= mat2(vec2(cos(rock), sin(rock)), vec2(-sin(rock), cos(rock)));
	            p_2d /= grow;
	            p_2d.y += 0.1 / grow;

	            float ox = abs(p_2d.x), oy = p_2d.y;
	            float o_lobes = length(vec2(ox - 0.22, oy - 0.1)) - 0.35;
	            float oh = clamp(dot(vec2(ox,oy)-a, b-a)/dot(b-a, b-a), 0.0, 1.0);
	            float o_body = length(vec2(ox,oy)-(a+(b-a)*oh)) - mix(0.35, 0.02, oh);
	            float d_2d = smin(o_lobes, o_body, 0.25);
	            
	            // Reduced thickness: now much more slender
	            float pulse_thick = (0.015 + music_intensity * 0.02) * (1.0 - pulse_phase);
	            float d_pulse_ring = (abs(d_2d) - pulse_thick) * grow;
	            
	            float final_pulse = max(d_pulse_ring, abs(p_shared.z) - 0.005);
	            
	            // Smoothly blend the opacity to prevent snapping at the start/end of the cycle
	            float opacity = smoothstep(0.0, 0.1, pulse_phase) * (1.0 - pulse_phase) * facing * pulse_gate;
	            d = smin(d_main, final_pulse + (1.0 - opacity), 0.05);

	            res_id = 2.0;
	        } 
	        break;

			case 42: { // Bone
	            float t = audio_time * 1.2;
	            
	            // --- DYNAMIC MOVEMENT ---
	            // Horizontal sway (x) and Vertical bob (y)
	            float sway_x = sin(t * 0.9) * 0.15;
	            float bob_y = cos(t * 1.3) * 0.1;
	            
	            float s = (0.75 * (1.0 + sin(t * 0.6) * 0.02 + music_intensity * 0.03));
	            
	            // Apply sway and bob to the translation
	            vec3 p_b = (p_mod - ch_p - vec3(sway_x, bob_y, 0.0)) / s;
	            
	            // --- ROTATION ---
	            float c = cos(t*0.8), st = sin(t*0.8);
	            p_b.xz *= mat2(vec2(c, st), vec2(-st, c));
	            float rs = sin(t*1.5)*0.1;
	            p_b.xy *= mat2(vec2(cos(rs), sin(rs)), vec2(-sin(rs), cos(rs)));
	            local_p = p_b;

	            // --- GEOMETRY ---
	            float shaft = max(length(p_b.yz) - (0.12 + music_intensity * 0.02), abs(p_b.x) - 0.45);
	            vec3 q = abs(p_b) - vec3(0.45, 0.12, 0.0);
	            d = smin(shaft, length(q) - (0.22 + music_intensity * 0.02), 0.1) * s;
	            res_id = 1.0;
	        } 
	        break;

	        case 43: { // Double Helix
	            float t = audio_time * 1.5;
	            float s = (0.85 + sin(t * 2.0) * 0.04 + music_intensity * 0.06) * 1.25;
	            vec3 p_d = (p_mod - ch_p - vec3(sin(t * 0.6) * 0.12, cos(t * 0.7) * 0.08, 0.0)) / s;
	            float c = cos(t*0.8), st = sin(t*0.8);
	            p_d.xz *= mat2(vec2(c, st), vec2(-st, c));
	            float rs = sin(t*1.1)*0.15;
	            p_d.xy *= mat2(vec2(cos(rs), sin(rs)), vec2(-sin(rs), cos(rs)));
	            local_p = p_d;

	            vec3 p_t = p_d; 
	            float twist = p_d.y * 6.0;
	            p_t.xz *= mat2(vec2(cos(twist), sin(twist)), vec2(-sin(twist), cos(twist)));
	            float h1 = length(p_t.xz - vec2(0.25, 0.0)) - 0.08, h2 = length(p_t.xz + vec2(0.25, 0.0)) - 0.08;
	            float bars = max(abs(p_t.x) - 0.25, max(abs(p_t.z) - 0.04, abs(fract(p_d.y * 5.0 + 0.5) - 0.5) / 5.0 - 0.02));
	            d = max(smin(min(h1, h2), bars, 0.03), abs(p_d.y) - 0.5) * s;
	            res_id = (min(h1, h2) < bars) ? 2.0 : 1.0;
	        } 
			break;

	        case 44: { // Solar System
	            float t = audio_time * 1.0;
	            
	            // --- NON-STICKING SATURATION ---
	            // atan(x) provides a curve that slows down but never truly stops increasing
	            float growth_factor = atan(music_intensity * 1.5) * 0.2;
	            float s = (1.0 + growth_factor) * 1.1;
	            
	            // --- DYNAMIC SWAY ---
	            float sway_x = sin(t * 0.8) * (0.12 + growth_factor * 0.1);
	            float bob_y = cos(t * 0.7) * (0.12 + growth_factor * 0.1);
	            
	            vec3 p_s = (p_mod - ch_p - vec3(sway_x, bob_y, 0.0)) / s;
	            local_p = p_s;

	            // --- GEOMETRY ---
	            float sun_pulse = (0.3 + growth_factor * 0.15);
	            float sun = length(p_s) - sun_pulse;
	            
	            // Planet 1
	            float p1 = length(p_s - vec3(cos(t*2.5)*0.55, sin(t*4.0)*0.15, sin(t*2.5)*0.55)) - 0.08;
	            
	            // Planet 2
	            float p2 = length(p_s - vec3(cos(t*1.2+2.0)*0.8, cos(t*2.0)*0.1, sin(t*1.2+2.0)*0.8)) - 0.1;
	            
	            // Saturn Ring
	            vec3 p_r = p_s; 
	            float rc = cos(t*0.5), rs = sin(t*0.5);
	            p_r.xz *= mat2(vec2(rc, rs), vec2(-rs, rc));
	            p_r.yz *= mat2(vec2(cos(0.3), sin(0.3)), vec2(-sin(0.3), cos(0.3)));
	            float ring = max(abs(length(p_r.xz) - 0.7), abs(p_r.y) - 0.01) - 0.015;
	            
	            d = min(sun, min(p1, min(p2, ring))) * s;
	            res_id = (sun < min(p1, p2)) ? 1.0 : 2.0;
	        } 
	        break;

	        case 45: { // Gears
	            float t = audio_time * 1.2;
	            float s = 0.9 + (music_intensity * 0.01);
	            vec3 p_g = (p_mod - ch_p - vec3(sin(t * 0.7) * 0.15, sin(t * 1.5) * 0.1, 0.0)) / s;
	            float gc = cos(t*0.4), gs = sin(t*0.4);
	            p_g.xz *= mat2(vec2(gc, gs), vec2(-gs, gc));
	            local_p = p_g;

	            vec3 q_c = p_g; 
	            float rot = t*2.0;
	            q_c.xy *= mat2(vec2(cos(rot), sin(rot)), vec2(-sin(rot), cos(rot)));
	            d = max(max(length(q_c.xy) - 0.35 - (cos(atan(q_c.x, q_c.y) * 10.0) * 0.06), abs(q_c.z) - 0.08), -(length(q_c.xy) - 0.12));
	            for(int i = 0; i < 2; i++) {
	                float ang = (t * 0.8) + (float(i) * 3.14159);
	                vec3 q = p_g - vec3(cos(ang) * 0.65, sin(ang) * 0.65, 0.0);
	                float gr = -t*4.0;
	                q.xy *= mat2(vec2(cos(gr), sin(gr)), vec2(-sin(gr), cos(gr)));
	                d = min(d, max(max(length(q.xy) - 0.22 - (cos(atan(q.x, q.y) * 8.0) * 0.05), abs(q.z) - 0.06), -(length(q.xy) - 0.06)));
	            }
	            d *= s; res_id = 1.0;
	        } 
			break;

	        case 46: { // Brain
	            float t = audio_time * 1.5, elastic = atan(music_intensity * 2.0) * 0.5;
	            
	            // --- UNIFIED THROB ---
	            float throb = (pow(max(0.0, sin(t * 3.5)), 4.0) * 0.1) + (music_intensity * 0.05);
	            float s = (0.85 * (1.0 + (throb * 0.4) + (elastic * 0.15)));
	            
	            vec3 p_b = (p_mod - ch_p - vec3(sin(t * 0.6) * 0.12, cos(t * 0.8) * 0.08, 0.0)) / s;
	            
	            // --- REFINED ROTATION ---
	            // Y-Wander: Combines two speeds for a non-linear rotation
	            float ang_y = t * 0.5 + sin(t * 0.8) * 0.5;
	            // Occasional Tilt: Subtle shifts on X and Z
	            float tilt_x = sin(t * 0.4) * 0.15;
	            float tilt_z = cos(t * 0.3) * 0.1;

	            p_b.xz *= mat2(vec2(cos(ang_y), sin(ang_y)), vec2(-sin(ang_y), cos(ang_y)));
	            p_b.yz *= mat2(vec2(cos(tilt_x), sin(tilt_x)), vec2(-sin(tilt_x), cos(tilt_x)));
	            p_b.xy *= mat2(vec2(cos(tilt_z), sin(tilt_z)), vec2(-sin(tilt_z), cos(tilt_z)));
	            local_p = p_b; 

	            // --- BRAIN TEXTURE ---
	            float bumps = sin(p_b.x * 12.0 + sin(p_b.z * 10.0)) * sin(p_b.y * 12.0 + sin(p_b.x * 10.0)) * sin(p_b.z * 12.0) * 0.04;
	            float fissure = smoothstep(0.08, 0.0, abs(p_b.x)) * 0.06;
	            
	            float lobe_r = 0.35 + (smoothstep(-0.3, 0.2, p_b.y) * 0.1);
	            float cerebrum = length(vec3(abs(p_b.x) - 0.18, p_b.y * 1.1, p_b.z * 0.8)) - lobe_r + fissure + bumps;

	            // --- THROBBING TENTACLES ---
	            float sway_y = p_b.y + 0.35;
	            
	            // Primary Cord
	            float sway1 = sin(t * 2.5 + sway_y * 4.0) * 0.08 * smoothstep(0.0, -0.6, p_b.y);
	            vec3 st_p1 = p_b - vec3(sway1, -0.2, 0.0);
	            float st_radius1 = (0.12 + throb * 0.1) * exp(1.8 * p_b.y); 
	            float d_stem1 = max(length(st_p1.xz) - st_radius1, p_b.y - 0.05);
	            d_stem1 = max(d_stem1, -p_b.y - 0.8);

	            // Secondary Cord
	            float sway2 = cos(t * 3.0 + sway_y * 5.0) * 0.05 * smoothstep(0.0, -0.4, p_b.y);
	            vec3 st_p2 = p_b - vec3(sway2 + 0.15, -0.15, 0.1);
	            float st_radius2 = (0.05 + throb * 0.05) * exp(2.0 * p_b.y);
	            float d_stem2 = max(length(st_p2.xz) - st_radius2, p_b.y - 0.05);
	            d_stem2 = max(d_stem2, -p_b.y - 0.5);

	            // Pons
	            float pons = length(p_b - vec3(0.0, -0.25, 0.05)) - 0.15 + bumps;

	            d = smin(smin(smin(cerebrum, pons, 0.1), d_stem1, 0.1), d_stem2, 0.1) * s; 
	            res_id = 2.0;
	        } 
	        break;
			
			case 47: { // Hand
	            float t = audio_time * 0.85; 
	            
	            // 1. SATURATED AUDIO & GROWTH
	            float sat_m = tanh(music_intensity * 2.0); 
	            // Integrated beat-sync scale growth (m_grow + base pulse)
	            float s_beat = 1.0 + (sat_m * 0.15); 
	            
	            // 2. SEQUENCER
	            float cycle_dur = 12.0; 
	            float cycle_id = floor(t / cycle_dur);
	            float cycle_fract = fract(t / cycle_dur);
	            float roll = hash(cycle_id); 
	            
	            int target_state = (roll > 0.6) ? (roll > 0.9 ? 2 : 1) : 0; 
	            float base_trig = smoothstep(0.1, 0.35, cycle_fract) * (1.0 - smoothstep(0.65, 0.9, cycle_fract));
	            if (roll <= 0.6) base_trig = 0.0;
	            float trigger = clamp(base_trig + sin(base_trig * 3.14159 * 1.5) * 0.04 * base_trig, 0.0, 1.0);
	            float is_idle = 1.0 - trigger;

	            // 3. ENHANCED SWAY & RIG
	            vec3 fluid_sway = vec3(sin(t * 0.4) * 0.2, cos(t * 0.5) * 0.15, sin(t * 0.3) * 0.08);
	            vec3 p_h = p_mod - ch_p - vec3(0.0, -0.05, -0.1) - fluid_sway;
	            
	            float z_tilt = (sin(t * 0.25) * 0.12) + (fluid_sway.x * 0.1);
	            float c_z = cos(z_tilt), s_z = sin(z_tilt);
	            p_h.xy *= mat2(vec2(c_z, s_z), vec2(-s_z, c_z));
	            
	            float tr_rot = trigger * 3.14159;
	            float c_tr = cos(tr_rot), s_tr = sin(tr_rot);
	            p_h.xy *= mat2(vec2(c_tr, s_tr), vec2(-s_tr, c_tr));
	            
	            float face_f = (trigger > 0.5) ? -1.0 : 1.0;
	            p_h.xz *= mat2(vec2(face_f, 0.0), vec2(0.0, face_f));
	            p_h.yz *= mat2(vec2(cos(0.35), sin(0.35)), vec2(-sin(0.35), cos(0.35)));
	            local_p = p_h;

	            // 4. GROWTH & THICKNESS MAPPING
	            // Beat scaling applied here
	            float s = 1.45 * s_beat * (1.0 + trigger * 0.12); 
	            float breathing = sin(t * 0.4) * 0.5 + 0.5;
	            
	            bool is_slender_gesture = (target_state == 1 || target_state == 2);
	            float gesture_thick = is_slender_gesture ? (trigger * 0.01) : (trigger * 0.035);
	            float thick_base = (sat_m * 0.02) + (0.008 * breathing) + gesture_thick;

	            // 5. ANATOMY
	            float p1 = sdCapsule(p_h - vec3(-0.08, -0.05, 0.05), vec3(0), vec3(0, 0, 0.15), 0.18 + thick_base);
	            float p2 = sdCapsule(p_h - vec3(0.08, -0.05, 0.05), vec3(0), vec3(0, 0, 0.15), 0.18 + thick_base);
	            float palm_base = smin(p1, p2, 0.12);
	            float knuckles = sdCapsule(p_h - vec3(-0.16, 0.04, 0.2), vec3(0), vec3(0.32, 0, 0), 0.09 + thick_base);
	            float thumb_m = length(p_h - vec3(-0.25, -0.1, 0.05)) - (0.1 + thick_base);
	            float palm = smin(smin(palm_base, knuckles, 0.1), thumb_m, 0.1);
	            float wrist_geo = sdCapsule(p_h - vec3(0, 0, -0.2), vec3(0, -0.05, 0), vec3(0, -0.02, -0.3), 0.17 + thick_base);

	            // 6. FINGERS
	            float fingers = 1e10;
	            for (int i = 0; i < 4; i++) {
	                float fi = float(i);
	                float phase_shift = (i == 0) ? 1.2 : (i == 1) ? 0.0 : (i == 2) ? 2.5 : 0.8;
	                float walk_t = (t * 3.5) + phase_shift; 
	                float lift = (0.5 - 0.5 * cos(walk_t)) * 0.15 * is_idle;
	                float f_trigger = clamp(trigger * 1.1 - (fi * 0.04), 0.0, 1.0);
	                
	                bool is_horn = (i == 0 || i == 3), is_middle = (i == 1);
	                float x_off = (fi - 1.5) * 0.17; 
	                if (target_state == 1) x_off = mix(x_off, is_horn ? (i==0?-0.32:0.24) : (fi-1.5)*0.04, f_trigger);
	                if (target_state == 2) x_off = mix(x_off, is_middle ? 0.0 : (fi-1.5)*0.05, f_trigger);
	                
	                vec3 f_p = p_h - vec3(x_off, -0.2 + lift, 0.25);
	                float bend = mix(mix(0.1, 1.5, 0.5 + 0.5 * sin(walk_t)) * is_idle, (target_state == 1 ? (is_horn ? 0.15 : 3.3) : (is_middle ? 0.15 : 3.5)), f_trigger);
	                float final_l = mix((i==0?0.45:(i==1?0.6:(i==2?0.55:0.4))), (target_state==1&&is_horn?0.78:(target_state==2&&is_middle?0.88:0.45)), f_trigger);
	                
	                bool slender_target = (target_state == 1 && is_horn) || (target_state == 2 && is_middle);
	                float finger_thick = slender_target ? (thick_base * 0.7) : thick_base;

	                float c_b1 = cos(bend*0.5), s_b1 = sin(bend*0.5);
	                f_p.yz *= mat2(vec2(c_b1, s_b1), vec2(-s_b1, c_b1));
	                vec3 j1 = vec3(0, -final_l * 0.5, 0.02);
	                vec3 f_p2 = f_p - j1;
	                f_p2.yz *= mat2(vec2(c_b1, s_b1), vec2(-s_b1, c_b1));
	                fingers = min(fingers, min(sdCapsule(f_p, vec3(0), j1, 0.032 + finger_thick), sdCapsule(f_p2, vec3(0), vec3(0, -final_l * 0.5, 0.04), 0.028 + finger_thick)));
	            }
	            
	            // 7. THUMB
	            float t_trig = smoothstep(0.1, 0.9, trigger);
	            float thumb_tap = max(0.0, sin(t * 3.5)) * 0.08 * is_idle; 
	            vec3 p_t = p_h - vec3(mix(-0.35 + sin(t*2.8)*0.05*is_idle, -0.1, t_trig), -0.05 + thumb_tap, mix(0.1, 0.32, t_trig));
	            float t_bend = mix(1.1, 2.3, t_trig);
	            float c_tb = cos(t_bend), s_tb = sin(t_bend);
	            p_t.yz *= mat2(vec2(c_tb, s_tb), vec2(-s_tb, c_tb));
	            float t_xt = t_trig * -0.5;
	            p_t.xz *= mat2(vec2(cos(t_xt), sin(t_xt)), vec2(-sin(t_xt), cos(t_xt)));
	            float thumb_geo = sdCapsule(p_t, vec3(0), vec3(-0.1, 0.16, 0.05), 0.04 + thick_base);

	            // 8. GHOST PULSE
	            float pulse_phase = fract(t * 1.2); 
	            float ghost_grow = 1.0 + clamp(pulse_phase * (0.25 + sat_m), 0.0, 0.45) * trigger;
	            float ghost_opacity = (1.0 - pulse_phase) * trigger;

	            float d_main = smin(smin(palm, wrist_geo, 0.15), min(thumb_geo, fingers), 0.14) * s;
	            float d_ghost = d_main / ghost_grow; 
	            d = smin(d_main, d_ghost + (1.0 - ghost_opacity), 0.05);
	            res_id = 2.0;
	        }
	        break;
			
			case 48: { // Dugtrio
	            float t = audio_time * 1.5;
	            float sat_m = tanh(music_intensity * 1.4);
	            float s = 0.95 * (1.0 + (sat_m * 0.12)); 
	            
	            float sway_x = sin(t * 0.4) * 0.12;
	            float sway_y = sin(t * 0.6) * 0.08; 
	            float sway_z = cos(t * 0.5) * 0.08;
	            float z_rot = sin(t * 0.3) * 0.05; 
	            
	            vec3 p_d = (p_mod - ch_p - vec3(sway_x, -0.35 + sway_y, sway_z)) / s;
	            float c_z = cos(z_rot); float s_z = sin(z_rot);
	            p_d.xy *= mat2(vec2(c_z, s_z), vec2(-s_z, c_z));
	            local_p = p_d;

	            float jagged = abs(sin(p_d.x * 14.0) * cos(p_d.z * 14.0)) * 0.04;
	            float morph = sin(p_d.x * 2.0 + t) * 0.03;
	            float mound = length(p_d / vec3(1.1, 0.45, 1.1)) - (0.52 + jagged + morph);
	            float ground_clip = p_d.y + 0.12; 
	            
	            float heads = 1e10;
	            float dirt = 1e10;

	            for(int i = 0; i < 3; i++) {
	                float fi = float(i);
	                float pop_id = floor((t + (fi * 1.6)) / 6.28); 
	                float pop_seed = hash(fi * 17.7 + pop_id);
	                vec2 offset = vec2(sin(fi * 2.1) * 0.24, cos(fi * 2.1) * 0.18);
	                
	                float pop_wave = sin(t + (fi * 1.6));
	                float pop_smooth = smoothstep(-0.7, 0.7, pop_wave); 
	                float height_limit = 0.08 + (pop_seed * 0.12);
	                float height = mix(-0.55, height_limit, pop_smooth);
	                
	                float h_rad = 0.12 + (pop_seed * 0.08); 
	                vec3 p_head = p_d - vec3(offset.x, height, offset.y);
	                float h_tilt = (pop_seed - 0.5) * 0.4;
	                float c_h = cos(h_tilt); float s_h = sin(h_tilt);
	                p_head.xz *= mat2(vec2(c_h, s_h), vec2(-s_h, c_h));
	                
	                float body = sdCapsule(p_head, vec3(0.0), vec3(0.0, 0.4, 0.0), h_rad);
	                float nose = length(p_head - vec3(0.0, 0.25, h_rad - 0.01)) - 0.04;
	                heads = smin(heads, smin(body, nose, 0.05), 0.12);

	                // --- INCREASED RARITY ---
	                // Threshold raised to 0.82 (~18% chance per pop)
	                float burst_trigger = step(0.82, hash(fi * 55.5 + pop_id));
	                // Narrower life window (0.15 to 0.45) for a snappier burst
	                float is_popping = step(0.15, pop_smooth) * step(pop_smooth, 0.45) * burst_trigger;
	                
	                for(int k = 0; k < 4; k++) {
	                    float fk = float(k);
	                    float p_seed = hash(fi * 31.0 + fk + pop_id);
	                    float p_life = smoothstep(0.15, 0.45, pop_smooth); 
	                    
	                    float burst_ang = (fk / 4.0) * 6.28 + (p_seed * 0.5);
	                    vec3 vel = vec3(cos(burst_ang) * 3.2, (4.5 + p_seed * 2.0) * (1.0 + sat_m * 0.3), sin(burst_ang) * 3.2);
	                    
	                    vec3 p_part = p_d - vec3(offset.x, 0.05, offset.y);
	                    p_part -= (vel * p_life * is_popping);
	                    p_part.y += (9.0 * p_life * p_life * is_popping); 
	                    
	                    float p_size = mix(0.035, 0.075, p_seed);
	                    float fade = smoothstep(0.7, 0.0, p_life); 
	                    dirt = min(dirt, length(p_part) - (p_size * fade * is_popping));
	                }
	            }
	            float clipped_heads = max(heads, -ground_clip);
	            d = smin(smin(clipped_heads, mound, 0.12), dirt, 0.02) * s; 
	            res_id = 1.0; 
	        }
	        break;

	        case 49: { // Pikmin
	            float t = audio_time * 1.2;
	            float sat_m = tanh(music_intensity * 1.5);
	            float s = 0.85 * (1.0 + (sat_m * 0.08));
	            
	            // --- NON-LINEAR RANDOM SWAY ---
	            // Summing primes to break periodicity
	            float sway_x = sin(t * 0.7) * 0.15 + sin(t * 1.3) * 0.05 + cos(t * 2.1) * 0.03;
	            float sway_z = cos(t * 0.6) * 0.1 + sin(t * 1.7) * 0.04;
	            float global_bob = sin(t * 0.8) * 0.1 + sin(t * 3.3) * 0.02;
	            
	            vec3 p_p = (p_mod - ch_p - vec3(sway_x, global_bob - 0.25, sway_z)) / s;
	            
	            // Erratic jog bounce
	            float bounce = abs(sin(t * 5.0 + sin(t * 2.0))) * 0.08; 
	            p_p.y -= bounce;
	            local_p = p_p;

	            float body = sdCapsule(p_p, vec3(0.0, -0.25, 0.0), vec3(0.0, 0.25, 0.0), 0.08);
	            float head = length(p_p - vec3(0.0, 0.35, 0.0)) - 0.14;
	            float base = smin(body, head, 0.1);
	            
	            float bend = sin(t * 2.5 + sway_x) * 0.12;
	            vec3 p_stem = p_p - vec3(0.0, 0.45, 0.0);
	            float stem = sdCapsule(p_stem, vec3(0.0), vec3(bend, 0.25, 0.0), 0.012);
	            
	            vec3 p_leaf = p_stem - vec3(bend, 0.28, 0.0);
	            float flutter = t * 8.0 + (sway_x * 10.0);
	            float c_f = cos(flutter);
	            float s_f = sin(flutter);
	            p_leaf.xy *= mat2(vec2(c_f, s_f), vec2(-s_f, c_f));
	            float leaf = length(p_leaf / vec3(1.8, 0.2, 1.0)) - 0.12;
	            
	            float limbs = 1e10;
	            for(int i = 0; i < 2; i++) {
	                float side = (i == 0) ? 1.0 : -1.0;
	                float walk_off = (i == 0) ? 0.0 : 3.14;
	                float p_t = t * 5.0 + walk_off;
	                
	                float leg_swing = sin(p_t) * 0.15;
	                float leg_lift = max(0.0, cos(p_t)) * 0.1;
	                float leg = sdCapsule(p_p, vec3(side * 0.06, -0.25, 0.0), vec3(side * 0.08 + leg_swing, -0.45 + leg_lift, 0.0), 0.035);
	                
	                float arm_swing = cos(p_t) * 0.25;
	                float arm = sdCapsule(p_p, vec3(side * 0.08, 0.1, 0.0), vec3(side * 0.25, 0.2 + arm_swing, 0.15), 0.025);
	                limbs = min(limbs, min(leg, arm));
	            }

	            d = smin(smin(base, limbs, 0.05), smin(stem, leaf, 0.02), 0.05) * s;
	            res_id = 3.0;
	        }
	        break;
			
			case 50: { // Dual-State Rig - Low Frequency Logic
	            float t_slow = audio_time * 0.125; // Roll for a new state every 8 seconds
	            float t_motion = audio_time * 1.2; // Relaxed motion speed
	            
	            // --- STATE MACHINE (Lowered Frequency) ---
	            float interval = floor(t_slow); 
	            float next_interval = floor(t_slow + 1.0);
	            
	            float hash_current = fract(sin(interval * 43758.5453) * 123.456);
	            float hash_next = fract(sin(next_interval * 43758.5453) * 123.456);
	            
	            float roll_current = step(hash_current, 0.3);
	            float roll_next = step(hash_next, 0.3);
	            
	            // Blends between rolls over the last 20% of the interval
	            float state_mix = mix(roll_current, roll_next, smoothstep(0.8, 1.0, fract(t_slow)));

	            // --- ELASTIC SCALING ---
	            float elastic = sqrt(1.0 + music_intensity * 0.1) - 1.0;
	            float throb = (pow(max(0.0, sin(t_motion * 2.0)), 4.0) * 0.03);
	            float base_s = mix(0.78, 0.92, state_mix);
	            float s = base_s * (1.0 + throb + elastic); 
	            
	            // --- CORE MOTION (Reduced Frequency) ---
	            float force = clamp(music_intensity, 0.0, 1.0);
	            float bounce = mix(0.0, 0.035, force) * sin(t_motion * 2.5);
	            float h_sway = sin(t_motion * 0.7) * 0.12;
	            
	            vec3 p_h = (p_mod - ch_p - vec3(h_sway, bounce, 0.0)) / s;
	            float z_tilt = h_sway * 0.3; 
	            mat2 rot_z = mat2(vec2(cos(z_tilt), sin(z_tilt)), vec2(-sin(z_tilt), cos(z_tilt)));
	            p_h.xy *= rot_z;
	            
	            // --- JIGGLE ---
	            float v_jiggle = mix(0.0, 0.04, force) * sin(t_motion * 2.5 - 0.7);
	            float h_jiggle = h_sway * 0.25 * sin(t_motion * 1.8);
	            vec3 p_t = p_h - vec3(h_jiggle, v_jiggle, 0.0);
	            local_p = p_h;

	            float spacing = mix(0.35, 0.38, state_mix); 
	            float radius = mix(0.33, 0.35, state_mix); 
	            
	            // 1. VOLUMES
	            vec3 p_l = p_t - vec3(-spacing, -0.25, 0.4), p_r = p_t - vec3(spacing, -0.25, 0.4);
	            float pair = smin(smin(length(p_l)-radius, length(p_r)-radius, 0.15), 
	                             min(length(p_l.xz)+p_l.y*0.45-0.12, length(p_r.xz)+p_r.y*0.45-0.12), 0.25);
	            pair = smax(pair, -(p_t.y + 0.6), 0.15); 

	            // 2. AREOLA & NIPPLE
	            float areola = min(length((p_l-vec3(0,-0.05,radius-0.02))/vec3(1,1,0.1))-0.17, 
	                               length((p_r-vec3(0,-0.05,radius-0.02))/vec3(1,1,0.1))-0.17);
	            float nipple = min(length(p_l-vec3(0,-0.05,radius+0.02))-0.035, 
	                               length(p_r-vec3(0,-0.05,radius+0.02))-0.035);
	            
	            // 3. BRA & ANIMATED STRAPS
	            float wire_l = sdCapsule(p_h, vec3(-0.1, -0.22, 0.48), vec3(-0.55, -0.15, 0.48), 0.04);
	            float wire_r = sdCapsule(p_h, vec3(0.1, -0.22, 0.48), vec3(0.55, -0.15, 0.48), 0.04);
	            
	            float strap_ripple = sin(p_h.y * 8.0 - t_motion * 3.0) * (0.004 + force * 0.008);
	            float slack = bounce * 0.4; 
	            
	            vec3 strap_pos_l = vec3(-(spacing+0.15) + strap_ripple + slack, -0.1, 0.45);
	            vec3 strap_pos_r = vec3((spacing+0.15) - strap_ripple - slack, -0.1, 0.45);
	            
	            float strap_l = sdCapsule(p_h, strap_pos_l, vec3(-(spacing+0.1), 0.6, 0.1), 0.025);
	            float strap_r = sdCapsule(p_h, strap_pos_r, vec3((spacing+0.1), 0.6, 0.1), 0.025);
	            
	            float bra = smin(min(wire_l, wire_r), min(strap_l, strap_r), 0.1);

	            d = smin(smin(pair, areola, 0.02), nipple, 0.01);
	            d = smin(d, bra, 0.04) * s;
	            
	            res_id = 1.0; 
	        }
	        break;
		};

    return vec2(d / 1.2, res_id);
}

vec3 get_internal_fill(vec3 hit_p, int effect_id, vec3 color_rgb, vec3 alt_rgb, float time, float intensity) {
    vec3 internal_fill = color_rgb;
    // Normalized intensity for scaling effects if needed
    float i = intensity; 

    switch (effect_id) {
        case 0: { // Base Color
            internal_fill = color_rgb;
        } break;
        case 1: { // Vapor Grid
            vec2 g = fract((hit_p.xy + vec2(0.0, time * 0.4)) * 8.0) - 0.5;
            float grid = step(0.44, max(abs(g.x), abs(g.y)));
            internal_fill = mix(color_rgb, alt_rgb, grid);
        } break;
        case 2: { // Solar Orbits
            float dist = length(hit_p - vec3(0.0, 0.35, 0.0));
            float orbit = step(0.92, sin(dist * 35.0 - time * 8.0));
            internal_fill = mix(color_rgb, alt_rgb, orbit);
        } break;
        case 3: { // Matrix Stripes
            float col = floor(hit_p.x * 15.0);
            float speed = hash(col) * 4.0 + 2.0;
            float stripes = step(0.85, sin(hit_p.y * 25.0 + time * speed));
            internal_fill = mix(color_rgb, alt_rgb, stripes);
        } break;
        case 4: { // Energy Scan
            float scan = step(0.97, sin(hit_p.y * 8.0 - time * 6.0));
            internal_fill = mix(color_rgb, alt_rgb, scan);
        } break;
        case 5: { // Topo Lines
            float topo = step(0.9, sin(hit_p.y * 25.0 + hit_p.x * 12.0 + time * 5.0));
            internal_fill = mix(color_rgb, alt_rgb, topo);
        } break;
        case 6: { // Bio Sparks
		    vec2 grid = floor(hit_p.xy * 20.0);
		    float n = hash(grid.x + grid.y * 57.0 + floor(time * 15.0));
		    float spark = step(0.98, n) * (sin(time * 20.0 + n * 6.28) * 0.5 + 0.5);
		    internal_fill = mix(color_rgb, alt_rgb, spark);
		} break;
        case 7: { // Cellular Armor
            vec2 p = (hit_p.xy + vec2(time * 0.3, time * 0.15)) * 10.0;
            float cells = step(0.46, length(fract(p) - 0.5));
            internal_fill = mix(alt_rgb, color_rgb, cells);
        } break;
        case 8: { // Lava Bubbles
		    vec2 p = hit_p.xy * 8.0;
		    float b = length(fract(p + vec2(0.0, time)) - 0.5);
		    float bubbles = step(b, 0.2 + 0.1 * sin(time + hash(floor(p.x))));
		    internal_fill = mix(color_rgb, alt_rgb, bubbles);
		} break;
        case 9: { // Dot Matrix
            vec2 p = (hit_p.xy + vec2(time * 0.5)) * 18.0;
            float dots = step(0.35, length(fract(p) - 0.5));
            internal_fill = mix(alt_rgb, color_rgb, dots);
        } break;
        case 10: { // Data Stream
            float row = floor(hit_p.y * 20.0);
            float speed = (hash(row) - 0.5) * 12.0;
            float stream = step(0.9, sin(hit_p.x * 20.0 + time * speed));
            internal_fill = mix(color_rgb, alt_rgb, stream);
        } break;
        case 11: { // Zebra Sweep
            float line = step(0.85, sin((hit_p.x + hit_p.y) * 18.0 + time * 10.0));
            internal_fill = mix(color_rgb, alt_rgb, line);
        } break;
        case 12: { // Laser Sweep
            float laser = step(abs(hit_p.y - (sin(time * 2.5) * 0.4 + 0.3)), 0.015);
            internal_fill = mix(color_rgb, alt_rgb, laser);
        } break;
        case 13: { // Static Glitch
		    float blockY = floor(hit_p.y * 12.0);
		    float lineY = floor(hit_p.y * 120.0);
		    float combinedNoise = hash(blockY + floor(time * 12.0)) + hash(lineY + floor(time * 24.0));
		    float glitch = step(0.85, fract(combinedNoise));
		    float strip = step(0.1, fract(hit_p.x + combinedNoise));
		    internal_fill = mix(color_rgb, alt_rgb, glitch * strip);
		} break;
        case 14: { // Silk Threads
            float wave = step(0.6, sin(hit_p.x * 25.0 + time) * cos(hit_p.y * 25.0 - time));
            internal_fill = mix(color_rgb, alt_rgb, wave);
        } break;
        case 15: { // Tunnel Starfield
            vec2 p = (hit_p.xy - vec2(0.0, 0.35)) * 12.0;
            float angle = atan(p.y, p.x);
            float dist = length(p);
            float stars = step(0.98, hash(floor(angle * 8.0) + floor(dist - time * 7.0)));
            internal_fill = mix(color_rgb, alt_rgb, stars);
        } break;
        case 16: { // Rolling Magma
            float flow = step(0.8, sin(hit_p.y * 10.0 - time * 3.0 + sin(hit_p.x * 5.0 + time)));
            internal_fill = mix(color_rgb, alt_rgb, flow);
        } break;
        case 17: { // Circuit Flow
		    vec2 grid = floor(hit_p.xy * 10.0);
		    float flow = step(0.95, sin(grid.x * 1.5 + grid.y * 2.0 + time * 5.0));
		    float line = step(0.45, fract(hit_p.x * 10.0)) * step(0.45, fract(hit_p.y * 10.0));
		    internal_fill = mix(color_rgb, alt_rgb, flow * (1.0 - line));
		} break;
        case 18: { // Radar Ring
            float r = length(hit_p.xy - vec2(0.0, 0.35));
            float ring = step(0.97, fract(r * 5.0 - time * 2.5));
            internal_fill = mix(color_rgb, alt_rgb, ring);
        } break;
        case 19: { // Honeycomb Pulse
            vec2 q = hit_p.xy * 14.0; q.x *= 0.866; q.y += mod(floor(q.x), 2.0) * 0.5;
            float hex = step(0.42, length(fract(q) - 0.5));
            float wave = step(0.7, sin(hit_p.y * 6.0 - time * 12.0));
            internal_fill = mix(color_rgb, alt_rgb, (1.0 - hex) * wave);
        } break;
       	case 20: { // Glitch Slices
		    float y = hit_p.y * 25.0;
		    float jitter = (hash(floor(y) + floor(time * 12.0)) - 0.5) * 0.2;
		    float slice = step(0.9, hash(floor(y + time) * 0.1));
		    float bar = step(0.1, fract(hit_p.x + jitter + time));
		    internal_fill = mix(color_rgb, alt_rgb, slice * (1.0 - bar));
		} break;
        case 21: { // Plasma Drift
		    vec2 uv = hit_p.xy * 2.0;
		    float v1 = sin(uv.x + time);
		    float v2 = sin(2.0 * (uv.y + time));
		    float v3 = sin(uv.x + uv.y + time);
		    float dist = sqrt(uv.x * uv.x + uv.y * uv.y);
		    float v4 = sin(dist + time);
		    float p = sin((v1 + v2 + v3 + v4) * 2.0);
		    internal_fill = mix(color_rgb, alt_rgb, smoothstep(0.4, 0.6, p));
		} break;
        case 22: { // Rotating Cubes
            vec2 p = hit_p.xy;
            float a = time * 2.5;
            mat2 m = mat2(vec2(cos(a), sin(a)), vec2(-sin(a), cos(a))); 
            p = m * p;
            float cubes = step(0.42, max(abs(fract(p.x * 6.0) - 0.5), abs(fract(p.y * 6.0) - 0.5)));
            internal_fill = mix(alt_rgb, color_rgb, cubes);
        } break;
        case 23: { // Overdrive Bars
            float bars = step(0.9, sin(hit_p.y * 120.0 - time * 60.0));
            internal_fill = mix(color_rgb, alt_rgb, bars);
        } break;
        case 24: { // Falling Shards
            vec2 p = hit_p.xy * 12.0;
            p.y -= time * 9.0; p.x += time * 3.0;
            float shards = step(0.98, hash(floor(p.x) + floor(p.y)));
            internal_fill = mix(color_rgb, alt_rgb, shards);
        } break;
        case 25: { // Binary Rain
            float col = floor(hit_p.x * 35.0);
            float rain = step(0.98, fract(hit_p.y * 0.3 + time * (1.5 + hash(col) * 3.0)));
            internal_fill = mix(color_rgb, alt_rgb, rain);
        } break;
        case 26: { // Spinning Vortex
            vec2 p = hit_p.xy - vec2(0.0, 0.35);
            float a = atan(p.y, p.x) + time * 5.0;
            float vortex = step(0.9, sin(a * 4.0 + length(p) * 12.0));
            internal_fill = mix(color_rgb, alt_rgb, vortex);
        } break;
        case 27: { // Sonar Pings
            float ping = step(0.99, fract(length(hit_p.xy - vec2(0.0, 0.35)) * 2.5 - time * 4.0));
            internal_fill = mix(color_rgb, alt_rgb, ping);
        } break;
        case 28: { // Wind Streaks
            float streaks = step(0.99, sin(hit_p.y * 180.0 + time * 80.0));
            internal_fill = mix(color_rgb, alt_rgb, streaks);
        } break;
        case 29: { // Hex Shield
            vec2 q = hit_p.xy * 18.0; q.x *= 0.866; q.y += mod(floor(q.x), 2.0) * 0.5;
            float wave = step(0.95, sin(hit_p.y * 5.0 - time * 10.0));
            float hex = step(0.15, length(fract(q) - 0.5));
            internal_fill = mix(color_rgb, alt_rgb, wave * (1.0 - hex));
        } break;
        case 30: { // Atomic Particles
		    vec3 p = hit_p * 8.0;
		    vec3 id = floor(p);
		    float n = hash(id.x + id.y * 37.0 + id.z * 91.0);
		    vec3 offset = vec3(sin(time * 2.0 + n * 6.2), cos(time * 3.0 + n * 5.0), sin(time * 1.5 + n));
		    float particle = smoothstep(0.2, 0.0, length(fract(p) - 0.5 + offset * 0.3));
		    internal_fill = mix(color_rgb, alt_rgb, particle * step(0.7, n));
		} break;
        case 31: { // Tracking Lines
		    float scan = sin(hit_p.y * 50.0 - time * 20.0);
		    float flicker = step(0.7, hash(floor(time * 30.0)));
		    internal_fill = mix(color_rgb, alt_rgb, step(0.9, scan) * flicker);
		} break;
        case 32: { // Neural Flash
		    vec2 grid = floor(hit_p.xy * 15.0);
		    float active = step(0.98, hash(grid.x + grid.y * 13.0 + floor(time * 8.0)));
		    internal_fill = mix(color_rgb, alt_rgb, active);
		} break;
        case 33: { // Outward Ripples
            float ripple = step(0.95, sin(length(hit_p.xy) * 25.0 - time * 15.0));
            internal_fill = mix(color_rgb, alt_rgb, ripple);
        } break;
        case 34: { // Power Grid
            vec2 g = fract(hit_p.xy * 14.0 + vec2(0.0, time * 1.2));
            float line = step(0.94, max(g.x, g.y));
            internal_fill = mix(color_rgb, alt_rgb, line);
        } break;
        case 35: { // Vertical Bitstream
            float col = floor(hit_p.x * 45.0);
            float bit = step(0.85, hash(floor(hit_p.y * 12.0 + time * 25.0 + col)));
            internal_fill = mix(color_rgb, alt_rgb, bit * step(0.92, hash(col)));
        } break;
        case 36: { // Scrolling Vines
            float vine = step(0.9, sin(hit_p.x * 12.0 + sin(hit_p.y * 6.0 + time * 3.0)));
            internal_fill = mix(color_rgb, alt_rgb, vine);
        } break;
        case 37: { // Electric Cracks
		    float crack = step(0.98, hash(hit_p.x * 50.0 + hit_p.y * 2.0 + time));
		    float jitter = step(0.98, hash(hit_p.y * 50.0 - time));
		    internal_fill = mix(color_rgb, alt_rgb, max(crack, jitter));
		} break;
        case 38: { // Swarming Nanites
		    vec2 p = floor(hit_p.xy * 40.0);
		    float swarm = step(0.97, hash(p.x + p.y * 41.0 + floor(time * 12.0)));
		    internal_fill = mix(color_rgb, alt_rgb, swarm);
		} break;
        case 39: { // Moving Arcs
            float arc = step(abs(hit_p.x - sin(hit_p.y * 5.0 + time * 12.0) * 0.25), 0.02);
            internal_fill = mix(color_rgb, alt_rgb, arc);
        } break;
        case 40: { // Fractal Zoom
		    float zoom = fract(time * 0.5);
		    vec2 p = fract((hit_p.xy - 0.5) * (1.0 / zoom) * 4.0);
		    float box = step(0.9, max(p.x, p.y));
		    internal_fill = mix(color_rgb, alt_rgb, box);
		} break;
        case 41: { // Moire Lines
            float m = step(0.8, sin(hit_p.x * 50.0 + time * 25.0) + sin(hit_p.y * 50.0 - time * 20.0));
            internal_fill = mix(color_rgb, alt_rgb, m);
        } break;
        case 42: { // Vertical Drift
            float drift = step(0.95, hash(hit_p.x + floor(hit_p.y * 6.0 - time * 12.0)));
            internal_fill = mix(color_rgb, alt_rgb, drift);
        } break;
        case 43: { // Clockwise Sweep
            vec2 p = hit_p.xy - vec2(0.0, 0.35);
            float angle = atan(p.y, p.x);
            float sweep = step(0.94, sin(angle - time * 10.0));
            internal_fill = mix(color_rgb, alt_rgb, sweep);
        } break;
        case 44: { // Pulse Wave
		    float d = length(hit_p.xy - 0.5);
		    float wave = step(0.95, sin(d * 30.0 - time * 15.0));
		    internal_fill = mix(color_rgb, alt_rgb, wave);
		} break;
        case 45: { // Square Tiling
            vec2 p = fract(hit_p.xy * 10.0 - vec2(time * 2.5));
            float sq = step(0.85, p.x) * step(0.85, p.y);
            internal_fill = mix(color_rgb, alt_rgb, sq);
        } break;
        case 46: { // Diagonal Rain
            float d = (hit_p.x + hit_p.y);
            float rain = step(0.95, fract(d * 12.0 + time * 15.0));
            internal_fill = mix(color_rgb, alt_rgb, rain);
        } break;
        case 47: { // Interference Bars
            float bars = step(0.9, hash(floor(hit_p.y * 50.0 + time * 30.0)));
            internal_fill = mix(color_rgb, alt_rgb, bars);
        } break;
        case 48: { // Diamond Pulse
            float d = abs(hit_p.x) + abs(hit_p.y - 0.35);
            float pulse = step(0.95, sin(d * 40.0 - time * 18.0));
            internal_fill = mix(color_rgb, alt_rgb, pulse);
        } break;
        case 49: { // RGB Shift
		    float slice = floor(hit_p.y * 60.0);
		    float shift = step(0.9, hash(slice + floor(time * 30.0)));
		    internal_fill = mix(color_rgb, alt_rgb, shift * step(0.2, fract(hit_p.x * 5.0)));
		} break;
        default: { // Base Color
            internal_fill = color_rgb;
        } break;
    }
	
    return internal_fill;
}

void vertex() {
    v_world_pos = (MODEL_MATRIX * vec4(VERTEX, 0.0, 1.0)).xy;
    v_world_y = MODEL_MATRIX[3].y; 
    v_auto_seed = hash(MODEL_MATRIX[3].x + MODEL_MATRIX[3].y);
    v_punch_variation = hash(MODEL_MATRIX[3].x - MODEL_MATRIX[3].y);
    
    // ADJUSTED THRESHOLDS (Original 1100 / 2 and 1550 / 2)
    // As v_world_y approaches the top (smaller values), the scalar decreases
    float scale_trigger = clamp((v_world_y - 550.0) / (775.0 - 550.0), 0.0, 1.0);
    
    // Middle-ground scaling: maintained relative sizes (0.425 to 0.5)
    v_size_scalar = mix(0.425, 0.5, scale_trigger);
    
    // Maintains boundary clarity through the transition
    v_stable_zoom = mix(0.5, 0.8, pow(scale_trigger, 2.0));
    
    // Coordinates normalized to the new screen height (half of 2160)
    v_y_pos = clamp(v_world_y / 1080.0, 0.0, 1.0);
    
    float alpha_ramp = sqrt(scale_trigger); 
    v_world_alpha = 1.0;
}

void fragment() {
    vec2 uv = (((UV - 0.5) / v_stable_zoom) / v_size_scalar) * uv_scale;
    uv.y = -uv.y; 
    
    vec3 ro = vec3(uv, 3.5), rd = vec3(0, 0, -1);
    float t_dist = 0.0, min_d = 1000.0; 

    // --- LONG-FORM STRATEGY ---
    float block_duration = 20.0;
    float time_block = floor(audio_time / block_duration);
    float block_progress = mod(audio_time, block_duration);
    float transition = smoothstep(0.0, 5.0, block_progress);

    float rand = hash(time_block + v_auto_seed);
    int state = int(rand * 3.0); 
    
    float prev_rand = hash((time_block - 1.0) + v_auto_seed);
    int prev_state = int(prev_rand * 3.0);

    vec3 hit_p = vec3(0.0); 
    float hit_id = 1.0; 
    float final_sub_d = 1000.0;
    
    // Alias for the new outline color to keep logic clean
    vec3 strict_alt = color_outline.rgb;

	for(int i = 0; i < 45; i++) {
        vec3 p = ro + rd * t_dist;
        vec3 lp_prev, lp_curr;
        float sub_d_prev, sub_d_curr; 
        
        // --- OPTIMIZATION: Only double-sample if we are actually transitioning ---
        float d;
        vec2 res_curr = map_master(p, v_auto_seed, state, uv, lp_curr, sub_d_curr);
        
        if (transition > 0.01 && transition < 0.99) {
            vec2 res_prev = map_master(p, v_auto_seed, prev_state, uv, lp_prev, sub_d_prev);
            d = mix(res_prev.x, res_curr.x, transition);
        } else {
            d = res_curr.x;
        }
        
        if (d < 0.005) { // Tightened hit threshold
	        hit_id = res_curr.y; 
	        hit_p = (transition > 0.01) ? mix(lp_prev, lp_curr, transition) : lp_curr;
	        final_sub_d = (transition > 0.01) ? mix(sub_d_prev, sub_d_curr, transition) : sub_d_curr;
	        
			if (hit_id < 1.5) {
		        min_d = 0.0; 
		    }
	        break; 
	    }

        if (t_dist > 12.0) break; // Reduced max distance slightly

		t_dist += d * 0.75; // More conservative steps
        min_d = min(min_d, d);
    }

    float br = mix(0.2, 2.5, v_y_pos);
    float ground_mask = smoothstep(-0.45, -0.12, uv.y); 
    float vertical_fade = 1.0;
    float final_alpha = vertical_fade * ground_mask;
    vec2 unit_size = vec2(dFdx(uv.x), dFdy(uv.y));

    // --- CONFINED SCROLLING RIPPLE ---
    float pulse_intensity = 0.0;
    float burst_cycle_time = 4.0; 
    float burst_window = 0.3;     
    float burst_timer = fract(audio_time / burst_cycle_time + v_auto_seed);
    
    if (burst_timer < burst_window) { 
        float progress = burst_timer / burst_window;
        float expansion = progress * 0.4; 
        float scroll_fade = clamp(1.0 - progress, 0.0, 1.0);
        float thickness = 0.02 * scroll_fade; 

        int wave_count = 1 + int(music_intensity * 3.0); 
        for(int i = 0; i < wave_count; i++) {
            float offset = float(i) * 0.08; 
            float dist_to_ring = abs(min_d - (expansion - offset));
            float wave = smoothstep(thickness, 0.0, dist_to_ring) * scroll_fade;
            pulse_intensity = max(pulse_intensity, wave);
        }
        pulse_intensity *= (0.8 + music_intensity * 2.0);
    }

    float hit_threshold = 1.1 * length(unit_size);
    float outline_threshold = 6.5 * length(unit_size) * 0.7071 * mix(0.2, 1.0, v_y_pos);
    float v_grad = mix(0.7, 1.0, smoothstep(-0.25, 0.45, uv.y));
    float final_br = br * v_grad;
    float fx_alpha = v_world_alpha * ground_mask;
    
    // --- EFFECT HIERARCHY ---
    float mask_a = get_internal_fill(hit_p, effect_slot_0, vec3(0.0), vec3(1.0), audio_time, music_intensity).r;
    float mask_b = get_internal_fill(hit_p, effect_slot_1, vec3(0.0), vec3(1.0), audio_time, music_intensity).r;
    float mask_c = get_internal_fill(hit_p, effect_slot_2, vec3(0.0), vec3(1.0), audio_time, music_intensity).r;
    float mask_d = get_internal_fill(hit_p, effect_slot_3, vec3(0.0), vec3(1.0), audio_time, music_intensity).r;
    
    vec3 final_fill;
    if (mask_a > 0.5) {
        final_fill = color_effect_slot_0.rgb;
    } else if (mask_b > 0.5) {
        final_fill = color_effect_slot_1.rgb;
    } else if (mask_c > 0.5) {
        final_fill = color_effect_slot_2.rgb;
    } else if (mask_d > 0.5) {
        final_fill = color_effect_slot_3.rgb;
    } else {
        final_fill = color_base.rgb;
    }

    if (min_d < hit_threshold) {
        vec3 final_color = final_fill;
        
        if (model == 9) { // Pokeball
            if (hit_p.y > 0.0) {
                // Top half uses Effect 0 (previously alt_color)
                final_color = color_outline.rgb * (final_fill / max(vec3(0.01), color_base.rgb));
            }
            if (length(hit_p - vec3(0.0, 0.0, 0.25)) < 0.08) {
                final_color = color_outline.rgb; 
            }
        }
        else if (model == 22) { // Companion Cube
            if (hit_id > 2.5) { final_color = final_fill; } // Heart
            else if (hit_id > 1.5) { final_color = strict_alt; } // Holder
            else { final_color = final_fill; } // Body
        } 
        else if (model == 23 || model == 24 || model == 27 || model == 28) {
            if (hit_id > 1.5) { final_color = strict_alt; } 
            else { final_color = final_fill; }
        }
        
        COLOR = vec4(final_color * final_br, vertical_fade); 
    } else if (min_d < outline_threshold) {
        vec3 outline_col = strict_alt;

        if (model == 9) { // Pokeball outline inversion
            outline_col = (hit_p.y > 0.0) ? color_base.rgb : color_outline.rgb;
        }
        
        COLOR = vec4(outline_col * final_br, vertical_fade); 
    } else {
        float aura_val = pow(max(0.0, 1.0 - (min_d / (0.12 * v_size_scalar))), 8.0);
        if (pulse_intensity > 0.01) {
            COLOR = vec4(strict_alt * clamp(pulse_intensity * final_br, 0.0, 2.0), pulse_intensity * fx_alpha);
        } else if (aura_val > 0.01) {
            COLOR = vec4(color_base.rgb * aura_val * final_br * 1.5, aura_val * fx_alpha);
        } else { 
            discard; 
        }
    }
    
    // Sheen/Sweep
    float instance_offset = v_auto_seed * 100.0;
    float instance_speed = 0.5 * (0.9 + hash(v_auto_seed) * 0.2);
    float sheen_val = fract((v_world_pos.x + v_world_pos.y * 0.6) * 0.00008 - (audio_time * instance_speed) + instance_offset);
    float ray_mask = exp(-(pow(sheen_val - 0.5, 2.0)) / 0.00002);
    COLOR.rgb += vec3(1.0) * ray_mask * 0.8 * final_br;
}