using UnityEngine;
using System.Collections;

public class ExtraSlowController : MonoBehaviour
{
	void OnTriggerEnter(Collider collider) 
    {
		//perform(collider.gameObject);
		ThirdPersonController.walkSpeed -= 20;
		Destroy(gameObject);
    }
}