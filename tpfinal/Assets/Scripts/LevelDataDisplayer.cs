using UnityEngine;
using System.Collections;

public class LevelDataDisplayer : MonoBehaviour {
	
	public GUISkin skin;
	public GUIStyle style;
	public Texture heart;
	
	void OnGUI () {
		GUI.Label (new Rect (150,30,50,50),"Score: " + StatsManager.Instance.coins, style);
		int i = StatsManager.Instance.remainingLives;
		while(i > 0) {
			GUI.DrawTexture(new Rect(Screen.width-150 + i * 30,10,30,30), heart, ScaleMode.ScaleToFit, true, 0f);
			i--;
		}
	}
}