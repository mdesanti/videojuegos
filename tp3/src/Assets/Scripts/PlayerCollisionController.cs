using UnityEngine;
using System.Collections;

public class PlayerCollisionController : MonoBehaviour
{
	public ScoreManager scoreManager;
	
	void OnParticleCollision(GameObject collision) 
    {
		string tag = collision.tag;
		if(tag == "Fire") {
			scoreManager.playerGotBurnt();
			Application.LoadLevel ("GameOver");
		}
    }
}