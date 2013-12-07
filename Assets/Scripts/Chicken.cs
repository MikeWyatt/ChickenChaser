using UnityEngine;
using System.Collections;

public enum ChickenState
{
	Spawning,
	Running,
	Held,
	Thrown,
	Escaping
}

public class Chicken : MonoBehaviour {
	const int CHANGE_DIRECTION_TIME = 1500; // in ms

	int changeDirectionTime;
	ChickenState state;
	GameObject holder;

	Vector3 curDestination;

	// Use this for initialization
	void Start () {
		state = ChickenState.Spawning;
		holder = null;
		changeDirectionTime = CHANGE_DIRECTION_TIME;
	}
	
	// Update is called once per frame
	void Update () {
		switch(state)
		{
		case ChickenState.Escaping:
			UpdateEscaping();
			break;
		case ChickenState.Held:
			UpdateHeld();
			break;
		case ChickenState.Running:
			UpdateRunning();
			break;
		case ChickenState.Spawning:
			UpdateSpawning();
			break;
		case ChickenState.Thrown:
			UpdateThrown();
			break;
		}
	}


	void UpdateSpawning()
	{

	}

	void UpdateRunning()
	{
		changeDirectionTime -= Time.deltaTime;
		if (changeDirectionTime < -
	}

	void UpdateHeld()
	{

	}

	void UpdateThrown()
	{

	}

	void UpdateEscaping()
	{

	}
}
