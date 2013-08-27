using UnityEngine;

public class ExtraShootingSpeed : MonoBehaviour
{	
	
	public int score;
	
    void OnCollisionEnter(Collision collision) 
    {
		if(collision.collider.gameObject.tag == "Player") {
			int playerScore = PlayerPrefs.GetInt("Score");
			PlayerPrefs.SetInt("Score",playerScore + score);
			Debug.Log("Extra shooting speed Hit a player!");
			Bullet.growthFactor *= (float)1.5;
			GameObject.Destroy(gameObject);
		}
    }		
}


