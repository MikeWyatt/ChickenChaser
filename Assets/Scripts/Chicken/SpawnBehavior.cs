using UnityEngine;
using System.Collections;

public class SpawnBehavior : MonoBehaviour, IChickenBehavior {
	const float INIT_DISTANCE = 2.0f;

	MoveWaypoints mover;

	void Start()
	{
		mover = gameObject.GetComponent<MoveWaypoints>();
		mover.Clear();

		Vector3 startMove = new Vector3(Random.Range(-10, 10)
		                            , 0
		                            , Random.Range (-10, 10));
		startMove.Normalize();
		startMove *= INIT_DISTANCE;
		Vector3 newPos = transform.position + startMove;
		mover.AddWayPoint(newPos);
		for (int i = 0; i < 3; i ++)
		{
			Vector3 newMove = Quaternion.Euler(0, Random.Range(-45, 45), 0) * startMove;
			newPos += newMove;
			mover.AddWayPoint(newPos);
		}
	}
}
