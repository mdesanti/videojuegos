using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	
	public AudioClip buttonClip;
	
	void OnGUI () {
		GUI.Box(new Rect(Screen.width/2-325,30,650,500),"");
		if (GUI.Button (new Rect (Screen.width/2-70,230,150,50), "Start")) {
			//AudioSource.PlayClipAtPoint(buttonClip , transform.position);
			Application.LoadLevel("LevelGenerator");
		}
	}
}