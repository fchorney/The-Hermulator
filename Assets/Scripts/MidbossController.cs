using UnityEngine;
using System.Collections;

public class MidBossController : EnemyController {

	protected override void moveWaiting() {
		base.moveWaiting();
		if(state == State.Active) {
			transform.parent = null;
		}
	}

	protected override void moveActive() {
		//enemyPosition.x = Mathf.Sine(Time.deltaTime * flightSpeed) - 0.5f * ScreenWidth;
	}
}
