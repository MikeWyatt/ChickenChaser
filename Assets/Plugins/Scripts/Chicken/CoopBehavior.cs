﻿using UnityEngine;
using System.Collections;

public class CoopBehavior : MonoBehaviour, IChickenBehavior {
	public float endDistance = 5.0f;
	public float startDistance = 2.0f;

	MoveWaypoints mover;

	void Start()
	{
		gameObject.collider.enabled = false;
		gameObject.rigidbody.useGravity = false;
		mover = gameObject.GetComponent<MoveWaypoints>();
		mover.Clear();

		float curDistance = startDistance;
		int numWaypoints = Random.Range(3,6);
		float deltaDistance = (endDistance - startDistance) / (float)numWaypoints;

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
