using UnityEngine;
using System.Collections;

public class ExtraBombController : AbstractExtrasController
{	
	public int score = 20;

	public override void perform(GameObject playerGameObject) {
		int playerScore = PlayerPrefs.GetInt("Score");
		PlayerPrefs.SetInt("Score",playerScore + score);
		playerGameObject.GetComponent<PlayerController>().bombCount += 1;
		Debug.Log("Extra speed player");
		GameObject.Destroy(gameObject);
    }
}