using UnityEngine;
using System.Collections;

public class ExtraExplosionController : AbstractExtrasController
{	
	public int score = 20;
	public AudioClip explosionClip;

	public override void perform(GameObject playerGameObject) {
		AudioSource.PlayClipAtPoint(explosionClip , transform.position);
		int playerScore = PlayerPrefs.GetInt("score");
		PlayerPrefs.SetInt("score", playerScore + score);
		BombController.incrementWide();
		Debug.Log("Extra explosion player");
		GameObject.Destroy(gameObject);
    }
}