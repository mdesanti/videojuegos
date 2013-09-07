using UnityEngine;
using System.Collections;

public class WoodenCubeController : AbstractCollisionController
{
	
	public override bool isDestroyedByFire() {
		return true;
	}
}