using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour 
{
	public float smooth = (float)2.0;
	public float DoorOpenAngle = (float)110.0;
	public bool open = false;

	//Activate the Main function when player is near the door
	//void OnTriggerEnter(Collider other)
	void OnCollisionEnter(Collision other)
	{
		if (other.collider.gameObject.tag == "Player" && !open) 
		{
			open = true;
			if(transform.position.x % 10 == 5) {
				transform.eulerAngles = new Vector3(90, 0, 0);
				transform.position = new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z + 4f);
			} else {
				transform.eulerAngles = new Vector3(90, 90, 0);
				transform.position = new Vector3(transform.position.x - 4, transform.position.y, transform.position.z + 5f);
			}
		}
	}
	
}

