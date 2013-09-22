using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour
{	
	void Start()
    {
		//autodestroy after 5 seconds
        Destroy(gameObject, 3);
    }
}
