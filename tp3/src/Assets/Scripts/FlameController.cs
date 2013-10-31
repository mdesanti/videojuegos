using UnityEngine;

public class FlameController : MonoBehaviour
{

	private GameObject[] explotionPool;
	private int POOL_SIZE = 20;
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
            Vector3 rotation = new Vector3(0, Mathf.Abs(transform.eulerAngles.z) * 90, 0);
            explotionPool[i].transform.eulerAngles = rotation;
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
        int i;
        bool loop;
        Vector3 dir = transform.eulerAngles;
        for(i = 0, loop = true; loop; i++) {
                Vector3 move = new Vector3(position.x + i * 10 * dir.x, position.y, position.z + i * 10 * dir.z);
                loop = putExplosion(dir, move, i);
            }
    }

    private bool putExplosion(Vector3 dir, Vector3 move, int i) {
        UnityEngine.RaycastHit hitInfo = new RaycastHit();
        if(Physics.Raycast(new Vector3(move.x, 0, move.z), dir, out hitInfo, 10f) && (hitInfo.collider.gameObject.tag == "Wall" || hitInfo.collider.gameObject.tag == "Door")) {
            Transform explotion = getExplotion();
            explotion.position = move;
            //if(hitInfo.collider)
                //Debug.Log("collider:" + hitInfo.collider.gameObject.tag);
            return false;
        } else {
            Transform explotion = getExplotion();
            if(explotion != null)
                explotion.position = move;
            return true;
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