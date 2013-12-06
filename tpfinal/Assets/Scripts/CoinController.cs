using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour
{
	void OnTriggerEnter(Collider collider) 
    {
		//perform(collider.gameObject);
		Destroy(gameObject);
    }
}