using UnityEngine;

public class TrapController : MonoBehaviour
{
	private double elapsedTime = 0;
    private double ANIMATION_TIME = 5;


	void Update() {
		elapsedTime += Time.deltaTime;
        if(elapsedTime >= ANIMATION_TIME) {
            animation.Play();
            elapsedTime = 0;
        }
	}
}