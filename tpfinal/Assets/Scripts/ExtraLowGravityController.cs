using UnityEngine;
using System.Collections;

public class ExtraLowGravityController : MonoBehaviour
{
	void OnTriggerEnter(Collider collider) 
    {
		//perform(collider.gameObject);
		ThirdPersonController.gravity -= 20;
		Destroy(gameObject);
    }
}