using System;

using UnityEngine;
using System.Collections;

public class GliderController : MonoBehaviour {
	
	void Start() {
    }
	
	void FixedUpdate() {
		double vertical_position = this.gameObject.transform.position.y;
		if(vertical_position < -50) {
			Application.LoadLevel("gameover");
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit collision) {
		Debug.Log("Hit a" + collision.gameObject.tag);
		if(collision.gameObject.tag == "SolidWall") {
			Application.LoadLevel("GameOver");
		}

		if(collision.gameObject.tag == "Target") {
			Application.LoadLevel("LevelFinished");
		}
    }
}

