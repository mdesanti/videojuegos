using UnityEngine;

// The RequireComponent attribute ensures a CharacterController is added to the
// GameObject whenever a PlayerController is added
public class ExtrasManager : MonoBehaviour
{	
	public Transform extraSpeed;
	public Transform extraShootingSpeed;
	
	private bool extraSpeedEnabled = false;
	private bool extraShootingSpeedEnabled = false;
	
	private static float MAX_TIME = 10f;
	
	private float extraSpeedTime = 0;
	private float extraShootingSpeedTime = 0;
	
	public void OnBallDestroyed(Transform position) {
		float rand = Random.value;
		if(rand >= 1) {
			return;
		}
		rand = Random.value;
		Transform prototype = null;
		if(rand < 0.5) {
			prototype = extraSpeed;
			extraSpeedEnabled = true;
			Debug.Log("Created an extra speed");
		} else {
			prototype = extraShootingSpeed;
			extraShootingSpeedEnabled = true;
			Debug.Log("Created an extra shooting speed");
		}
		Transform extra = (Transform)GameObject.Instantiate(prototype);
		extra.position = position.position;
	}
	
	void FixedUpdate() {
		updateSpeed();
		updateShootingSpeed();
	}
	
	void updateSpeed() {
		if(!extraSpeedEnabled) {
			return;
		}
		extraSpeedTime += Time.deltaTime;
		if(extraSpeedTime > MAX_TIME) {
			extraSpeedTime = 0;
			collision.collider.gameObject.GetComponent<PlayerController>().speed /= (float)2;
			extraSpeedEnabled = false;
		}
	}
	
	void updateShootingSpeed() {
		if(!extraShootingSpeedEnabled) {
			return;
		}
		extraShootingSpeedTime += Time.deltaTime;
		if(extraShootingSpeedTime > MAX_TIME) {
			extraShootingSpeedTime = 0;
			Bullet.growthFactor /= (float)1.5;
			extraShootingSpeedEnabled = false;
		}
	}
}

