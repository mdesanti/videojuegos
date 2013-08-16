using UnityEngine;

// The RequireComponent attribute ensures a CharacterController is added to the
// GameObject whenever a PlayerController is added
public class Ball : MonoBehaviour
{
    public float jumpSpeed = 15.0f;
	public float gravity = 20.0f;

	private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        rigidbody.AddForce(1000,-1000,0);
    }
	
	void OnCollisionEnter(Collision collision) 
    {
        Debug.Log("Hit a " + collision.collider.gameObject.name);
    }

    void FixedUpdate() {

        //Ask the controller if we are in mid air, if not, then act
		//if (controller.isGrounded) {
		//	moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        //    moveDirection.Normalize();
		//	moveDirection.y = jumpSpeed;
		//}

		//moveDirection.y -= gravity * Time.deltaTime;
		//moveDirection.x = 50;

        //Just tell the controller where we want to move, it will handle collisions itself
		//controller.Move(moveDirection * Time.deltaTime);
	}
}
