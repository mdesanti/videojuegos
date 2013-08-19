using UnityEngine;
using System.Collections;

// The RequireComponent attribute ensures a CharacterController is added to the
// GameObject whenever a PlayerController is added
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	public float speed = 13.0f;

	private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
	public Transform bulletPrototype;
	public GameObject[] bullets;

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
		//transform.localScale = new Vector3(transform.localScale.x + 0.1f, (float) transform.localScale.y + 0.1f, transform.localScale.z + 0.1f);
	}
	
	void loadGun() {
		//bullets = FindObjectsOfType(Bullet);
		//GameObject bullets = GameObject.FindWithTag("Bullet");
		bullets =  GameObject.FindGameObjectsWithTag ("Bullet");
		if (Input.GetKeyDown(KeyCode.Space) && bullets.Length < 2) {
            // Copy the prefab. the returned type of Instantiate is allways the same 
            // type as the parameter, so it's safe to cast
            Transform bullet = (Transform)GameObject.Instantiate(bulletPrototype);

            //Set the bullet in it's initial position and rotation
            //bullet.position = transform.position;
			Vector3 move = new Vector3(transform.position.x, (float)(transform.position.y - 2), transform.position.z);
			bullet.position = move;
            bullet.transform.Rotate(0,0,0);
            //GameObject player = GameObject.FindWithTag("Player");
            //Physics.IgnoreCollision(bullet.collider, player.collider);
            Physics.IgnoreLayerCollision(8, 9); //layer 8 is Bullet layer, layer 9 is Player layer
            Debug.Log("Bullet Instanciated!!");

        }
	}
	
	void OnCollisionEnter(Collision collision) 
    {
        Debug.Log("Hit a " + collision.collider.gameObject.name);
		if(collision.collider.gameObject.tag == "Ball") {
			GameObject.Destroy(gameObject);
		}
    }
}