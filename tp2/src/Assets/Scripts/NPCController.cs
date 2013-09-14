using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class NPCController : MonoBehaviour
{
	
	private float moves = 0;
	//Cantidad total de pasos para hacer un movimiento. Mientras mayor sea, mas lento se mueve.
	private float max_moves = 32;
	private Vector3 movingDirection = Vector3.zero;
	private CharacterController controller;
	
	private static Vector3 LEFT = new Vector3(-10, 0, 0);
	private static Vector3 RIGHT = new Vector3(10, 0, 0);
	private static Vector3 UP = new Vector3(0, -10, 0);
	private static Vector3 DOWN = new Vector3(0, 10, 0);
	
	void Start()
    {
        controller = GetComponent<CharacterController>();
    }
	
    void Update () {
		if(moves == 0) {
			movingDirection = getMovingDirection();
			moves = max_moves;
		}
		
		if (movingDirection != Vector3.zero && moves > 0){
			controller.Move(movingDirection * 1/max_moves);
			moves--;
			if(moves == 0) {
				movingDirection = new Vector3(0,0,0);
				transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0.5f);
			}
		}
		
		//Hay que rotar despues de mover, sino toma cualquier valor
    	if(movingDirection == LEFT && moves == max_moves-1) {
			transform.eulerAngles = new Vector3(0, 270, 270);
		} else if(movingDirection == RIGHT && moves == max_moves-1) {
			transform.eulerAngles = new Vector3(0, 90, 90);
		} else if(movingDirection == UP && moves == max_moves-1) {
			transform.eulerAngles = new Vector3(90, 0, 0);
		} else if(movingDirection == DOWN && moves == max_moves-1) {
			transform.eulerAngles = new Vector3(270, 0, 180);
		}
	}
	
	private Vector3 getMovingDirection() {
		int rand = Random.Range(0,11);
		int last = rand;
		bool found = false;
		
		if(last == -1) {
			last = 3;
		}
		Vector3 dir = Vector3.zero;
		do {
			dir = getDirection(rand);
			UnityEngine.RaycastHit hitInfo = new RaycastHit();
			Vector3 position = transform.position;
			Vector3 from = new Vector3(position.x, position.y, 0.5f);
			if (!Physics.Raycast (from, dir, out hitInfo, 10f)) {
				found = true;
			} else {	
				//Debug.Log("Ray in direction" + dir + " Hit a -> " + hitInfo.collider.tag);
				rand++;
				if(rand > 3) {
					rand = 0;
				}
			}
		} while(rand != last && !found);
		if(!found) {
			dir = Vector3.zero;
		}
		//Debug.Log("Moves -> " + dir);
		return dir*10;
	}
	
	private Vector3 getDirection(int i) {
		if(i == 0 || i == 4 || i == 8) {
			return new Vector3(-1,0,0);
		} else if(i == 1 || i == 5 || i == 9) {
			return new Vector3(1,0,0);
		} else if(i == 2 || i == 6 || i == 10) {
			return new Vector3(0,1,0);
		} else {
			return new Vector3(0,-1,0);
		}
	}
	
	void OnParticleCollision(GameObject collision) 
    {
		if(collision.tag == "Fire") {
			Destroy(gameObject);
		}
    }
}
