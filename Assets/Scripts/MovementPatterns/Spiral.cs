using UnityEngine;
using System.Collections;

public class Spiral : EnemyMovementPattern {
	float circleSize = 3f;
	float circleSpeed = 3;

	public override void Move (GameObject obj) {
		Vector3 position = obj.transform.position;
		position.x -= Mathf.Sin((Time.time - activeTime) * circleSpeed) * circleSize * Time.deltaTime;
		position.y -= Mathf.Abs(Mathf.Cos((Time.time - activeTime) * circleSpeed) * circleSize * Time.deltaTime);
		obj.transform.position = position;
	}
}
