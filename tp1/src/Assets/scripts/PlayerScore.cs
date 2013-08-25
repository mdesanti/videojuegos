using UnityEngine;

public class PlayerScore : MonoBehaviour {
	
	void OnGUI () {
		string score = PlayerPrefs.GetInt("Score").ToString();
		GUI.Label (new Rect (Screen.width/2,100,100,50),"Score: " + score);	
	}
}
