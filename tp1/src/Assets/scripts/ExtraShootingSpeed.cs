using UnityEngine;

public class ExtraShootingSpeed : MonoBehaviour
{	

    void OnCollisionEnter(Collision collision) 
    {
		if(collision.collider.gameObject.tag == "Player") {
			Debug.Log("Hit a player!");
			Bullet.growthFactor *= (float)1.5;
			GameObject.Destroy(gameObject);
		}
    }		
}


