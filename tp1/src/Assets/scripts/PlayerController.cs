using UnityEngine;

// The RequireComponent attribute ensures a CharacterController is added to the
// GameObject whenever a PlayerController is added
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	public float speed = 9.0f;

	private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate() {

    	//Ask the controller if we are in mid air, if not, then act
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection.Normalize();
		moveDirection *= speed;

         if (moveDirection != Vector3.zero)
             transform.forward = Vector3.Slerp(transform.forward, moveDirection, 0.3f);

        //Just tell the controller where we want to move, it will handle collisions itself
		controller.Move(moveDirection * Time.deltaTime);
	}
}