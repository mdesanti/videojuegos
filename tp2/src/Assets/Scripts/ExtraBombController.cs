using UnityEngine;
using System.Collections;

public class ExtraBombController : AbstractExtrasController
{	
	public int score = 20;
	public AudioClip explosionClip;

	public override void perform(GameObject playerGameObject) {
		AudioSource.PlayClipAtPoint(explosionClip , transform.position);
		int playerScore = PlayerPrefs.GetInt("score");
		PlayerPrefs.SetInt("score",playerScore + score);
		playerGameObject.GetComponent<PlayerController>().incrementBombQtty();
		Debug.Log("Extra bomb player");
		GameObject.Destroy(gameObject);
    }
}