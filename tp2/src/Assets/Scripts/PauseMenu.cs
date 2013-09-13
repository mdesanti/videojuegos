
using System;
using UnityEngine;
 
public class PauseMenu : MonoBehaviour
{
    bool paused = false;
	public GUIStyle style;
	public AudioClip buttonClip;
	
    void Update()
    {
       if(Input.GetKeyDown("escape"))
         paused = togglePause();
    }
 
    void OnGUI(){
		
       if(paused){
       		GUI.Box(new Rect(Screen.width/2-325,30,650,500),"");	
			GUI.Label (new Rect (Screen.width/2-165,65,100,70),"Pause Menu", style);	
	       	if(GUI.Button (new Rect (Screen.width/2-70,200,150,50), "Continue")){
				AudioSource.PlayClipAtPoint(buttonClip , transform.position);
	         	paused = togglePause();
			}
			if (GUI.Button (new Rect (Screen.width/2-70,290,150,50), "Restart Game")) {
				AudioSource.PlayClipAtPoint(buttonClip , transform.position);
				paused = togglePause();
				Application.LoadLevel("Level1");
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