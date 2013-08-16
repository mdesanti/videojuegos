using UnityEngine;
using System.Collections;

// The RequireComponent attribute ensures a CharacterController is added to the
// GameObject whenever a PlayerController is added
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	public float speed = 9.0f;

	private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
	public Transform bulletPrototype;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate() {
		loadGun();
    	//Ask the controller if we are in mid air, if not, then act
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        moveDirection.Normalize();
		moveDirection *= speed;

         if (moveDirection != Vector3.zero)
             transform.forward = Vector3.Slerp(transform.forward, moveDirection, 0.3f);

        //Just tell the controller where we want to move, it will handle collisions itself
		controller.Move(moveDirection * Time.deltaTime);
	}
	
	void loadGun() {
		if (Input.GetKeyDown(KeyCode.LeftControl)) {
            // Copy the prefab. the returned type of Instantiate is allways the same 
            // type as the parameter, so it's safe to cast
            Transform bullet = (Transform)GameObject.Instantiate(bulletPrototype);

            //Set the bullet in it's initial position and rotation
            bullet.position = transform.position*2;
            bullet.transform.Rotate(0,0,0);
        }
	}
}