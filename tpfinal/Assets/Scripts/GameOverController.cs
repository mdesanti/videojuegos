using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {
	
	public GUISkin skin;
	public GUIStyle style;
	
	void OnGUI () {
		GUI.skin = skin;
		GUI.Label (new Rect (Screen.width/2-50,150,200,50),"Score: " + StatsManager.Instance.coins, style);
		if (GUI.Button (new Rect (Screen.width/2-100,230,200,50), "Play Again!")) {
			Application.LoadLevel("StartMenu");
		}
		GUI.enabled = StatsManager.Instance.CanBuyExtraLife();
		if (GUI.Button (new Rect (Screen.width/2-200,320,400,50), "Get extra life for 20 coins!")) {
			StatsManager.Instance.AddExtraLife();
		}
		GUI.enabled = true;
	}
}