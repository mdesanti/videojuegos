using UnityEngine;

public class ExtrasManager : MonoBehaviour
{	
	public Transform extraSpeed;
	public Transform extraBomb;


	public void OnCubeDestroyed(Transform position) {
		float rand = Random.value;
		if(rand >= 0.10) {
			return;
		}
		rand = Random.value;
		Transform prototype = null;
		if(rand < 0.5) {
			prototype = extraSpeed;
		} else {
			prototype = extraBomb;
		}
		Transform extra = (Transform)GameObject.Instantiate(prototype);
		extra.position = position.position;
	}

}