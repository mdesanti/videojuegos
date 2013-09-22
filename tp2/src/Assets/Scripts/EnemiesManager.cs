using UnityEngine;

// The RequireComponent attribute ensures a CharacterController is added to the
// GameObject whenever a PlayerController is added
public class EnemiesManager : MonoBehaviour
{	
	public int enemiesCount = 0;
	
	void Start() {
		GameObject[] aux = GameObject.FindGameObjectsWithTag("Enemy");
		enemiesCount = aux.Length;
	}
	
	public void OnEnemyDestroyed(NPCController destroyedEnemy) {
		int score = PlayerPrefs.GetInt("Score");
		PlayerPrefs.SetInt("Score", score + destroyedEnemy.score);
		GameObject.Destroy(destroyedEnemy.gameObject);
		enemiesCount--;
		
		if(enemiesCount == 0){
			if( Application.loadedLevel + 1 < Application.levelCount ){
    			Application.LoadLevel( Application.loadedLevel + 1 );
			}else{
				Application.LoadLevel ("WinScene");
			}
		}
	}
}
