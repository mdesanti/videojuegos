using UnityEngine;

public class Bullet : MonoBehaviour {
	public float detonationForce = 2000.0f;

    float time = 0;

    void Start()
    {
        rigidbody.AddForce(transform.up * detonationForce);
    }

    void FixedUpdate()
    {
        //VERY VERY IMPORTANT!! Destroy the bullet after a while
        time += Time.deltaTime;
        if (time > 1.0f) {
            //GameObject.Destroy(gameObject);
		}
    }

    void OnCollisionEnter(Collision collision) 
    {
        Debug.Log("Hit a " + collision.collider.gameObject.name);
        GameObject.Destroy(gameObject);
    }
}