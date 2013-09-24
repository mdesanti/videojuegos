using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour
{
	private double elapsedTime = 0;
	private static double EXPLOSION_TIME = 3;
	public Transform explotionPrototype;
	public static int wide = 1;
	public AudioClip explosionClip;
	private Vector3 position;
	private PlayerController player;
	private Transform[] explotionPool;
	private static int POOL_SIZE = 15*15;


	void Start() {
        position = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        initPool();
    }

    private void initPool() {
    	explotionPool = new Transform[POOL_SIZE];
    	for(int i = 0; i < POOL_SIZE; i++) {
    		explotionPool[i] = (Transform)GameObject.Instantiate(explotionPrototype);
    		explotionPool[i].gameObject.SetActive(false);
    	}
    }

    void FixedUpdate() {
		elapsedTime += Time.deltaTime;
		if(elapsedTime >= EXPLOSION_TIME) {

			//poner la primera explosion donde estaba la bomba
			Transform explotion = getExplotion();
			explotion.position = position;

			int i;
			bool loop;
			Vector3 move;
			//explosiones hacia la derecha
			Vector3 dir = new Vector3(1, 0, 0);
			for(i = 1, loop = true; i <= wide && loop; i++) {
				move = new Vector3(position.x + i * 10, position.y, 0.5f);
				loop = putExplosion(dir, move, i);
			}
			//explosiones hacia la izquierda
			dir = new Vector3(-1, 0, 0);
			for(i = -1, loop = true; i >= -wide && loop; i--) {
				move = new Vector3(position.x + i * 10, position.y, 0.5f);
				loop = putExplosion(dir, move, i);
			}
			//explosiones hacia atras
			dir = new Vector3(0, 1, 0);
			for(i = 1, loop = true; i <= wide && loop; i++) {
				move = new Vector3(position.x, position.y + i * 10, 0.5f);
				loop = putExplosion(dir, move, i);
			}
			//explosiones hacia adelante
			dir = new Vector3(0, -1, 0);
			for(i = -1, loop = true; i >= -wide && loop; i--) {
				move = new Vector3(position.x, position.y + i * 10, 0.5f);
				loop = putExplosion(dir, move, i);
			}

			Destroy(gameObject);
			AudioSource.PlayClipAtPoint(explosionClip , transform.position);
			if(player != null) {
				player.incrementBombQtty();
			} else {
				Debug.Log("Should not happen");
			}
		}
	}

	private bool putExplosion(Vector3 dir, Vector3 move, int i) {
		UnityEngine.RaycastHit hitInfo = new RaycastHit();
		//si no le pego a nada o le pego a un extra o le pego al player pongo la explosion
		if (!Physics.Raycast(position, dir, out hitInfo, 10f * Mathf.Abs(i)) || hitInfo.collider.tag == "Extra" || hitInfo.collider.tag == "Player") {
			Transform explotion = getExplotion();
			explotion.position = move;
		} else if(hitInfo.collider.tag == "Wooden Cube") { //si le pego a un cubo de madera pongo la explosion y corto
			Transform explotion = getExplotion();
			explotion.position = move;
			return false;
		} else if(hitInfo.collider.tag == "Steel Cube") {
			return false;
		}
		return true;
	}

	private Transform getExplotion() {
		for(int i = 0; i < POOL_SIZE; i++) {
			if(!explotionPool[i].active) {
				//explotionPool[i].SetActive(true);
				explotionPool[i].active = true;
				return explotionPool[i].transform;
			}
		}
		return null;
	}

	public static void incrementWide() {
		wide += 1;
	}
}