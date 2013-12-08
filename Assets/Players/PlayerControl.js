#pragma strict

import System.Collections.Generic;

var playerIndex:int;
var moveSpeed:float;
var movementSmooth:float;
var turnSmooth:float;
var fenceWidth:float;
var bodyGraphic:Renderer;
var punchHitbox:Renderer;
var myCollider:Collider;
var maxPunchCharge:float;
var punchHitTime:float;
var punchCooldownTime:float;
var punchRadius:float;
var punchKnockback:float;
var bodyPunchColor:Color;
var bodyCooldownColor:Color;

@HideInInspector
var position:Vector2;
@HideInInspector
var inputPrefix:String;
@HideInInspector
var health:float=1f;
@HideInInspector
var stunTimer:float=0f;

private var facingDirection:Vector3;
private var punchTimer:float=0f;
private var punching:boolean=false;
private var punchCooldown:float=0f;
private var punchHitTimer:float=0f;
private var punchStrength:float;
private var bodyStartingColor:Color;

static var playBounds:Rect;
static var doneInit:boolean=false;
static var allPlayers:List.<PlayerControl>;

function Start () {
	var i:int;
	
	if (doneInit==false) {
		doneInit=true;
		allPlayers=new List.<PlayerControl>();
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
	
	bodyStartingColor=bodyGraphic.material.color;
	position=Utilities.Vector3To2(transform.position);
	inputPrefix="P"+(playerIndex+1);
}

function Update () {
	var i:int;
	var j:int;
	var inputDir:Vector2=new Vector2(Input.GetAxis(inputPrefix+"Horizontal"),Input.GetAxis(inputPrefix+"Vertical"))*2f;
	if (inputDir.magnitude>1f) inputDir=inputDir.normalized;
	if (inputDir.magnitude>0f) {
		facingDirection=Utilities.Vector2To3(inputDir);
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
	
	if (Input.GetButton(inputPrefix+"Fire1")) {
		if (punchCooldown<=0f) {
			punchHitTimer=punchHitTime;
			punchStrength=1f;
			punchCooldown=punchCooldownTime;
		}
		/*if (punching==false) {
			if (punchCooldown<=0f) {
				punching=true;
				punchTimer=0f;
			}
		} else {
			punchTimer+=Time.deltaTime;
			if (punchTimer>maxPunchCharge) {
				punching=false;
				punchHitTimer=punchHitTime;
				punchStrength=1f;
				punchCooldown=punchMaxCooldown;
				punchTimer=0f;
			}
		}*/
	} else {
		if (punching) {
			punching=false;
			punchHitTimer=punchHitTime;
			punchStrength=punchTimer/maxPunchCharge;
			//punchCooldown=Mathf.Lerp(punchMinCooldown,punchMaxCooldown,punchStrength);
			punchTimer=0f;
		}
	}
	
	bodyGraphic.material.color=Color.Lerp(bodyStartingColor,bodyPunchColor,punchTimer/maxPunchCharge);
	
	var punchDir:Vector2=Utilities.Vector3To2(punchHitbox.transform.position-transform.position).normalized;
	if (punchHitTimer>0f) {
		var hitPlayer:boolean=false;
		for (i=0;i<allPlayers.Count;i++) {
			if (allPlayers[i]!=this) {
				var attackVector:Vector3=allPlayers[i].myCollider.transform.position-punchHitbox.transform.position;
				if (attackVector.magnitude<punchRadius) {
					allPlayers[i].position+=punchDir*punchKnockback;
					hitPlayer=true;
				}
			}
		}
		var chickenHits:Collider[]=Physics.OverlapSphere(punchHitbox.transform.position,.5f,1<<8);
		for (i=0;i<chickenHits.length;i++) {
			var cChicken:Chicken=chickenHits[i].GetComponent(Chicken);
			cChicken.rigidbody.velocity+=Utilities.Vector2To3(punchDir)*2f;
		}
		
		punchHitbox.enabled=true;
		punchHitTimer-=Time.deltaTime;
	} else {
		punchHitbox.enabled=false;
		if (punchCooldown>0f) {
			bodyGraphic.material.color=bodyCooldownColor;
			punchCooldown-=Time.deltaTime;
			if (punchCooldown<=0f) {
				bodyGraphic.material.color=bodyStartingColor;
			}
		}
	}
	
	if (playerIndex==0) {
		for (i=0;i<allPlayers.Count;i++) {
			for (j=i+1;j<allPlayers.Count;j++) {
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