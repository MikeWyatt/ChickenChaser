using UnityEngine;
using System.Collections;
using System.Linq;

public class RunningBehavior : MonoBehaviour, IChickenBehavior {

	public GameObject[] EscapeZones;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	public Vector3 CalcEscapePoint() {
//		var options = EscapeZones.ToDictionary(
//			go => go,
//			go => go.collider.ClosestPointOnBounds(transform.position));
//
//		//options.OrderBy(kvp => 
////
//
//	}
}
