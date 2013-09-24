using UnityEngine;
using System.Collections;

public abstract class AbstractCollisionController : MonoBehaviour
{

    void OnParticleCollision(GameObject collision) 
    {
		if(collision.tag == "Fire" && isDestroyedByFire()) {
			Destroy(gameObject);
			GameObject.Find("Extras Manager").GetComponent<ExtrasManager>().OnCubeDestroyed(gameObject.transform);
		}
    }
	
	public virtual bool isDestroyedByFire() {
		return false;
	}
}