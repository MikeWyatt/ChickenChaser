using UnityEngine;
using System.Collections;

public class DroppedBehavior : MonoBehaviour, IChickenBehavior {
	public float throwDistance = 2.0f;
	
	Vector3 endPos;
	
	// Use this for initialization
	void Awake () {
		SetThrowArc();
	}
	public void SetThrowArc() {
		endPos = gameObject.transform.position 
			+ gameObject.transform.forward * throwDistance;
	}
	
	// Update is called once per frame
	void Update () {
		endPos.y=transform.position.y;
		gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPos, 2.5f*Time.deltaTime);
		if (gameObject.transform.position.y<.05f) {
			gameObject.transform.position=new Vector3(transform.position.x,0f,transform.position.z);
			GetComponent<Chicken>().ChangeBehavior<RunningBehavior>();
		}
	}
	
}
