
using System;
using UnityEngine;
 
public class PauseMenu : MonoBehaviour
{
    bool paused = false;
	public GUIStyle style;
	public GUISkin skin;
	public AudioClip buttonClip;
	
    void Update()
    {
       if(Input.GetKeyDown("escape"))
         paused = togglePause();
    }
 
    void OnGUI(){
	   GUI.skin = skin;
		
	   if(paused){
			GUI.Label (new Rect (Screen.width/2-165,65,100,70),"Pause Menu", style);	
	       	if(GUI.Button (new Rect (Screen.width/2-70,200,150,50), "Continue")){
	         	paused = togglePause();
			}
			if (GUI.Button (new Rect (Screen.width/2-125,290,250,50), "Main Menu")) {
				paused = togglePause();
				Application.LoadLevel("startmenu");
			}
		
	   }
    }
 
	
    bool togglePause(){
       if(Time.timeScale == 0f){
         Time.timeScale = 1f;
         return(false);
       }else{
         Time.timeScale = 0f;
         return(true);    
       }
    }
}