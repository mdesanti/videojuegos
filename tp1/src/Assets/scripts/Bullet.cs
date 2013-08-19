using UnityEngine;

public class Bullet : MonoBehaviour {
	public float detonationForce = 2000.0f;
    private float growthFactor = 0.5f;

    float time = 0;

    void Start()
    {
        //rigidbody.AddForce(transform.up * detonationForce);
    }

    void FixedUpdate()
    {
        //VERY VERY IMPORTANT!! Destroy the bullet after a while
        time += Time.deltaTime;
        //transform.Scale(,,);
        //Vector3 position = transform.position;
        //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 0.01f, transform.localScale.z);
        //transform.position = position;
        //this.renderer.enabled = true;
        //Debug.Log(transform.localScale.y);
        if(transform.position.x > -80) {
            transform.localScale = new Vector3(transform.localScale.x, (float) transform.localScale.y + growthFactor, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, (float)(transform.position.y + growthFactor), transform.position.z);
        }
        //if (time > 0.5f) {
            //GameObject.Destroy(gameObject);
		//}
    }

    void OnCollisionEnter(Collision collision) 
    {
        Debug.Log("Bullet Hit a " + collision.collider.gameObject.name);
        GameObject.Destroy(gameObject);
    }
}