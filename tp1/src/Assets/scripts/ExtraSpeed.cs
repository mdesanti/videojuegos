using UnityEngine;

public class ExtraSpeed : MonoBehaviour
{	
	
	public int score;

    void OnCollisionEnter(Collision collision) 
    {
		if(collision.collider.gameObject.tag == "Player") {
			int playerScore = PlayerPrefs.GetInt("Score");
			PlayerPrefs.SetInt("Score",playerScore + score);
			collision.collider.gameObject.GetComponent<PlayerController>().speed *= (float)2;
			GameObject.Destroy(gameObject);
		}
    }		
}

