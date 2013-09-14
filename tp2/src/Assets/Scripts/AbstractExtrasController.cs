using UnityEngine;
using System.Collections;

public abstract class AbstractExtrasController : MonoBehaviour
{
    void OnTriggerEnter(Collider collider) 
    {
		if(collider.gameObject.tag == "Player") {
			perform(collider.gameObject);
			Destroy(gameObject);
		}
    }
	
	public abstract void perform(GameObject playerGameObject);
}