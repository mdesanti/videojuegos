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

	private int moves = 0;
	//Cantidad total de pasos para hacer un movimiento. Mientras mayor sea, mas lento se mueve.
	private int MAX_MOVES = 10;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
		plantBomb();
    	if(Input.GetKey(KeyCode.LeftArrow) && moves == 0) {
			moveDirection = new Vector3(-10, 0, 0);
			moves = MAX_MOVES;
		} else if(Input.GetKey(KeyCode.RightArrow) && moves == 0) {
			moveDirection = new Vector3(10, 0, 0);
			moves = MAX_MOVES;
		} else if(Input.GetKey(KeyCode.UpArrow) && moves == 0) {
			moveDirection = new Vector3(0, -10, 0);
			moves = MAX_MOVES;
		} else if(Input.GetKey(KeyCode.DownArrow) && moves == 0) {
			moveDirection = new Vector3(0, 10, 0);
			moves = MAX_MOVES;
		}

        //Just tell the controller where we want to move, it will handle collisions itself
         if (moveDirection != Vector3.zero && moves > 0){
         	controller.Move(moveDirection * 1/MAX_MOVES);
			moves--;
			if(moves == 0) {
				moveDirection = new Vector3(0,0,0);
				transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0.5f);
			}
		}

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