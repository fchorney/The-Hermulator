using UnityEngine;
using System.Collections;

public class MidbossMovement : EnemyMovementPattern {
	float amplitude = 4f;
	float frequency = 0.4f;
	
	public override void Move (GameObject obj) {
		Vector3 enemyPosition = obj.transform.position;
		enemyPosition.x = amplitude *(Mathf.Sin(2*Mathf.PI*frequency*(Time.time - activeTime)));
		obj.transform.position = enemyPosition;
	}
}
