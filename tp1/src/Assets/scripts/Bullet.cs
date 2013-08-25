using UnityEngine;

public class Bullet : MonoBehaviour {
	public float detonationForce = 2000.0f;
    public static float growthFactor = 0.5f;

    void Start()
    {

    }

    void FixedUpdate()
    {
            transform.localScale = new Vector3(transform.localScale.x, (float) transform.localScale.y + growthFactor,0);
            transform.position = new Vector3(transform.position.x, (float)(transform.position.y + growthFactor) ,0);
            Vector2 tiling = new Vector2(2, transform.localScale.y / 4);
            renderer.material.SetTextureScale ("_MainTex", tiling);
    }

    void OnCollisionEnter(Collision collision) 
    {
        GameObject.Destroy(gameObject);
        //Debug.Log("Bullet hit " + collision.collider.gameObject.name);
    }
}