using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	
	public GUISkin skin;
	
	void OnGUI () {
		//GUI.Box(new Rect(Screen.width/2-325,30,650,500),"");
		GUI.skin = skin;
		if (GUI.Button (new Rect (Screen.width/2-70,230,150,50), "Start")) {
			//AudioSource.PlayClipAtPoint(buttonClip , transform.position);
			Application.LoadLevel("level1");
		} else if (GUI.Button (new Rect (Screen.width/2-120,320,250,50), "Instructions")) {
			//AudioSource.PlayClipAtPoint(buttonClip , transform.position);
			Application.LoadLevel("instructions");
		} else if (GUI.Button (new Rect (Screen.width/2-120,410,250,50), "Power Ups")) {
			//AudioSource.PlayClipAtPoint(buttonClip , transform.position);
			Application.LoadLevel("powerups");
		} 
	}
}