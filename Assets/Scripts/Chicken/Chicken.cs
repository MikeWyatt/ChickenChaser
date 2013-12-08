using UnityEngine;
using System;
using System.Collections;


public class Chicken : MonoBehaviour {
	private IChickenBehavior currentBehavior;

	public void ChangeBehavior<T>() where T : MonoBehaviour, IChickenBehavior {
		if(currentBehavior != null) {
			currentBehavior.enabled = false;
		}
		currentBehavior = GetComponent<T>();
		if(currentBehavior == null) {
			throw new Exception("Behavior not found!");
		}
		currentBehavior.enabled = true;
	}
}
