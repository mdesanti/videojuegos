using UnityEngine;
using System.Collections;

public abstract class AbstractExtrasController : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) 
    {
		if(collision.collider.gameObject.tag == "Player") {
			perform(collision.gameObject);
			Destroy(gameObject);
		}
    }
	
	public abstract void perform(GameObject playerGameObject);
}