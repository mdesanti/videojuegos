using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour 
{
	public float smooth = (float)2.0;
	public float DoorOpenAngle = (float)110.0;
	public bool open = false;
	

	void Update () 
	{
		if(open == true)
		{
			transform.eulerAngles = new Vector3(90, transform.rotation.y, transform.rotation.z);
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			//var target = Quaternion.Euler (transform.localRotation.x, transform.localRotation.y+DoorOpenAngle, transform.localRotation.z);
			//transform.localRotation = Quaternion.Slerp(transform.localRotation, target,
			//Time.deltaTime * smooth);
		}
	
	}

	//Activate the Main function when player is near the door
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			open = true;
		}
	}
	
}

