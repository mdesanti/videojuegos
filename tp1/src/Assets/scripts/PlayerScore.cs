using UnityEngine;

public class PlayerScore : MonoBehaviour {
	
	public GUIStyle style;
	
	void OnGUI () {
		string score = PlayerPrefs.GetInt("Score").ToString();
		GUI.Label (new Rect (Screen.width/2-65,140,100,50),"Score: " + score, style);	
	}
}
