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
		int rand = Random.Range(0,3);
		int last = rand;
		bool found = false;
		
		if(last == -1) {
			last = 3;
		}
		Vector3 dir = Vector3.zero;
		do {
			dir = getDirection(rand);
			if (!Physics.Raycast (transform.position, dir, 10)) {
				found = true;
			} else {	
				rand++;
				if(rand > 3) {
					rand = 0;
				}
			}
		} while(rand != last && !found);
		Debug.Log("Direction -> " + dir);
		return dir*10;
	}
	
	private Vector3 getDirection(int i) {
		switch(i) {
		case 0: return new Vector3(-1,0,0);
		case 1: return new Vector3(1,0,0);
		case 2: return new Vector3(0,1,0);
		case 3: return new Vector3(0,-1,0);
		default: return Vector3.zero;
		}
	}
}
