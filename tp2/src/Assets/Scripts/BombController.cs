using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour
{
	private double elapsedTime = 0;
	private static double EXPLOTION_TIME = 3;
	public Transform explotionPrototype;

    void FixedUpdate() {
		elapsedTime += Time.deltaTime;
		if(elapsedTime >= EXPLOTION_TIME) {
			Transform explotion = (Transform)GameObject.Instantiate(explotionPrototype);
			Vector3 move = new Vector3(transform.position.x, (transform.position.y), 0.5f);
			explotion.position = move;
			Destroy(gameObject);
			GameObject go = GameObject.FindGameObjectWithTag("Player");
			if(go != null) {
				go.GetComponent<PlayerController>().bombExploded();
			} else {
				Debug.Log("Should not happen");
			}
		}
	}
}