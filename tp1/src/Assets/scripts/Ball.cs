using UnityEngine;

// The RequireComponent attribute ensures a CharacterController is added to the
// GameObject whenever a PlayerController is added
public class Ball : MonoBehaviour
{
    //public float jumpSpeed = 15.0f;
	//public float gravity = 20.0f;
	
	public Transform childBall;

    void Start()
    {
		//random + or - sign
		//float y = Random.value;
		//float x = Random.value;
		
        rigidbody.AddForce(500,200,0);
		Debug.Log("Ball Instanciated!!");
    }

    void OnCollisionEnter(Collision collision) 
    {
        //Debug.Log("Hit a " + collision.collider.gameObject.name);
		if(collision.collider.gameObject.tag == "Bullet") {
			if(childBall != null) {
				Transform ball1 = (Transform)GameObject.Instantiate(childBall);
				Transform ball2 = (Transform)GameObject.Instantiate(childBall);
				ball1.position = transform.position;
				ball2.position = transform.position;
				ball1.renderer.enabled = true;
				ball2.renderer.enabled = true;
				ball2.rigidbody.AddForce(-1000,0,0);
				Physics.IgnoreCollision(ball1.collider, ball2.collider);

			}
			GameObject.Destroy(gameObject);
		}
		//if(collision.collider.gameObject.tag == "Ball"){
		//	collision.collider.enabled = false;
		//}
    }
}
