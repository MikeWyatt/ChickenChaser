#pragma strict

var moveSpeed:float;
var movementSmooth:float;
var turnSmooth:float;
var fenceWidth:float;
var bodyGraphic:Renderer;
var punchHitbox:Renderer;
var maxPunchCharge:float;
var punchHitTime:float;
var punchMinCooldown:float;
var punchMaxCooldown:float;
var bodyPunchColor:Color;
var bodyCooldownColor:Color;

@HideInInspector
var position:Vector2;
@HideInInspector
var inputPrefix:String;
@HideInInspector
var playerIndex:int;

private var facingDirection:Vector3;
private var punchTimer:float=0f;
private var punching:boolean=false;
private var punchCooldown:float=0f;
private var punchHitTimer:float=0f;
private var punchStrength:float;
private var bodyStartingColor:Color;

static var fenceRadius:float;
static var madeFence:boolean=false;

function Start () {
	if (madeFence==false) {
		madeFence=true;
		fenceRadius=FenceControl.staticFenceRadius;
	}
	
	bodyStartingColor=bodyGraphic.material.color;
	position=Utilities.Vector3To2(transform.position);
	playerIndex=0;
	inputPrefix="P"+(playerIndex+1);
}

function Update () {
	var inputDir:Vector2=new Vector2(Input.GetAxis(inputPrefix+"Horizontal"),Input.GetAxis(inputPrefix+"Vertical"))*2f;
	if (inputDir.magnitude>1f) inputDir=inputDir.normalized;
	if (inputDir.magnitude>0f) {
		facingDirection=Utilities.Vector2To3(inputDir);
	}
	transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(facingDirection,Vector3.up),turnSmooth*Time.deltaTime);
	position+=inputDir*moveSpeed*Time.deltaTime;
	
	if (position.magnitude<(fenceRadius+fenceWidth)) {
		position=position.normalized*(fenceRadius+fenceWidth);
	}
	
	transform.position=Vector3.Lerp(transform.position,Utilities.Vector2To3(position),movementSmooth*Time.deltaTime);
	
	if (Input.GetButton(inputPrefix+"Fire1")) {
		if (punching==false) {
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
		}
	} else {
		if (punching) {
			punching=false;
			punchHitTimer=punchHitTime;
			punchStrength=punchTimer/maxPunchCharge;
			punchCooldown=Mathf.Lerp(punchMinCooldown,punchMaxCooldown,punchStrength);
			punchTimer=0f;
		}
	}
	
	bodyGraphic.material.color=Color.Lerp(bodyStartingColor,bodyPunchColor,punchTimer/maxPunchCharge);
	
	if (punchHitTimer>0f) {
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
}