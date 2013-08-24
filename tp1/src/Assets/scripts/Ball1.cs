using UnityEngine;

// The RequireComponent attribute ensures a CharacterController is added to the
// GameObject whenever a PlayerController is added
public class Ball1 : MonoBehaviour
{	
    void Start()
    {
        int ballLayer = LayerMask.NameToLayer("Ball");
        Physics.IgnoreLayerCollision(ballLayer, ballLayer);
    }

    void OnCollisionEnter(Collision collision) 
    {
		if(collision.collider.gameObject.tag == "Bullet") {
			GameObject.Destroy(gameObject);
		}
    }
}
