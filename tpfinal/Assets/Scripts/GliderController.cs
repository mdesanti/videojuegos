using System;

using UnityEngine;
using System.Collections;

public class GliderController : MonoBehaviour {
	
	void FixedUpdate() {
		double vertical_position = this.gameObject.transform.position.y;
		if(vertical_position < -50) {
			StatsManager.Instance.OnGliderDied();
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit collision) {
		
		if(collision.gameObject.tag == "SolidWall") {
			StatsManager.Instance.OnGliderDied();
		}

		if(collision.gameObject.tag == "Target") {
			Application.LoadLevel("LevelFinished");
		}
    }
}

