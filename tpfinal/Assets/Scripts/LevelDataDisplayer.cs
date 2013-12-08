using UnityEngine;
using System.Collections;

public class LevelDataDisplayer : MonoBehaviour {
	
	public GUISkin skin;
	public GUIStyle style;
	
	void OnGUI () {
		GUI.Label (new Rect (100,30,50,50),"Score: " + StatsManager.Instance.coins, style);	
	}
}