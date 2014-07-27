using UnityEngine;
using System.Collections;

public class MidbossController : EnemyController {
	float screenWidth;
	float fTime;
	float amplitude = 3f;
	float frequency = 0.5f;
	float activeTime;

	protected override void moveWaiting() {
		base.moveWaiting();
		if(state == State.Active) {
			transform.parent = null;
			activeTime = Time.time;
		}
	}

	protected override void moveActive() {
		Vector3 enemyPosition = transform.position;
		enemyPosition.x = amplitude *(Mathf.Sin(2*Mathf.PI*frequency*(Time.time - activeTime)));

		transform.position = enemyPosition;
	}
}
