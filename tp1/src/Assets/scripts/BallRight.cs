using UnityEngine;
using System.Collections;

public class BallRight : MonoBehaviour {

	// Use this for initialization
	void Start () {
		rigidbody.AddForce(-500,200,0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}