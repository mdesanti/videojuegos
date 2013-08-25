using UnityEngine;

public class Bullet : MonoBehaviour {
	public float detonationForce = 2000.0f;
    private float growthFactor = 0.5f;

    void Start()
    {

    }

    void FixedUpdate()
    {
        //transform.Scale(,,);
        //Vector3 position = transform.position;
        //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 0.01f, transform.localScale.z);
        //transform.position = position;
        //this.renderer.enabled = true;
        //Debug.Log(transform.localScale.y);
            transform.localScale = new Vector3(transform.localScale.x, (float) transform.localScale.y + growthFactor, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, (float)(transform.position.y + growthFactor) , transform.position.z);
            Vector2 tiling = new Vector2(2, transform.localScale.y / 4);
            renderer.material.SetTextureScale ("_MainTex", tiling);
    }

    void OnCollisionEnter(Collision collision) 
    {
        GameObject.Destroy(gameObject);
        //Debug.Log("Bullet hit " + collision.collider.gameObject.name);
    }
}