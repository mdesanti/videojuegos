using UnityEngine;
using System.Collections;

public class PowerUpsMenu : MonoBehaviour {
	
	public GUISkin skin;
	public GUIStyle style;
	public Texture extra_slow;
	public Texture extra_speed;
	public Texture low_gravity;
	
	void Start() {
		StatsManager.Instance.ResetStatistics();
	}
	
	void OnGUI () {
		GUI.Label (new Rect (100,300,50,50),"Extra Slow", style);
		GUI.DrawTexture(new Rect(140,350,30,30), extra_slow, ScaleMode.StretchToFill, true, 0f);
		
		GUI.Label (new Rect (300,300,50,50),"Extra Speed", style);
		GUI.DrawTexture(new Rect(350,350,30,30), extra_speed, ScaleMode.StretchToFill, true, 0f);
		
		GUI.Label (new Rect (500,300,50,50),"Lower Gravity", style);
		GUI.DrawTexture(new Rect(560,350,30,30), low_gravity, ScaleMode.StretchToFill, true, 0f);
	}
}