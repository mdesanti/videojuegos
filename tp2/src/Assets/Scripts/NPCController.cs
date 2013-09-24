using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class NPCController : MonoBehaviour
{
	
	private float moves = 0;
	public int score = 20;
	public static bool onPause = false;
	//Cantidad total de pasos para hacer un movimiento. Mientras mayor sea, mas lento se mueve.
	private float max_moves = 32;
	private Vector3 movingDirection = Vector3.zero;
	private CharacterController controller;
	
	private static Vector3 LEFT = new Vector3(-10, 0, 0);
	private static Vector3 RIGHT = new Vector3(10, 0, 0);
	private static Vector3 UP = new Vector3(0, -10, 0);
	private static Vector3 DOWN = new Vector3(0, 10, 0);
	private EnemiesManager enemiesManager;
	
	void Start()
    {
        controller = GetComponent<CharacterController>();
        enemiesManager = GameObject.Find("Enemies Manager").GetComponent<EnemiesManager>();
    }
	
    void Update () {
    	if(onPause){
    		return;
    	}
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
				if(hitInfo.collider.tag == "Player") {
					Application.LoadLevel ("GameOver");
				}
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
			enemiesManager.OnEnemyDestroyed(this);
		}
    }

    void OnCollisionEnter(Collision collision) {
    	string tag = collision.collider.gameObject.tag;
		if(tag == "Player") {
			Application.LoadLevel ("GameOver");
		}
    }

    public static void pause(){
    	onPause = !onPause;
    }

}
