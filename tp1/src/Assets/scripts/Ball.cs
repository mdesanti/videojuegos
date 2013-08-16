using UnityEngine;

// The RequireComponent attribute ensures a CharacterController is added to the
// GameObject whenever a PlayerController is added
public class Ball : MonoBehaviour
{
    public float jumpSpeed = 15.0f;
	public float gravity = 20.0f;
	public Transform childBall;

    void Start()
    {
		//random + or - sign
		float y = Random.value;
		float x = Random.value;
		
        rigidbody.AddForce(x*1000,y*1000,0);
		Debug.Log("Instanciated!!");
    }

    void OnCollisionEnter(Collision collision) 
    {
        Debug.Log("Hit a " + collision.collider.gameObject.name);
		if(collision.collider.gameObject.tag == "Bullet") {
			if(childBall != null) {
				Transform ball1 = (Transform)GameObject.Instantiate(childBall);
				Transform ball2 = (Transform)GameObject.Instantiate(childBall);
				Vector3 move1 = new Vector3(transform.position.x + 5, (float)(transform.position.y), transform.position.z);
				Vector3 move2 = new Vector3(transform.position.x - 5, (float)(transform.position.y), transform.position.z);
				ball1.position = move1;
				ball2.position = move2;
				ball1.renderer.enabled = true;
				ball2.renderer.enabled = true;
			}
			GameObject.Destroy(gameObject);
		}
    }
}
