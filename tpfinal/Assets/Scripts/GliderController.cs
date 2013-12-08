using System;

using UnityEngine;
using System.Collections;

public class GliderController : MonoBehaviour {

	private float gravity = 0;
	private float walkSpeed = 0;

	void Start() {
		gravity = PlayerController.gravity;
		walkSpeed = PlayerController.walkSpeed;
	}
	
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

  	public void OnGliderReset() {
  		PlayerController.gravity = gravity;
  		PlayerController.walkSpeed = walkSpeed;
  	}
}

