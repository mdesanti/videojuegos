using System;

using UnityEngine;
using System.Collections;

public class GliderController : MonoBehaviour {
	
	private double original_position;
	
	void Start() {
        original_position = this.gameObject.transform.position.y;
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
    }
}

