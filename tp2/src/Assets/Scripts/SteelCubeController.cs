using UnityEngine;
using System.Collections;

public class SteelCubeController : AbstractCollisionController
{
	
	public override bool isDestroyedByFire() {
		return false;
	}
}