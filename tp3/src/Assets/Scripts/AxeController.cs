using UnityEngine;
using System.Collections;

public class AxeController : MonoBehaviour 
{
	public ScoreManager scoreManager;
	
	//Activate the Main function when player is near the door
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			scoreManager.axeCollected();
			Destroy(gameObject);
		}
	}
	
}


