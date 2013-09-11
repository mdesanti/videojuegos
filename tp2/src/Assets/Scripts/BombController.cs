using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour
{
	private double elapsedTime = 0;
	private static double EXPLOTION_TIME = 3;
	public Transform explotionPrototype;
	public static float wide = 2;

    void FixedUpdate() {
		elapsedTime += Time.deltaTime;
		if(elapsedTime >= EXPLOTION_TIME) {
			Transform verticalExplotion = (Transform)GameObject.Instantiate(explotionPrototype);
			Transform horizontalExplotion = (Transform)GameObject.Instantiate(explotionPrototype);
			Vector3 move = new Vector3(transform.position.x, (transform.position.y), 0.5f);
			verticalExplotion.position = move;
			horizontalExplotion.position = move;
			verticalExplotion.localScale = new Vector3(0.7f, wide, 1);
			horizontalExplotion.localScale = new Vector3(wide, 0.7f, 1);
			Destroy(gameObject);
			GameObject go = GameObject.FindGameObjectWithTag("Player");
			if(go != null) {
				go.GetComponent<PlayerController>().bombExploded();
			} else {
				Debug.Log("Should not happen");
			}
		}
	}

	public static void incrementWide() {
		wide += 1.5f;
	}
}