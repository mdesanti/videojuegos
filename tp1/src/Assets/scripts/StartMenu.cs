using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	

	void OnGUI () {
		GUI.Box(new Rect(Screen.width/2-325,30,650,500),"");
		if (GUI.Button (new Rect (Screen.width/2-70,230,150,50), "Start")) {
			PlayerPrefs.SetInt("Score",0);
			Application.LoadLevel("level1");
		}
		if (GUI.Button (new Rect (Screen.width/2-70,330,150,50), "Exit")) {
			 Application.Quit();
		}
	}
}