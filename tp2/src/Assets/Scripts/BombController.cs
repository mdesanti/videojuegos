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


	void Start() {
        position = transform.position;
    }

    void FixedUpdate() {
		elapsedTime += Time.deltaTime;
		if(elapsedTime >= EXPLOSION_TIME) {

			//poner la primera explosion donde estaba la bomba
			Transform explotion = (Transform)GameObject.Instantiate(explotionPrototype);
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
			GameObject go = GameObject.FindGameObjectWithTag("Player");
			if(go != null) {
				go.GetComponent<PlayerController>().bombExploded();
			} else {
				Debug.Log("Should not happen");
			}
		}
	}

	private bool putExplosion(Vector3 dir, Vector3 move, int i) {
		UnityEngine.RaycastHit hitInfo = new RaycastHit();
		//si no le pego a nada o le pego a un extra o le pego al player pongo la explosion
		if (!Physics.Raycast(position, dir, out hitInfo, 10f * Mathf.Abs(i)) || hitInfo.collider.tag == "Extra" || hitInfo.collider.tag == "Player") {
			Transform explotion = (Transform)GameObject.Instantiate(explotionPrototype);
			explotion.position = move;
		} else if(hitInfo.collider.tag == "Wooden Cube") { //si le pego a un cubo de madera pongo la explosion y corto
			Transform explotion = (Transform)GameObject.Instantiate(explotionPrototype);
			explotion.position = move;
			return false;
		} else if(hitInfo.collider.tag == "Steel Cube") {
			return false;
		}
		return true;
	}

	public static void incrementWide() {
		wide += 1;
	}
}