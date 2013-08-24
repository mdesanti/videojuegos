using UnityEngine;
using System.Collections;

public class BallSmall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		float verticalForce = transform.position.y - 35;
		rigidbody.AddForce(400, 30*verticalForce,0);
		int ballLayer = LayerMask.NameToLayer("Ball");
		Physics.IgnoreLayerCollision(ballLayer, ballLayer);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}