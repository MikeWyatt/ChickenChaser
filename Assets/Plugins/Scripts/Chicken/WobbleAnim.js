#pragma strict

private var timer:float=0f;

function Start () {
	
}

function Update () {
	timer+=Time.deltaTime*15f;
	transform.localRotation=Quaternion.Euler(-90f+Mathf.Sin(timer)*15f,90f,0f);
}