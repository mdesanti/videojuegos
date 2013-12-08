using UnityEngine;
 
public class StatsManager : MonoBehaviour
{
    // This field can be accesed through our singleton instance,
    // but it can't be set in the inspector, because we use lazy instantiation
    public int coins = 0;
 
    // Static singleton instance
    private static StatsManager instance;
 
    // Static singleton property
    public static StatsManager Instance
    {
        // Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
        // otherwise we assign instance to a new component and return that
        get { return instance ?? (instance = new GameObject("StatsManager").AddComponent<StatsManager>()); }
    }
 
    // Instance method, this method can be accesed through the singleton instance
    public void OnCoinCollected()
    {
        coins++;
    }
}