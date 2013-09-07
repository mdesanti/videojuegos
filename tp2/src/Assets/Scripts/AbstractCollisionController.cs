using UnityEngine;
using System.Collections;

public abstract class AbstractCollisionController : MonoBehaviour
{
    void OnParticleCollision(GameObject collision) 
    {
		if(collision.tag == "Fire" && isDestroyedByFire()) {
			Destroy(gameObject);
		}
    }
	
	public virtual bool isDestroyedByFire() {
		return false;
	}
}