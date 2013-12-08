#pragma strict

import System.Collections.Generic;

var playerIndex:int;
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
var animator:Animator;
var featherPoofEffect:GameObject;
var swishSound:AudioSource;
var hitSound:AudioSource;

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

static var doneInit:boolean=false;
static var allPlayers:List.<PlayerMovement>;

function Start () {	
	if (doneInit==false) {
		doneInit=true;
		allPlayers=new List.<PlayerMovement>();
	}
	allPlayers.Add(gameObject.GetComponent(PlayerMovement));
	
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
	inputPrefix="P"+(playerIndex+1);
}

function Update () {
	//put this here to optimize loop memory efficiency
	var i:int;
	var cChicken:Chicken;
	var chickenHits:Collider[];
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
			if (allPlayers[i]!= gameObject.GetComponent(PlayerMovement)) {
				var attackVector:Vector3=allPlayers[i].myCollider.transform.position-punchHitbox.transform.position;
				if (attackVector.magnitude<punchRadius) {
					
					//I JUST PUNCHED SOMEONE
					if (!hitSound.isPlaying) hitSound.Play();


					allPlayers[i].position+=punchDir*punchKnockback;
					allPlayers[i].stunTimer=1f;
					hitPlayer=true;
				}
			}
		}
		chickenHits=Physics.OverlapSphere(punchHitbox.transform.position,.5f,1<<8);
		for (i=0;i<chickenHits.length;i++) {
			cChicken=chickenHits[i].GetComponent(Chicken);
			cChicken.rigidbody.velocity+=Utilities.Vector2To3(punchDir)*2f;
			Instantiate(featherPoofEffect,cChicken.transform.position+Vector3.up*.5f,Quaternion.Euler(-90f,0f,0f));
			hitPlayer=true;
		}
		
		if (hitPlayer==false) {
			//A SWING AND A MISS
			if (!swishSound.isPlaying) swishSound.Play();
		}
		
		//punchHitbox.enabled=true;
		punchHitTimer-=Time.deltaTime;
		
		if(animator) {
			animator.SetBool("IsPunching", true);
		}
	} else {
		//punchHitbox.enabled=false;
		if (punchCooldown>0f) {
			//bodyGraphic.material.color=bodyCooldownColor;
			punchCooldown-=Time.deltaTime;
			if (punchCooldown<=0f) {
				//bodyGraphic.material.color=bodyStartingColor;
			}
		}
		if(animator) {
			animator.SetBool("IsPunching", false);
		}
	}
}