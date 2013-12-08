#pragma strict

static function Vector3To2(input:Vector3):Vector2 {
	return(new Vector2(input.x,input.z));
}
static function Vector2To3(input:Vector2):Vector3 {
	return(new Vector3(input.x,0f,input.y));
}