using UnityEngine;

public class ExtrasManager : MonoBehaviour
{	
	public Transform extraSpeed;
	public Transform extraBomb;
	public Transform extraExplosion;


	public void OnCubeDestroyed(Transform position) {
		float rand = Random.value;
		if(rand >= 0.15) {
			return;
		}
		rand = Random.value;
		Transform prototype = null;
		if(rand < 0.33) {
			prototype = extraSpeed;
		} else if (rand < 0.66) {
			prototype = extraBomb;
		} else {
			prototype = extraExplosion;
		}
		Transform extra = (Transform)GameObject.Instantiate(prototype);
		extra.position = position.position;
	}

}