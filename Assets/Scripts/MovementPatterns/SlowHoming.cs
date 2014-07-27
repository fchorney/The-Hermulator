using UnityEngine;
using System.Collections;

public class SlowHoming : EnemyMovementPattern {

	public override void Move(GameObject self) {
		Vector3 position = self.transform.position;
		if(player != null) {
			float playerX = player.transform.position.x;
			float delta = FlightSpeed * Time.deltaTime;
			if(position.x > playerX + delta)
				position.x -= delta;
			else if(position.x < playerX - delta)
				position.x += delta;
			else
				position.x = playerX;
			
			self.transform.position = position;
		}
	}
}
