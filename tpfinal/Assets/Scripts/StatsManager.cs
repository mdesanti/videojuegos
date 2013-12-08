using UnityEngine;
 
public class StatsManager : MonoBehaviour
{
    // This field can be accesed through our singleton instance,
    // but it can't be set in the inspector, because we use lazy instantiation
    public int coins = 0;
	public static int STARTING_LIVES = 3;
	public int remainingLives = STARTING_LIVES;
 
    // Static singleton instance
    private static StatsManager instance;
 
    // Static singleton property
    public static StatsManager Instance
    {
        // Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
        // otherwise we assign instance to a new component and return that
        get { return instance ?? (instance = new GameObject("StatsManager").AddComponent<StatsManager>()); }
    }
	
	public void AddExtraLife() {
		remainingLives = remainingLives + 1;
	}
	
	public void OnGliderDied() {
		remainingLives = remainingLives - 1;
		if(remainingLives == 0) {
			remainingLives = STARTING_LIVES;
			Application.LoadLevel("gameover");
		} else {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
 
    // Instance method, this method can be accesed through the singleton instance
    public void OnCoinCollected()
    {
        coins++;
    }
}