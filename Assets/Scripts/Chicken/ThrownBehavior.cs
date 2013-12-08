using UnityEngine;
using System.Collections;

public class ThrownBehavior : MonoBehaviour, IChickenBehavior {
	public float throwDistance = 5.0f;

	Vector3 startPos;
	Vector3 endPos;

	// Use this for initialization
	void Awake () {
		startPos = gameObject.transform.position;
		endPos = gameObject.transform.position 
			+ gameObject.transform.forward * throwDistance;
		
		Debug.Log(startPos);
		Debug.Log(endPos);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPos, Time.deltaTime);
	}
}
