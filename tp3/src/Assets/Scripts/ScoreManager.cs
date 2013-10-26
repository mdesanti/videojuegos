using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{
	public GUIStyle style;
	
	void Start() {
		PlayerPrefs.SetInt("Score", 0);
		PlayerPrefs.SetInt("Life", 100);
    }
	
	public void axeCollected() {
		int score = PlayerPrefs.GetInt("Score");
		PlayerPrefs.SetInt("Score", score + 10);
	}
	
	public void poissonCollected() {
		int score = PlayerPrefs.GetInt("Life");
		PlayerPrefs.SetInt("Life", score - 40);
	}
	
	public void extraScore() {
		int score = PlayerPrefs.GetInt("Score");
		PlayerPrefs.SetInt("Score", score + 40);
	}
	
	void OnGUI () {
		string score = PlayerPrefs.GetInt("Score").ToString();
		string life = PlayerPrefs.GetInt("Life").ToString();
		GUI.Label (new Rect (Screen.width/2-65,140,100,50),"Score: " + score, style);	
		GUI.Label (new Rect (Screen.width/2-65,200,100,50),"Life: " + life, style);	
	}
	
}



