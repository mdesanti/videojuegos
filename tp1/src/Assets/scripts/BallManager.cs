using UnityEngine;

// The RequireComponent attribute ensures a CharacterController is added to the
// GameObject whenever a PlayerController is added
public class BallManager : MonoBehaviour
{	
	private static int CHILD_QTY = 2;
	public int ballCount;
	
	void Start() {
		int ballLayerNumber = LayerMask.NameToLayer("Ball");
		Physics.IgnoreLayerCollision(ballLayerNumber, ballLayerNumber, true);
	}
	
	public void OnBallDestroyed(Ball destroyedBall) {
		Transform childBall = destroyedBall.getChildBall();
		if(childBall != null) {
			for(int i = 0; i < CHILD_QTY; i++) {
				Transform ball1 = (Transform)GameObject.Instantiate(destroyedBall.getChildBall());	
				ball1.position = destroyedBall.transform.position;
				ball1.renderer.enabled = true;
				if(i % 2 == 0) {
					ball1.rigidbody.AddForce(600,200,0);
				} else {
					ball1.rigidbody.AddForce(-600,200,0);
				}
			}
			ballCount += CHILD_QTY;
		}
		GameObject.Destroy(destroyedBall.gameObject);
		ballCount--;
	}
}
