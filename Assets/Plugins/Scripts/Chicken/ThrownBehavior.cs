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
		
		Debug.Log(startPos);
		Debug.Log(endPos);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPos, 2f*Time.deltaTime);
	}
	
}
