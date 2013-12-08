using UnityEngine;
using System.Collections;

public class ThrownBehavior : MonoBehaviour, IChickenBehavior {
	public float throwDistance = 8.0f;

	Vector3 startPos;
	Vector3 endPos;

	// Use this for initialization
	void Awake () {
		SetThrowArc();
	}
	public void SetThrowArc() {
		startPos = gameObject.transform.position;
		endPos = gameObject.transform.position 
			+ gameObject.transform.forward * throwDistance;
		
//		Debug.Log(startPos);
//		Debug.Log(endPos);
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
