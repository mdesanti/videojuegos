using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	void OnGUI () {
		if (GUI.Button (new Rect (Screen.width/2-50,150,100,50), "Start")) {
			Application.LoadLevel ("InitialScene");
		}
		if (GUI.Button (new Rect (Screen.width/2-50,230,100,50), "Exit")) {
			 Application.Quit();
		}
	}
}