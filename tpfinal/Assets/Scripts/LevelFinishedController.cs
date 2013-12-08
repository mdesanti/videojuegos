using UnityEngine;
using System.Collections;

public class LevelFinishedController : MonoBehaviour {
	
	public GUISkin skin;
	public GUIStyle style;
	
	void OnGUI () {
		GUI.skin = skin;
		GUI.Label (new Rect (Screen.width/2-100,150,200,50),"Score: " + StatsManager.Instance.coins, style);
		if (GUI.Button (new Rect (Screen.width/2-100,230,200,50), "Play Again!")) {
			Application.LoadLevel("StartMenu");
		}
	}
}
