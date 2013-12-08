using UnityEngine;
using System.Collections;

public class PlayerDrop : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider) {
		ThrownBehavior thrownBehavior = collider.gameObject.GetComponent<ThrownBehavior>();
		if(thrownBehavior == null || !thrownBehavior.enabled) {
			// the object is not a thrown chicken
			return;
		}

		Destroy(collider.gameObject);

		//increment score for thrownBehavior.Thrower
	}
}
