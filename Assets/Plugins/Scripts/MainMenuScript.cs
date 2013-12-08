using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void  OnGUI(){
		
		if (GUI.Button (new Rect (150, 130, 100, 50), "PLAY")){
			print ("This button should start the game!");
			Application.LoadLevel(1);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
