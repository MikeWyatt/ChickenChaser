using UnityEngine;
using System.Collections;

public class ThrownBehavior : MonoBehaviour, IChickenBehavior {
	public float throwDistance = 10.0f;

	Vector3 startPos;
	Vector3 endPos;

	// Use this for initialization
	void Awake () {
		startPos = gameObject.transform.position;
		endPos = gameObject.transform.position 
			+ gameObject.transform.forward * throwDistance;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPos, Time.deltaTime);
	}

	void OnCollisionEnter(Collider collider)
	{
		if (collider.tag.Equals("Ground"))
		{
			this.enabled = false;
			Chicken parent = gameObject.GetComponent<Chicken>();
			parent.ChangeBehavior<RunningBehavior>();
		}
	}
}
