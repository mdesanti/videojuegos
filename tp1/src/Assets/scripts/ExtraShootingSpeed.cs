using UnityEngine;

public class ExtraShootingSpeed : MonoBehaviour
{	

    void OnCollisionEnter(Collision collision) 
    {
		if(collision.collider.gameObject.tag == "Player") {
			GameObject.Find("Bullet").GetComponent<Bullet>().growthFactor *= (float)2;
			GameObject.Destroy(gameObject);
		}
    }		
}


