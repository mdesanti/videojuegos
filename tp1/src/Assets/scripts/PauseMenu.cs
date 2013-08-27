
using System;
using UnityEngine;
 
public class PauseMenu : MonoBehaviour
{
    bool paused = false;
	public GUIStyle style1;
	public GUIStyle style2;
 
    void Update()
    {
       if(Input.GetKeyDown("escape"))
         paused = togglePause();
    }
 
    void OnGUI(){
       if(paused){
       		GUI.Box(new Rect(Screen.width/2-325,30,650,500),"");	
			string score = PlayerPrefs.GetInt("Score").ToString();
			GUI.Label (new Rect (Screen.width/2-185,60,100,50),"Pause Menu", style1);	
			GUI.Label (new Rect (Screen.width/2-65,140,100,50),"Score: " + score, style2);	
	       	if(GUI.Button (new Rect (Screen.width/2-70,200,150,50), "Continue")){
	         	paused = togglePause();
			}
			if (GUI.Button (new Rect (Screen.width/2-70,290,150,50), "Restart Game")) {
				paused = togglePause();
				PlayerPrefs.SetInt("Score",0);
				Application.LoadLevel("level1");
			}
			if (GUI.Button (new Rect (Screen.width/2-70,380,150,50), "Exit")) {
				paused = togglePause();
				Application.Quit();
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