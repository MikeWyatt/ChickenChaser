using UnityEngine;
using System.Collections;

public class PauseMenuBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void  OnGUI(){
	
		if (GUI.Button (new Rect (10, 10, 150, 100), "I am a button.")){
			print ("You clicked the button!");
		}
		
		
		if (GUI.Button (new Rect (10, 50, 170, 130), "PLAY")){
			print ("This button should start the game!");
			Application.LoadLevel(1);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
