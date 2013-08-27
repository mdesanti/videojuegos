using UnityEngine;

// The RequireComponent attribute ensures a CharacterController is added to the
// GameObject whenever a PlayerController is added
public class BallManager : MonoBehaviour
{	
	private static int CHILD_QTY = 2;
	public int ballCount=0;
	
	void Start() {
		int ballLayerNumber = LayerMask.NameToLayer("Ball");
		Physics.IgnoreLayerCollision(ballLayerNumber, ballLayerNumber, true);
		
		GameObject[] aux = GameObject.FindGameObjectsWithTag("Ball");
		ballCount = aux.Length;
	}
	
	public void OnBallDestroyed(Ball destroyedBall) {
		Transform childBall = destroyedBall.getChildBall();
		if(childBall != null) {
			for(int i = 0; i < CHILD_QTY; i++) {
				Transform ball1 = (Transform)GameObject.Instantiate(childBall);	
				Vector3 move = new Vector3(destroyedBall.transform.position.x, (destroyedBall.transform.position.y), 0);
				ball1.position = move;
				ball1.renderer.enabled = true;
				if(i % 2 == 0) {
					ball1.rigidbody.AddForce(600,350,0);
				} else {
					ball1.rigidbody.AddForce(-600,350,0);
				}
			}
			ballCount += CHILD_QTY;
		}
		int score = PlayerPrefs.GetInt("Score");
		PlayerPrefs.SetInt("Score",score + destroyedBall.score);
		GameObject.Destroy(destroyedBall.gameObject);
		ballCount--;
		
		if(ballCount == 0){
			if( Application.loadedLevel + 1 < Application.levelCount ){
    			Application.LoadLevel( Application.loadedLevel + 1 );
			}else{
				Application.LoadLevel ("WinScene");
			}
		} else {
			GameObject.Find("Extras Manager").GetComponent<ExtrasManager>().OnBallDestroyed(destroyedBall.transform);
		}
	}
}
