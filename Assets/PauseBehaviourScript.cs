using UnityEngine;
using System.Collections;

public class PauseBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void  OnGUI(){
		
		if (GUI.Button (new Rect (50, 160, 150, 130), "RESUME")){ 
			print ("This button should start the game!");
			Application.LoadLevel(1);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
