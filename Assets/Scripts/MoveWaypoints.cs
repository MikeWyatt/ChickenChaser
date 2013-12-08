using UnityEngine;
using System.Collections.Generic;

public class MoveWaypoints : MonoBehaviour {
	public float speed = 5.0f;

	Queue<Vector3> waypoints;
	Vector3 destination;
	Vector3 direction;

	void Start()
	{
		destination = Vector3.zero;
		direction = destination - transform.position;
	}

	public void AddWayPoint(Vector3 waypoint)
	{
		waypoints.Enqueue(waypoint);
	}

	public void Clear()
	{
		waypoints.Clear();
	}

	//public void enable();
	//public void disable();
	// Update is called once per frame
	void Update () {
		if (destination.magnitude > 0)
		{
			transform.position += direction * speed * Time.deltaTime;
			Vector3 distToDest = destination - gameObject.transform.position;
			if (distToDest.magnitude <= 0.1)
			{
				//destination reached
				destination = Vector3.zero;
			}
		}
		else if (waypoints.Count > 0)
		{
			destination = waypoints.Dequeue();
			direction = destination - transform.position;
		}
	}
}
