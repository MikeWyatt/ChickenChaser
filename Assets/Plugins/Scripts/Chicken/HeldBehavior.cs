using UnityEngine;
using System.Collections;

public class HeldBehavior : MonoBehaviour, IChickenBehavior {

	public GameObject Holder { get; set; }
	public float HoldHeight = 1f;

	// Update is called once per frame
	public void Update () 
	{
		transform.position = new Vector3(
			Holder.transform.position.x,
			HoldHeight,
			Holder.transform.position.z);
		transform.rotation = Holder.transform.rotation;
	}
}
