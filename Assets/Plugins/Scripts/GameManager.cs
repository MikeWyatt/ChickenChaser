using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public PlayerScore[] Scores;

	private bool paused = false;
	
	public Texture PauseTexture;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("P1Pause") ||
		    Input.GetButtonDown ("P2Pause")||
		    Input.GetButtonDown ("P3Pause")||
		    Input.GetButtonDown ("P4Pause"))
		{
			if (Time.timeScale == 1.0F)
			{
				Time.timeScale = 0F;
				paused = true;
			}
			else
			{
				Time.timeScale = 1.0F;
				paused = false;
			}
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
		}
	}

	void OnGUI() {
		if (paused)
			GUI.DrawTexture(new Rect(Screen.width / 2 - PauseTexture.width / 2, 10, PauseTexture.width , PauseTexture.height), PauseTexture, ScaleMode.StretchToFill, true, 10.0F);

		if (Scores.Length > 0)
			GUI.Label(new Rect(10, 10, 100, 30), "Player 0: " + Scores[0].Value.ToString());

		if (Scores.Length > 1)
			GUI.Label(new Rect(Screen.width - 110, 10, 100, 30), "Player 1: " + Scores[1].Value.ToString());

		if (Scores.Length > 2)
			GUI.Label(new Rect(Screen.width - 110, Screen.height - 40, 100, 30), "Player 2: " + Scores[2].Value.ToString());

		if (Scores.Length > 3)
			GUI.Label(new Rect(10, Screen.height - 40, 100, 30), "Player 3: " + Scores[3].Value.ToString());
	}
}
