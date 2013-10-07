using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour
{	
	private float deltaTime = 0;
	private bool emit = true;

    void Update() {
    	deltaTime += Time.deltaTime;
    	if(deltaTime > 3) {
    		this.gameObject.SetActive(false);
    	}
    }
}
