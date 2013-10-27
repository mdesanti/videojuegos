using UnityEngine;
using System.Collections;

public class TimeController : MonoBehaviour {
	
	public float availableTime = 0f;
	public GUIStyle style;
	
	// Update is called once per frame
	void Update () {
		availableTime -= Time.deltaTime;
		if(availableTime < 0) {
			//loose!
		}
	}
	
	void OnGUI () {
		GUI.Label (new Rect (Screen.width/2-65 + 150,140,100,50),"Time: " + availableTime, style);	
	}
}