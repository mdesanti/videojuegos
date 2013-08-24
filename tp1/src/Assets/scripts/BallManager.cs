using UnityEngine;

// The RequireComponent attribute ensures a CharacterController is added to the
// GameObject whenever a PlayerController is added
public class BallManager : MonoBehaviour
{	
	private static int CHILD_QTY = 2;
	public Transform bigBall;
	private int ballCount;
	
	void Start() {
		Transform mainBall = (Transform)GameObject.Instantiate(bigBall);
		mainBall.position = new Vector3((float)-16.39134, (float)50.20382, (float)100);
		ballCount++;
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
		//Physics.IgnoreCollision(ball1.collider, ball2.collider);
	}
}
