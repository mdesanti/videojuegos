using UnityEngine;
using System.Collections;

public class Instructions : MonoBehaviour {
	
	public GUISkin skin;
	
	void OnGUI () {
		//GUI.Box(new Rect(Screen.width/2-325,30,650,500),"");
		GUI.skin = skin;
		if (GUI.Button (new Rect (Screen.width/2-130,500,250,50), "Back to menu")) {
			//AudioSource.PlayClipAtPoint(buttonClip , transform.position);
			Application.LoadLevel("StartMenu");
		}
	}
}