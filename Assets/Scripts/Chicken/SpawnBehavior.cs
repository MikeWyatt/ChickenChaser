using UnityEngine;
using System.Collections;

public class SpawnBehavior : MonoBehaviour, IChickenBehavior {

	// Update is called once per frame
	public void Update() {
	}

	public void OnCollisionEnter(Collision collision) {
		if(!collision.gameObject.CompareTag("Wall")) {
			return;
		}
		GetComponent<Chicken>().ChangeBehavior<RunningBehavior>();
	}
}
