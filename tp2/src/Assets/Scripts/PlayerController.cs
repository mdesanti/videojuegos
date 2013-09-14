using UnityEngine;
using System.Collections;

// The RequireComponent attribute ensures a CharacterController is added to the
// GameObject whenever a PlayerController is added
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{	
	private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
	public Transform bombPrototype;
	public int bombCount = 1;
	public int points = 0;
	public float score;

	private float moves = 0;
	//Cantidad total de pasos para hacer un movimiento. Mientras mayor sea, mas lento se mueve.
	private float max_moves = 32;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
		plantBomb();
    	if(Input.GetKey(KeyCode.LeftArrow) && moves == 0) {
			moveDirection = new Vector3(-10, 0, 0);
			moves = max_moves;
		} else if(Input.GetKey(KeyCode.RightArrow) && moves == 0) {
			moveDirection = new Vector3(10, 0, 0);
			moves = max_moves;
		} else if(Input.GetKey(KeyCode.UpArrow) && moves == 0) {
			moveDirection = new Vector3(0, -10, 0);
			moves = max_moves;
		} else if(Input.GetKey(KeyCode.DownArrow) && moves == 0) {
			moveDirection = new Vector3(0, 10, 0);
			moves = max_moves;
		}

        //Just tell the controller where we want to move, it will handle collisions itself
         if (moveDirection != Vector3.zero && moves > 0){
			controller.Move(moveDirection * 1/max_moves);
			moves--;
			if(moves == 0) {
				moveDirection = new Vector3(0,0,0);
				//redondeo la posicion a un entero
				transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0.5f);
				//redondeo la posicion a un multiplo de 10 por las dudas que haya quedado corrido
				float x = transform.position.x;
				float y = transform.position.y;
				if(x%10 > 0) {
					if(y%10 > 0) {
						transform.position = new Vector3(x%10 >= 5? x+(10-x%10) : x-x%10, y%10 >= 5? y+(10-(y%10)) : y-y%10, 0.5f);
					} else {
						transform.position = new Vector3(x%10 >= 5? x+(10-x%10) : x-x%10, Mathf.Abs(y%10) >= 5? y-(10+(y%10)) : y-y%10, 0.5f);
					}
				} else {
					if(y%10 > 0) {
						transform.position = new Vector3(Mathf.Abs(x%10) >= 5? x-(10+x%10) : x-x%10, y%10 >= 5? y+(10-(y%10)) : y-y%10, 0.5f);
					} else {
						transform.position = new Vector3(Mathf.Abs(x%10) >= 5? x-(10+x%10) : x-x%10, Mathf.Abs(y%10) >= 5? y-(10+(y%10)) : y-y%10, 0.5f);
					}
				}
			}
		}

		//Hay que rotar despues de mover, sino toma cualquier valor
    	if(Input.GetKey(KeyCode.LeftArrow) && moves == max_moves-1) {
			transform.eulerAngles = new Vector3(0, 270, 270);
		} else if(Input.GetKey(KeyCode.RightArrow) && moves == max_moves-1) {
			transform.eulerAngles = new Vector3(0, 90, 90);
		} else if(Input.GetKey(KeyCode.UpArrow) && moves == max_moves-1) {
			transform.eulerAngles = new Vector3(90, 0, 0);
		} else if(Input.GetKey(KeyCode.DownArrow) && moves == max_moves-1) {
			transform.eulerAngles = new Vector3(270, 0, 180);
		}
	}
	
	void plantBomb() {
		if (Input.GetKeyDown(KeyCode.Space) && bombCount > 0) {
            Transform bomb = (Transform)GameObject.Instantiate(bombPrototype);
			float x = transform.position.x;
			float y = transform.position.y;
			Debug.Log("x%10" + x%10);
			Vector3 move = Vector3.zero;
			if(x%10 > 0) {
				if(y%10 > 0) {
					move = new Vector3(x%10 >= 5? x+(10-x%10) : x-x%10, y%10 >= 5? y+(10-(y%10)) : y-y%10, 0.5f);
				} else {
					move = new Vector3(x%10 >= 5? x+(10-x%10) : x-x%10, Mathf.Abs(y%10) >= 5? y-(10+(y%10)) : y-y%10, 0.5f);
				}
			} else {
				if(y%10 > 0) {
					move = new Vector3(Mathf.Abs(x%10) >= 5? x-(10+x%10) : x-x%10, y%10 >= 5? y+(10-(y%10)) : y-y%10, 0.5f);
				} else {
					move = new Vector3(Mathf.Abs(x%10) >= 5? x-(10+x%10) : x-x%10, Mathf.Abs(y%10) >= 5? y-(10+(y%10)) : y-y%10, 0.5f);
				}
			}
			Debug.Log("Player position: " + transform.position);
			Debug.Log("Bomb position: " + move);
			bomb.position = move;
			bombCount--;
        }
	}
	
	public void bombExploded() {
		bombCount++;
	}

	public void updateSpeed() {
		this.max_moves -= 4;
	}

	
	void OnParticleCollision(GameObject collision) 
    {
		string tag = collision.tag;
		if(tag == "Fire" || tag == "Enemy") {
			Application.LoadLevel ("GameOver");
		}
    }
}