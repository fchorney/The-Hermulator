using UnityEngine;
using System.Collections;

public class SpiralLeft : EnemyMovementPattern {
	float circleSize = 6;
	float circleSpeed = 3;
	protected enum Direction {
		Left=0,
		Right
	}

	public override void Move(GameObject obj) {
		spiral(obj, Direction.Left);
	}

	protected void spiral (GameObject obj, Direction direction) {
		Vector3 position = obj.transform.position;
		if(direction == Direction.Left) {
			position.x -= Mathf.Sin((Time.time - activeTime) * circleSpeed) * circleSize * Time.deltaTime;
		}
		else {
			position.x += Mathf.Sin((Time.time - activeTime) * circleSpeed) * circleSize * Time.deltaTime;
		}
		position.y -= Mathf.Abs(Mathf.Cos((Time.time - activeTime) * circleSpeed) * circleSize * Time.deltaTime);
		obj.transform.position = position;
	}
}
