using UnityEngine;
using System.Collections;

public class PlayerCollisionController : MonoBehaviour
{
	void OnParticleCollision(GameObject collision) 
    {
		string tag = collision.tag;
		if(tag == "Fire") {
			Application.LoadLevel ("GameOver");
		}
    }
}