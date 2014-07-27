using UnityEngine;
using System.Collections;

public class SlowHoming : EnemyMovementPattern {

	public override void Move(GameObject obj) {
		Vector3 position = obj.transform.position;
		if(player != null) {
			float playerX = player.transform.position.x;
			float delta = FlightSpeed * Time.deltaTime;

			if(position.x > playerX + delta)
				position.x -= delta;
			else if(position.x < playerX - delta)
				position.x += delta;
			else
				position.x = playerX;
			
			obj.transform.position = position;
		}
	}
}
