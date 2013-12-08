using UnityEngine;
using System.Collections;

public class SpawnBehavior : ChickenBehavior {
	const float INIT_DISTANCE = 2.0f;

	MoveWaypoints mover;

	void Awake()
	{
		mover.Clear();
		mover = gameObject.GetComponent<MoveWaypoints>();

		Vector3 startMove = new Vector3(Random.Range(-10, 10)
		                            , Random.Range(-10, 10)
		                            , Random.Range (-10, 10));
		startMove.Normalize();
		startMove *= INIT_DISTANCE;
		mover.AddWayPoint(startMove);
		for (int i = 0; i < 3; i ++)
		{
			Vector3 newMove = Quaternion.Euler(0, Random.Range(-45, 45), 0) * startMove;
			mover.AddWayPoint(newMove);
		}
	}
}
