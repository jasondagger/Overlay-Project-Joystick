shader_type canvas_item;

uniform vec4 color: source_color;
uniform vec4 star_color: source_color;
uniform float at_time;
uniform float at_time_slow;

float rand(vec2 st) {
    return fract(sin(dot(st.xy, vec2(12.9898, 78.233))) * 43758.5453123);
}

void fragment() {
    vec2 current_coords = FRAGCOORD.xy;
    float size = 80.0; 
    float prob = 0.8;  
    
    vec2 pos = floor(current_coords / size);
    float starIntensity = 0.0;
    float starValue = rand(pos);

    if (starValue > prob) {
        // Offset center randomly within the cell so they aren't in a perfect grid
        vec2 offset = vec2(rand(pos), rand(pos + 1.0)) * size * 0.8;
        vec2 center = size * pos + offset;
        
        float useTime = (starValue > 0.93) ? at_time : at_time_slow;
        
        // Slower, smoother pulsing (reduced multiplier from 8.0 to 1.5)
        float t = 0.9 + 0.15 * sin(useTime * 1.5 + starValue * 10.0);
        
        float dist = distance(current_coords, center);
        starIntensity = clamp(1.0 - dist / (0.4 * size), 0.0, 1.0);
        
        float glowX = t / (abs(current_coords.x - center.x) + 0.5);
        float glowY = t / (abs(current_coords.y - center.y) + 0.5);
        starIntensity = starIntensity * glowX * glowY;
    }
    else if (rand(SCREEN_UV.xy / 15.0) > 0.994) {
        float r = rand(SCREEN_UV.xy);
        starIntensity = r * (0.85 * sin(at_time_slow * (r * 2.0)) + 0.95) * 0.5;
    }
    
    COLOR = vec4(vec3(starIntensity), 1.0) + color;
}