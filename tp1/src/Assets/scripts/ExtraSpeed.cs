using UnityEngine;

public class ExtraSpeed : MonoBehaviour
{	

    void OnCollisionEnter(Collision collision) 
    {
		if(collision.collider.gameObject.tag == "Player") {
			collision.collider.gameObject.GetComponent<PlayerController>().speed *= (float)2;
			GameObject.Destroy(gameObject);
		}
    }		
}

