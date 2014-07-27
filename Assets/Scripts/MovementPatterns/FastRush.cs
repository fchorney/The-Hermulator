using UnityEngine;
using System.Collections;

public class FastRush : EnemyMovementPattern {

	public override void Move (EnemyMovementController controller) {
		Vector3 position = controller.gameObject.transform.position;
		position.y -= controller.FlightSpeed;
		controller.gameObject.transform.position = position;
		
	}
}
