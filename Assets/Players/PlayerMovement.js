#pragma strict

import System.Collections.Generic;

var playerIndex:int;
var bodyGraphic:Renderer;
var stunColor:Color;
var moveSpeed:float;
var movementSmooth:float;
var turnSmooth:float;
var fenceWidth:float;
var myCollider:Collider;
var animator:Animator;

@HideInInspector
var position:Vector2;
@HideInInspector
var inputPrefix:String;
@HideInInspector
var health:float=1f;
@HideInInspector
var stunTimer:float=0f;

private var facingDirection:Vector3;

static var playBounds:Rect;
static var doneInit:boolean=false;
static var allPlayers:List.<PlayerMovement>;

function Start () {
	
	if (doneInit==false) {
		doneInit=true;
		allPlayers=new List.<PlayerMovement>();
	}
	allPlayers.Add(this);

	facingDirection=(-transform.position);
	
	/*if (doneInit==false) {
		doneInit=true;
		fenceRadius=FenceControl.staticFenceRadius;
		var boundaryObjects:GameObject[]=GameObject.FindGameObjectsWithTag("Escape Zone");
		playBounds=new Rect(0,0,5,5);
		for (i=0;i<boundaryObjects.length;i++) {
			switch (boundaryObjects[i].name) {
				case "North Wall":
					playBounds.yMax=boundaryObjects[i].transform.position.z;
					break;
				case "South Wall":
					playBounds.yMin=boundaryObjects[i].transform.position.z;
					break;
				case "East Wall":
					playBounds.xMax=boundaryObjects[i].transform.position.x;
					break;
				case "West Wall":
					playBounds.xMin=boundaryObjects[i].transform.position.x;
					break;
			}
		}
	}*/
	
	position=Utilities.Vector3To2(transform.position);
	inputPrefix="P"+(playerIndex+1);
}

function Update () {
	var cChicken:Chicken;
	var chickenHits:Collider[];
	// Grab movement axis
	var inputDir:Vector2=new Vector2(Input.GetAxis(inputPrefix+"Horizontal"),Input.GetAxis(inputPrefix+"Vertical"))*2f;
	if (stunTimer>0f) {
		//I AM STUNNED
		stunTimer-=Time.deltaTime;
		if (stunTimer<0f) {
			stunTimer=0f;
		}
		inputDir=Vector2.zero;
		bodyGraphic.material.color=stunColor;
	} else {
		bodyGraphic.material.color=Color.white;
	}
	if (inputDir.magnitude>1f) inputDir=inputDir.normalized;
	if (inputDir.magnitude>0f) {
		facingDirection=Utilities.Vector2To3(inputDir);
	}
	
	if(animator) {
		animator.SetBool("IsRunning", inputDir.magnitude>0f);
	}
	
	transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(facingDirection,Vector3.up),turnSmooth*Time.deltaTime);
	position+=inputDir*moveSpeed*Time.deltaTime;
	
	if (position.magnitude<(FenceControl.staticFenceRadius+fenceWidth)) {
		position=position.normalized*(FenceControl.staticFenceRadius+fenceWidth);
	}
	if (position.magnitude>(FenceControl.staticOuterFenceRadius-fenceWidth)) {
		position=position.normalized*(FenceControl.staticOuterFenceRadius-fenceWidth);
	}
	/*if (position.x>playBounds.xMax-fenceWidth) {
		position.x=playBounds.xMax-fenceWidth;
	}
	if (position.x<playBounds.xMin+fenceWidth) {
		position.x=playBounds.xMin+fenceWidth;
	}
	if (position.y>playBounds.yMax-fenceWidth) {
		position.y=playBounds.yMax-fenceWidth;
	}
	if (position.y<playBounds.yMin+fenceWidth) {
		position.y=playBounds.yMin+fenceWidth;
	}*/
	
	transform.position=Vector3.Lerp(transform.position,Utilities.Vector2To3(position),movementSmooth*Time.deltaTime);
	
	if (playerIndex==0) {
		for (var i=0;i<allPlayers.Count;i++) {
			for (var j=i+1;j<allPlayers.Count;j++) {
				var diffVector:Vector2=allPlayers[i].position-allPlayers[j].position;
				if (diffVector.sqrMagnitude<1f) {
					var extra:float=1f-diffVector.magnitude;
					allPlayers[i].position+=diffVector.normalized*extra*.5f;
					allPlayers[j].position-=diffVector.normalized*extra*.5f;
				}
			}
		}
	}
}