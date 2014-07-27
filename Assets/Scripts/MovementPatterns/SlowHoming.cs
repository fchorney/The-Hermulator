using UnityEngine;
using System.Collections;

public class SlowHoming : EnemyMovementPattern {

	public override void Move(EnemyMovementController controller) {
		Vector3 position = controller.gameObject.transform.position;
		if(player != null) {
			float playerX = player.transform.position.x;
			float delta = controller.FlightSpeed * Time.deltaTime;
			if(position.x > playerX + delta)
				position.x -= delta;
			else if(position.x < playerX - delta)
				position.x += delta;
			else
				position.x = playerX;
			
			controller.gameObject.transform.position = position;
		}
	}
}
