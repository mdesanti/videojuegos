using System;

using UnityEngine;
using System.Collections;

public class GliderController : MonoBehaviour {
	
	public int lives = 3;
	
	void Start() {
    }
	
	void FixedUpdate() {
		double vertical_position = this.gameObject.transform.position.y;
		if(vertical_position < -50) {
			Application.LoadLevel("gameover");
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit collision) {
		if(collision.gameObject.tag == "SolidWall") {
			if( lives == 0 ) {
				Application.LoadLevel("GameOver");
			} else {
				lives = lives -1;
				Application.LoadLevel(Application.loadedLevel);
			}
		}

		if(collision.gameObject.tag == "Target") {
			Application.LoadLevel("LevelFinished");
		}
    }
}

