using UnityEngine;

// The RequireComponent attribute ensures a CharacterController is added to the
// GameObject whenever a PlayerController is added
[RequireComponent(typeof(CharacterController))]
public class Ball : MonoBehaviour
{
    public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;

	private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        // Calling GetComponent on an Update or FixedUpdate is a performance No-No
        // so we store the CharacterController in a property
        // here GetComponent can never be null, because of RequireComponent(...)
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate() {

        //Ask the controller if we are in mid air, if not, then act
		if (controller.isGrounded) {
			moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection.Normalize();
			moveDirection.y = jumpSpeed;
		}

		moveDirection.y -= gravity * Time.deltaTime;

        //Just tell the controller where we want to move, it will handle collisions itself
		controller.Move(moveDirection * Time.deltaTime);
	}
}
