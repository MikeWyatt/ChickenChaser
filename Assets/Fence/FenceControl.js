#pragma strict

var fenceRadius:float;
var fencePieceLength:float;
var fencePiecePrefab:GameObject;

static var staticFenceRadius:float;

function Awake () {
	staticFenceRadius=fenceRadius;
	
	MakeFence();
}

function MakeFence() {
	var i:int;
	var numberOfSides:int=Mathf.CeilToInt(Mathf.PI/(Mathf.Asin(fencePieceLength/(2f*fenceRadius))));
	fenceRadius=fencePieceLength/(2f*Mathf.Sin(Mathf.PI/numberOfSides));
	for (i=0;i<numberOfSides;i++) {
		var angle:float=Mathf.PI*2f*(i*1f/numberOfSides);
		var cPosition:Vector3=new Vector3(Mathf.Cos(angle)*fenceRadius,0f,Mathf.Sin(angle)*fenceRadius);
		angle=Mathf.PI*2f*((i+1)*1f/numberOfSides);
		var nextPosition:Vector3=new Vector3(Mathf.Cos(angle)*fenceRadius,0f,Mathf.Sin(angle)*fenceRadius);
		Instantiate(fencePiecePrefab,cPosition,Quaternion.LookRotation(nextPosition-cPosition));
	}
}

function Update () {

}