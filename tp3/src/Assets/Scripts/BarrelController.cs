using UnityEngine;
using System.Collections;

public class BarrelController : MonoBehaviour 
{
	public ScoreManager scoreManager;
	
	//Activate the Main function when player is near the door
	void OnCollisionEnter(Collision other)
	{
		Debug.Log("Barrel hit a " + other.collider.gameObject.tag);
		if (other.collider.gameObject.tag == "Player") 
		{
			if(Random.value < 0.3) {
				//is poisson
				scoreManager.poissonCollected();
			} else {
				//gives more score
			}
			Destroy(gameObject);
		}
	}
	
}


