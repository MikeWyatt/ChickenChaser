#pragma strict

import System.Collections.Generic;

var playerIndex:int;
var punchHitbox:Renderer;
var animator:Animator;

@HideInInspector
var inputPrefix:String;

private var grabbedChicken:Chicken;

static var doneInit:boolean=false;

function Start () {
	inputPrefix="P"+(playerIndex+1);
}

function Update () {
	var cChicken:Chicken;
	var chickenHits:Collider[];
	
	if (grabbedChicken==null) {
		if (Input.GetButtonDown(inputPrefix+"Fire2")) {
			chickenHits=Physics.OverlapSphere(punchHitbox.transform.position,.5f,1<<8);
			if (chickenHits.length>0) {
				cChicken=chickenHits[0].GetComponent(Chicken);
				grabbedChicken=cChicken;
				cChicken.ChangeBehavior.<HeldBehavior>();
				cChicken.GetComponent(HeldBehavior).Holder=gameObject;
			}
		}
	} else {
		if (Input.GetButtonDown(inputPrefix+"Fire2")) {
			grabbedChicken.ChangeBehavior.<ThrownBehavior>();
			grabbedChicken.GetComponent(HeldBehavior).Holder=null;
			grabbedChicken.rigidbody.velocity.y=1f;
			grabbedChicken.GetComponent(ThrownBehavior).SetThrowArc();
			grabbedChicken=null;
		}
	}
}