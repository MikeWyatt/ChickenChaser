using UnityEngine;
using System.Collections;

public class HeldBehavior : ChickenBehavior {

	public GameObject Holder { get; set; }

	// Update is called once per frame
	public void Update () 
	{
		const float heldY = 3;
		transform.position = new Vector3(
			Holder.transform.position.x,
			heldY,
			Holder.transform.position.z);
	}
}
