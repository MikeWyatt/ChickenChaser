#pragma strict

var killTime:float=2f;

function Start () {
	Destroy(gameObject,killTime);
}