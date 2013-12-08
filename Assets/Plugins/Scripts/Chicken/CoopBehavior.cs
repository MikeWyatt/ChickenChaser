using UnityEngine;
using System.Collections;

public class CoopBehavior : MonoBehaviour, IChickenBehavior {
	const float END_DISTANCE = 5.0f;
	const float START_DISTANCE = 2.0f;

	MoveWaypoints mover;

	void Start()
	{
		gameObject.collider.enabled = false;
		gameObject.rigidbody.useGravity = false;
		mover = gameObject.GetComponent<MoveWaypoints>();
		mover.Clear();

		float curDistance = START_DISTANCE;
		int numWaypoints = Random.Range(3,6);
		float deltaDistance = (END_DISTANCE - START_DISTANCE) / (float)numWaypoints;

		Vector3 curMove;

		for (int i = 0; i < numWaypoints; i ++)
		{
			curMove = new Vector3(Random.Range(-1.0f, 1.0f)
		                                , 0
		                                , Random.Range (-1.0f, 1.0f));
			curMove.Normalize();
			curMove *= curDistance;
			curDistance += deltaDistance;

			mover.AddWayPoint(transform.position + curMove);
		}
	}

	void Update()
	{
		if (mover.finished)
		{
			collider.enabled = true;
			gameObject.rigidbody.useGravity = true;
			this.enabled = false;
			Chicken parent = gameObject.GetComponent<Chicken>();
			parent.ChangeBehavior<RunningBehavior>();
		}
	}
}
