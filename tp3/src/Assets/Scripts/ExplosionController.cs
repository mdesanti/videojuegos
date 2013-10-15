using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour
{	
	private float deltaTime = 0;

    void Update() {
    	deltaTime += Time.deltaTime;
    	if(deltaTime > 3) {
    		this.gameObject.SetActive(false);
    		deltaTime = 0;
    	}
    }
}
