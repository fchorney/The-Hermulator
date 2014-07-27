using UnityEngine;
using System.Collections;

public class SpiralRight : SpiralLeft {

	public override void Move(GameObject obj) {
		spiral(obj, Direction.Right);
	}
}
