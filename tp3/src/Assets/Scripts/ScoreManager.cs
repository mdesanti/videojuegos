using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{
	public GUIStyle style;
	
	private static int axesLeft = 0;
	
	void Start() {
		PlayerPrefs.SetInt("Score", 0);
		PlayerPrefs.SetInt("Life", 100);
    }
	
	public void SetAxesLeft() {
		GameObject[] aux = GameObject.FindGameObjectsWithTag("Axe");
		axesLeft = aux.Length;
	}
	
	public void axeCollected() {
		int score = PlayerPrefs.GetInt("Score");
		PlayerPrefs.SetInt("Score", score + 10);
		axesLeft--;
		if(axesLeft == 0) {
			Application.LoadLevel("WinScene");
		}
	}
	
	public void playerGotBurnt() {
		int life = PlayerPrefs.GetInt("Life");
		if(life <= 0)
			Application.LoadLevel ("GameOver");
		else 
			PlayerPrefs.SetInt("Life", life - 5);
	}
	
	void OnGUI () {
		Debug.Log(axesLeft);
		string score = PlayerPrefs.GetInt("Score").ToString();
		string life = PlayerPrefs.GetInt("Life").ToString();
		GUI.Label (new Rect (Screen.width/2-65,140,100,50),"Score: " + score, style);	
		GUI.Label (new Rect (Screen.width/2-65 + 70,140,100,50),"Life: " + life, style);
		GUI.Label (new Rect (Screen.width/2-65 + 150,140,100,50),"Axes Left: " + axesLeft, style);
	}
	
}



