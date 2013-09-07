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
	public Transform bombPrototype;
	public int bombCount = 0;
	public int points = 0;
	public float score;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
		plantBomb();
    	moveDirection = new Vector3(0,0,0);
    	if(Input.GetKey(KeyCode.LeftArrow)) {
			moveDirection = new Vector3(-10, 0, 0);
		} else if(Input.GetKey(KeyCode.RightArrow)) {
			moveDirection = new Vector3(10, 0, 0);
		} else if(Input.GetKey(KeyCode.UpArrow)) {
			moveDirection = new Vector3(0, -10, 0);
		} else if(Input.GetKey(KeyCode.DownArrow)) {
			moveDirection = new Vector3(0, 10, 0);
		}
		moveDirection *= speed;

         if (moveDirection != Vector3.zero)
             transform.forward = Vector3.Slerp(transform.forward, moveDirection, 0.3f);

        //Just tell the controller where we want to move, it will handle collisions itself
		controller.Move(moveDirection * Time.deltaTime);

		//Hay que rotar despues de mover, sino toma cualquier valor
    	if(Input.GetKey(KeyCode.LeftArrow)) {
			transform.eulerAngles = new Vector3(0, 270, 270);
		} else if(Input.GetKey(KeyCode.RightArrow)) {
			transform.eulerAngles = new Vector3(0, 90, 90);
		} else if(Input.GetKey(KeyCode.UpArrow)) {
			transform.eulerAngles = new Vector3(90, 0, 0);
		} else if(Input.GetKey(KeyCode.DownArrow)) {
			transform.eulerAngles = new Vector3(270, 0, 180);
		}
	}
	
	void plantBomb() {
		if (Input.GetKeyDown(KeyCode.Space) && bombCount > 0) {
            Transform bomb = (Transform)GameObject.Instantiate(bombPrototype);

			Vector3 move = new Vector3(transform.position.x, (transform.position.y), 0.5f);
			bomb.position = move;
			bombCount--;
        }
	}
	
	public void bombExploded() {
		bombCount++;
	}
	
	void OnCollisionEnter(Collision collision) 
    {
		if(collision.collider.gameObject.tag == "Ball") {
			Application.LoadLevel ("GameOver");
		}
    }
	
	void OnParticleCollision(GameObject collision) 
    {
		if(collision.tag == "Fire") {
			Destroy(gameObject);
		}
    }
}