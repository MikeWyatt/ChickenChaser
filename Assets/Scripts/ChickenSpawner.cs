using UnityEngine;
using System.Collections;

public class ChickenSpawner : MonoBehaviour {
	public GameObject chickenPrefab;

	const float SPAWN_TIMER = 5000; // in ms
	float timeToSpawn;
	// Use this for initialization
	void Start () {
		timeToSpawn = SPAWN_TIMER;
	}
	
	// Update is called once per frame
	void Update () {
		timeToSpawn -= Time.deltaTime;
		if (timeToSpawn < 0)
		{
			timeToSpawn += timeToSpawn;
			Instantiate(chickenPrefab);
		}
	}
}
