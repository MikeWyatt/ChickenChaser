using UnityEngine;
using System.Collections.Generic;

public class MoveWaypoints : MonoBehaviour {
	public float speed = 5.0f;
	public float reachedDestDist = 0.2f;

	Queue<Vector3> waypoints;
	Vector3 destination;
	Vector3 direction;

	void Awake()
	{
		waypoints = new Queue<Vector3>();
		destination = Vector3.zero;
	}

	void Start()
	{
		direction = destination - transform.position;
		direction.Normalize();
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
			if (distToDest.magnitude <= reachedDestDist)
			{
				//destination reached
				destination = Vector3.zero;
				Debug.Log("Destination Reached");
			}
			else
			{
				Debug.Log ("Dist to dest: " + distToDest.magnitude.ToString());
			}
		}
		else if (waypoints.Count > 0)
		{
			destination = waypoints.Dequeue();
			direction = destination - transform.position;
			direction.Normalize();
		}
	}
}
