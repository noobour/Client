shader_type canvas_item;

void fragment() {
	COLOR.a *= 1.0 - UV.x - 1.0;
	COLOR.a *= 1.0 - UV.y - 2.0;
}