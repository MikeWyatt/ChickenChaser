using UnityEngine;
using System.Collections;

public class ChickenSpawner : MonoBehaviour {
	public GameObject chickenPrefab;
	public float SPAWN_TIMER = 3; // in seconds

	float timeToSpawn;
	// Use this for initialization
	void Start () {
		timeToSpawn = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timeToSpawn -= Time.deltaTime;
		if (timeToSpawn <= 0)
		{
			timeToSpawn += timeToSpawn;
			Instantiate(chickenPrefab
		                 , gameObject.transform.position
		                 , gameObject.transform.rotation);
			timeToSpawn += SPAWN_TIMER;
		}
	}

	void OnCollisionEnter(Collision collider)
	{
		if (collider.gameObject.tag == "Ground")
		{
			gameObject.rigidbody.velocity = Vector3.zero;
		}
	}
}
