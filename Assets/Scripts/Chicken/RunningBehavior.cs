using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RunningBehavior : MonoBehaviour, IChickenBehavior {
	public float Speed = 5f;
	public float EscapeRadius = 15f;
	public float PlayerAvoidanceWeight = 4f;		// how badly the chicken wants to avoid the players vs. reaching the fence
	public float PlayerAvoidanceMinRadius = 1.5f;	// should be chicken radius + ChickenScarer radius
	public float PlayerAvoidanceMaxRadius = 5f;		// distance at which chicken starts avoiding the ChickenScarer.

	void Update () {
		Vector3 deltaFromOrigin = (transform.position - FindObjectOfType<ChickenSpawner>().transform.position);

		// check if escaped
		if(deltaFromOrigin.magnitude >= EscapeRadius) {
			Destroy(gameObject);
			return;
		}

		// start desired vector so that chicken simply tries to leave the map
		Vector3 desired = deltaFromOrigin.normalized;

		// find nearby players
		IEnumerable<GameObject> nearbyPlayers = FindObjectsOfType<ChickenScarer>()
			.Select(pc => pc.gameObject);

		// add bias so chicken moves away from players.
		foreach(GameObject go in nearbyPlayers) {
			Vector3 delta = go.transform.position - transform.position;
			float weight = 1f - Mathf.Clamp01((delta.magnitude - PlayerAvoidanceMinRadius) / PlayerAvoidanceMaxRadius);
			float finalWeight = Mathf.Pow (weight, 4) * PlayerAvoidanceWeight;
			desired += -delta.normalized * finalWeight;
		}

		desired.Normalize();

		transform.position += desired * Speed * Time.deltaTime;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desired), 8 * Time.deltaTime);
	}
}
