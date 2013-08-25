using UnityEngine;

// The RequireComponent attribute ensures a CharacterController is added to the
// GameObject whenever a PlayerController is added
public class ExtrasManager : MonoBehaviour
{	
	public Transform extraSpeed;
	public Transform extraShootingSpeed;
	
	public void OnBallDestroyed(Transform position) {
		float rand = Random.value;
		if(rand >= 1) {
			return;
		}
		rand = Random.value;
		Transform prototype = null;
		if(rand < 0.5) {
			prototype = extraSpeed;
		} else {
			prototype = extraShootingSpeed;
		}
		Transform extra = (Transform)GameObject.Instantiate(prototype);
		extra.position = position.position;
	}
}

