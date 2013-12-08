using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RunningBehavior : MonoBehaviour, IChickenBehavior {
	public float Speed = 5f;
	public float EscapeRadius = 15f;
	public float PlayerAvoidanceWeight = 4f;		// how badly the chicken wants to avoid the players vs. reaching the fence
	public float PlayerAvoidanceRadius = 3f;
	public GameObject Origin;	// fixed point chicken is running from, other than the players

	void Update () {
		Vector3 deltaFromOrigin = (transform.position - Origin.transform.position);

		// check if escaped
		if(deltaFromOrigin.magnitude >= EscapeRadius) {
			Destroy(gameObject);
			return;
		}

		// start desired vector so that chicken simply tries to leave the map
		Vector3 desired = deltaFromOrigin.normalized;

		// find nearby players
		IEnumerable<GameObject> nearbyPlayers = FindObjectsOfType<ChickenScarer>()
			.Select(pc => pc.gameObject)
			.Where(p => (p.transform.position - transform.position).magnitude <= PlayerAvoidanceRadius);

		// add bias so chicken moves away from players.
		foreach(GameObject go in nearbyPlayers) {
			desired += -(go.transform.position - transform.position).normalized * PlayerAvoidanceWeight;
		}

		desired.Normalize();

		transform.position += desired * Speed * Time.deltaTime;
		transform.rotation = Quaternion.LookRotation(desired);  // not working
	}
}
