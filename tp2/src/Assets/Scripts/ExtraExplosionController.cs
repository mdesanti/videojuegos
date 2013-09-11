using UnityEngine;
using System.Collections;

public class ExtraExplosionController : AbstractExtrasController
{	
	public int score = 20;

	public override void perform(GameObject playerGameObject) {
		int playerScore = PlayerPrefs.GetInt("Score");
		PlayerPrefs.SetInt("Score", playerScore + score);
		BombController.incrementWide();
		Debug.Log("Extra speed player");
		GameObject.Destroy(gameObject);
    }
}