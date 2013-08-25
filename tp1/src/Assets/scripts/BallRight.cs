using UnityEngine;
using System.Collections;

public class BallRight : MonoBehaviour {

	// Use this for initialization
	void Start () {
		rigidbody.AddForce(-500,400,0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}