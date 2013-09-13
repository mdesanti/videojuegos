using UnityEngine;
using System.Collections;


public class ExtraSpeedController : AbstractExtrasController
{	

	public int score = 20;
	public AudioClip explosionClip;

	public override void perform(GameObject playerGameObject) {
		AudioSource.PlayClipAtPoint(explosionClip , transform.position);
		int playerScore = PlayerPrefs.GetInt("Score");
		PlayerPrefs.SetInt("Score",playerScore + score);
		playerGameObject.GetComponent<PlayerController>().updateSpeed();
		Debug.Log("Extra speed player");
		GameObject.Destroy(gameObject);
    }
}