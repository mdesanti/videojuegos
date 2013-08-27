using UnityEngine;
using System.Collections;

// The RequireComponent attribute ensures a CharacterController is added to the
// GameObject whenever a PlayerController is added
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	public static float STARTING_SPEED = 50.0f;
	public float speed = STARTING_SPEED;
	
	private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
	public Transform bulletPrototype;
	public GameObject[] bullets;
	public int points = 0;
	public float score;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate() {
		loadGun();
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        moveDirection.Normalize();
		moveDirection *= speed;

         if (moveDirection != Vector3.zero)
             transform.forward = Vector3.Slerp(transform.forward, moveDirection, 0.3f);

        //Just tell the controller where we want to move, it will handle collisions itself
		controller.Move(moveDirection * Time.deltaTime);
	}
	
	void loadGun() {
		bullets =  GameObject.FindGameObjectsWithTag ("Bullet");
		//if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyUp(KeyCode.Space)) && bullets.Length < 1) {
		if (Input.GetKey(KeyCode.Space) && bullets.Length < 1) {
            Transform bullet = (Transform)GameObject.Instantiate(bulletPrototype);

            //Set the bullet in it's initial position and rotation
			Vector3 move = new Vector3(transform.position.x, (transform.position.y) + 1, 0);
			bullet.position = move;
            bullet.transform.Rotate(0,0,0);
            int bulletLayer = LayerMask.NameToLayer("Bullet");
			int playerLayer = LayerMask.NameToLayer("Player");
            Physics.IgnoreLayerCollision(bulletLayer, playerLayer);

        }
	}
	
	void OnCollisionEnter(Collision collision) 
    {
		if(collision.collider.gameObject.tag == "Ball") {
			Application.LoadLevel ("GameOver");
		}
    }
}