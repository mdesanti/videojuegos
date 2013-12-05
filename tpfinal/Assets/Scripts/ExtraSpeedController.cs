using UnityEngine;
using System.Collections;

public class ExtraSpeedController : MonoBehaviour
{
	void OnTriggerEnter(Collider collider) 
    {
		//perform(collider.gameObject);
		Destroy(gameObject);
    }
}