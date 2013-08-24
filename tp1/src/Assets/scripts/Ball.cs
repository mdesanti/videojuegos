using UnityEngine;

// The RequireComponent attribute ensures a CharacterController is added to the
// GameObject whenever a PlayerController is added
public class Ball : MonoBehaviour
{	
	public Transform childBall;

    void Start()
    {
        int ballLayer = LayerMask.NameToLayer("Ball");
        Physics.IgnoreLayerCollision(ballLayer, ballLayer);
    }

    void OnCollisionEnter(Collision collision) 
    {
        //Debug.Log("Hit a " + collision.collider.gameObject.name);
		if(collision.collider.gameObject.tag == "Bullet") {
				Transform ball1 = (Transform)GameObject.Instantiate(childBall);
				Transform ball2 = (Transform)GameObject.Instantiate(childBall);
				ball1.position = transform.position;
				ball2.position = transform.position;
				ball1.renderer.enabled = true;
				ball2.renderer.enabled = true;
				ball1.rigidbody.AddForce(600,200,0);
				ball2.rigidbody.AddForce(-600,200,0);
				Physics.IgnoreCollision(ball1.collider, ball2.collider);
			GameObject.Destroy(gameObject);
		}
    }
}
