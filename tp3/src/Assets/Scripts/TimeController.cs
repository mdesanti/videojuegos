using UnityEngine;
using System.Collections;

public class TimeController : MonoBehaviour {
	
	private static float availableTime;
	private static bool started = false;
	public GUIStyle style;

	void Update () {
		if(!started) {
			return;
		}
		availableTime -= Time.deltaTime;
		if(availableTime < 0) {
			Application.LoadLevel ("GameOver");
		}
	}
	
	void OnGUI () {
		GUI.Label (new Rect (Screen.width/2-65 + 250,140,100,50),"Time: " + availableTime, style);	
	}
	
	public void SetTime() {
		GameObject[] aux = GameObject.FindGameObjectsWithTag("Floor");
		string diff = PlayerPrefs.GetString("difficult");
		bool difficult = bool.Parse(diff);
		float factor = 1.1f;
		if(difficult) {
			factor = 0.8f;
		}
		availableTime = aux.Length*factor;
		started = true;
	}
	
}