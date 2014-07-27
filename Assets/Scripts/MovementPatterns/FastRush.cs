using UnityEngine;
using System.Collections;

public class FastRush : EnemyMovementPattern {

	public float FlightSpeed = 6f;

	public override void Move (GameObject obj) {
		Vector3 position = obj.transform.position;
		position.y -= FlightSpeed * Time.deltaTime;
		obj.transform.position = position;
		
	}
}
