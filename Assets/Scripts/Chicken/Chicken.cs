using UnityEngine;
using System.Collections;



public class Chicken : MonoBehaviour {
	/**
	const float CHANGE_DIRECTION_TIME = 1500; // in ms

	float changeDirectionTime;
	//ChickenState state;
	GameObject holder;
	GameObject spawner;

	ChickenBehavior behavior;
	ChickenState state;

	Vector3 direction;

	// Use this for initialization
	void Start () {
		state = ChickenState.Spawning;
	}

	// Update is called once per frame
	void Update () {
		switch (state)
		{
		case ChickenState.Spawning:
		case ChickenState.Thrown:
			if (!behavior.IsEnabled)
			{
				direction = transform.position - spawner.transform.position;
				Vector3 offset = new Vector3(Random.Range(-0.25, 0.25),
				                             0, Random.Range(-0.25, 0.25));
				direction += offset;
			}
			state = ChickenState.Running;
			break;

		}
	}

	private void changeBehavior(ChickenBehavior newBehavior)
	{
		curBehavior.disable();
		curBehavior = newBehavior;
		curBehavior.enable();
	}**/
}
