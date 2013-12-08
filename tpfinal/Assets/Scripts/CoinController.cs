using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour
{
	void OnTriggerEnter(Collider collider) 
    {
		StatsManager.Instance.OnCoinCollected();
		Destroy(gameObject);
    }
}