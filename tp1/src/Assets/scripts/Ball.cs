using UnityEngine;

// The RequireComponent attribute ensures a CharacterController is added to the
// GameObject whenever a PlayerController is added
public class Ball : MonoBehaviour
{	
	public Transform childBall;

    void Start()
    {
        int ballLayer = LayerMask.NameToLayer("Ball");
        Physics.IgnoreLayerCollision(ballLayer, ballLayer);
    }

    void OnCollisionEnter(Collision collision) 
    {
        //Debug.Log("Hit a " + collision.collider.gameObject.name);
		if(collision.collider.gameObject.tag == "Bullet") {
			GameObject.Find("Ball Manager").GetComponent<BallManager>().OnBallDestroyed(this);
		}
    }
	
	public Transform getChildBall() {
		return childBall;
	}
}
