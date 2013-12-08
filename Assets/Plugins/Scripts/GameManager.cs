using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public PlayerScore[] Scores;
	public int MaxScore = 10;

	private bool paused = false;
	private bool gameOver = false;
	private int winner = -1;

	public Texture PauseTexture;
	public Texture[] WinTextures;

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

		for(int p=0; p<Scores.Length; p++) {
			PlayerScore score = Scores[p];
			if(score.Value >= MaxScore) {
				winner = p;
				Invoke("GameOver", 2); 
				gameOver = true;
			}
		}
	}

	public void GameOver() {
		Time.timeScale = 0F;
	}

	void OnGUI() {
		if (paused)
			GUI.DrawTexture(new Rect(Screen.width / 2 - PauseTexture.width / 2, 10, PauseTexture.width , PauseTexture.height), PauseTexture, ScaleMode.StretchToFill, true, 10.0F);

		if(gameOver) {
			Texture winTexture = WinTextures[winner];
			GUI.DrawTexture(new Rect(Screen.width / 2 - winTexture.width / 2, Screen.height / 2 - winTexture.height / 2, winTexture.width , winTexture.height), winTexture, ScaleMode.StretchToFill, true, 10.0F);
		}

		if (Scores.Length > 0)
			GUI.Label(new Rect(10, 10, 100, 30), "Player 1: " + Scores[0].Value.ToString());

		if (Scores.Length > 1)
			GUI.Label(new Rect(Screen.width - 110, 10, 100, 30), "Player 2: " + Scores[1].Value.ToString());

		if (Scores.Length > 2)
			GUI.Label(new Rect(Screen.width - 110, Screen.height - 40, 100, 30), "Player 3: " + Scores[2].Value.ToString());

		if (Scores.Length > 3)
			GUI.Label(new Rect(10, Screen.height - 40, 100, 30), "Player 4: " + Scores[3].Value.ToString());
	}
}
