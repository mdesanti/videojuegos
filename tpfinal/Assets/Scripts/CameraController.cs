
using UnityEngine;

public class CameraController : MonoBehaviour
{

public Transform target;

	void Update() {
	    transform.position = new Vector3(0, 20, target.position.z - 40);
	}

}