using UnityEngine;
using System.Collections;

public class PlayerDrop : MonoBehaviour {

	public PlayerScore Score;
	public ParticleSystem Particles;

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

		Score.Value++;
		Debug.Log (string.Format ("Player {0} scores!", Score.gameObject.name));
		ParticleSystem go = (ParticleSystem)Instantiate(Particles);
		go.transform.parent = transform;
	}
}
