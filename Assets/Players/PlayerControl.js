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

@HideInInspector
var position:Vector2;
@HideInInspector
var inputPrefix:String;
@HideInInspector
var health:float=1f;
@HideInInspector
var stunTimer:float=0f;

private var punchTimer:float=0f;
private var punching:boolean=false;
private var punchCooldown:float=0f;
private var punchHitTimer:float=0f;
private var punchStrength:float;
private var bodyStartingColor:Color;
private var grabbedChicken:Chicken;

static var doneInit:boolean=false;
static var allPlayers:List.<PlayerControl>;

function Start () {
	var i:int;
	
	if (doneInit==false) {
		doneInit=true;
		allPlayers=new List.<PlayerControl>();
	}
	allPlayers.Add(this);
	
	bodyStartingColor=bodyGraphic.material.color;
	inputPrefix="P"+(playerIndex+1);
}

function Update () {
	//Put this here for memory efficiency in loop code
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
			if (allPlayers[i]!=this) {
				var attackVector:Vector3=allPlayers[i].myCollider.transform.position-punchHitbox.transform.position;
				if (attackVector.magnitude<punchRadius) {
					allPlayers[i].position+=punchDir*punchKnockback;
					hitPlayer=true;
				}
			}
		}
		chickenHits=Physics.OverlapSphere(punchHitbox.transform.position,.5f,1<<8);
		for (i=0;i<chickenHits.length;i++) {
			cChicken=chickenHits[i].GetComponent(Chicken);
			cChicken.rigidbody.velocity+=Utilities.Vector2To3(punchDir)*2f;
		}
		
		//punchHitbox.enabled=true;
		punchHitTimer-=Time.deltaTime;
		
		if(animator) {
			animator.SetBool("IsPunching", true);
		}
	} else {
		//punchHitbox.enabled=false;
		if (punchCooldown>0f) {
			bodyGraphic.material.color=bodyCooldownColor;
			punchCooldown-=Time.deltaTime;
			if (punchCooldown<=0f) {
				bodyGraphic.material.color=bodyStartingColor;
			}
		}
		if(animator) {
			animator.SetBool("IsPunching", false);
		}
	}
}