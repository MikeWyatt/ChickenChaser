using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RunningBehavior : MonoBehaviour, IChickenBehavior {
	public float Speed = 5f;

	private GameObject[] escapeZones;
	public string EscapeZoneTag = "Escape Zone";

	void Start () {
		escapeZones = FindObjectsOfType<Collider>()
			.Select(c => c.gameObject)
			.Where(go => go.CompareTag(EscapeZoneTag))
			.ToArray();
//
//		Debug.Log(string.Format ("all colliders: {0}", FindObjectsOfType<Collider>().Select ()));
		Debug.Log (escapeZones);
		//		//EscapeZoneTag
//
//		if(EscapeZones.Count() == 0) {
//			throw new UnityException("Needs at least one escape zone!");
//		}
	}
	/*
	void Update () {
		Vector3 escapePoint = CalcEscapePoint();
//		Debug.Log (string.Format ("escapePoint = {0}", escapePoint));

		var delta = escapePoint - transform.position;
		delta.Normalize();
		transform.position += delta * Speed * Time.deltaTime;
		transform.LookAt(escapePoint);
	}

	public Vector3 CalcEscapePoint() {
		var options = escapeZones.ToDictionary(
			go => go,
			go => go.collider.ClosestPointOnBounds(transform.position));

		KeyValuePair<GameObject, Vector3> closest = options.OrderBy(kvp => (transform.position - kvp.Value).sqrMagnitude).First();
		return closest.Value;
	}

	public void OnCollisionEnter(Collision collision) {
		//if(!EscapeZones.Contains (collision.gameObject)) {
		if(!collision.gameObject.CompareTag(EscapeZoneTag)) {
			return;
		}
		//GetComponent<Chicken>().ChangeBehavior<RunningBehavior>();
		Destroy(gameObject);
	}
	*/
}
