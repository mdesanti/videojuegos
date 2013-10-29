using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	
	public AudioClip buttonClip;
	
	private string width = "400";
	private string height = "400";
	private string seed = "1234";
	bool difficult = false;
	
	void OnGUI () {
		GUI.Box(new Rect(Screen.width/2-325,30,650,500),"");
		
		seed = GUI.TextField (new Rect(Screen.width/2-70,130,150,20), seed, 100);
		width = GUI.TextField (new Rect(Screen.width/2-70,160,150,20), width, 100);
		height = GUI.TextField (new Rect(Screen.width/2-70,190,150,20), height, 100);
		difficult = GUI.Toggle(new Rect(Screen.width/2-70,220,150,20), difficult, "Dificil?");
	
		
		if (GUI.Button (new Rect (Screen.width/2-70,250,150,50), "Start")) {
			//AudioSource.PlayClipAtPoint(buttonClip , transform.position);
			PlayerPrefs.SetInt("Score",0);
			PlayerPrefs.SetInt("Life", 100);
			PlayerPrefs.SetInt("width", int.Parse(width));
			PlayerPrefs.SetInt("height", int.Parse(height));
			PlayerPrefs.SetInt("seed", int.Parse(seed));
			if(difficult) {
				PlayerPrefs.SetString ("difficult", "true");
			} else {
				PlayerPrefs.SetString ("difficult", "false");
			}
			GUILayout.Label ("Dungeon settings");
			Application.LoadLevel("LevelGenerator");
		}
	}
}