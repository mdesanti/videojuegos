using UnityEngine;

public class FlameController : MonoBehaviour
{

	private static GameObject[] explotionPool;
	private static int POOL_SIZE = 40;
	private Vector3 position;
	public GameObject explotionPrototype;
    private double elapsedTime = 0;
    private static double EXPLOSION_TIME = 5;
	
	void Start() {
        position = transform.position;
        initPool();
    }

    private void initPool() {
    	explotionPool = new GameObject[POOL_SIZE];
    	for(int i = 0; i < POOL_SIZE; i++) {
    		explotionPool[i] = (GameObject)GameObject.Instantiate(explotionPrototype);
    		explotionPool[i].SetActive(false);
    	}
    }

    void FixedUpdate() {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= EXPLOSION_TIME) {
            burn();
            elapsedTime = 0;
        }
    }

    private void burn() {
        //Debug.Log("burning");
        int i;
        bool loop;
        Vector3 dir = new Vector3(1, 0, 0);
        for(i = 1, loop = true; loop; i++) {
                Vector3 move = new Vector3(position.x + i * 10, position.y, position.z);
                loop = putExplosion(dir, move, i);
            }
    }

    private bool putExplosion(Vector3 dir, Vector3 move, int i) {
        //Debug.Log("Putting explosion");
        UnityEngine.RaycastHit hitInfo = new RaycastHit();
        Physics.Raycast(position, dir, out hitInfo, 10f * Mathf.Abs(i));
        if(!hitInfo.collider) {
            Transform explotion = getExplotion();
            explotion.position = move;
            //Debug.Log("position: " + move);
            return true;
        } else {
            //Debug.Log("hit a: " + hitInfo.collider.tag);
            return false;
        }
    }

    private Transform getExplotion() {
        for(int i = 0; i < POOL_SIZE; i++) {
            if(!explotionPool[i].activeSelf) {
                explotionPool[i].SetActive(true);
                return explotionPool[i].transform;
            }
        }
        return null;
    }


}