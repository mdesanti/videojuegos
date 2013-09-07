using UnityEngine;
using System.Collections;

public abstract class AbstractExtrasController : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) 
    {
		Debug.Log("Extras Controller hit a: " + collision.collider.gameObject.tag);
		if(collision.collider.gameObject.tag == "Player") {
			perform(collision.gameObject);
			Destroy(gameObject);
		}
    }
	
	public abstract void perform(GameObject playerGameObject);
}