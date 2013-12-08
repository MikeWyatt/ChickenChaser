using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public int NumPlayers = 4;
	private List<int> playerScores;

	private bool paused = false;
	
	public Texture PauseTexture;

	// Use this for initialization
void Start () {
		playerScores = new List<int>();
		for (int i = 0; i < NumPlayers; i ++)
		{
			playerScores.Add(0);
		}
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

	public int GetScore(int playerNum)
	{
		if (NumPlayers >= playerNum)
			return playerScores[playerNum - 1];
		return -1;
	}

	public void AddScore(int playerNum, int deltaScore)
	{
		if (NumPlayers >= playerNum)
			playerScores[playerNum - 1] = playerScores[playerNum - 1] + deltaScore;
	}

	void OnGUI() {
		if (paused)
			GUI.DrawTexture(new Rect(Screen.width / 2 - PauseTexture.width / 2, 10, PauseTexture.width , PauseTexture.height), PauseTexture, ScaleMode.StretchToFill, true, 10.0F);

		if (NumPlayers > 0)
			GUI.Label(new Rect(10, 10, 100, 30), "Player 1: " + playerScores[0].ToString());

		if (NumPlayers > 1)
			GUI.Label(new Rect(10, Screen.height - 40, 100, 30), "Player 2: " + playerScores[1].ToString());

		if (NumPlayers > 2)
			GUI.Label(new Rect(Screen.width - 110, 10, 100, 30), "Player 3: " + playerScores[2].ToString());

		if (NumPlayers > 3)
			GUI.Label(new Rect(Screen.width - 110, Screen.height - 40, 100, 30), "Player 4: " + playerScores[3].ToString());
	}
}
