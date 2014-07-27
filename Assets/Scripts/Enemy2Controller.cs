using UnityEngine;
using System.Collections;

public class Enemy2Controller : EnemyController {

	new void Start() {
		base.Start();
	}

	/*protected override void moveWaiting() {
		base.moveWaiting();
		if(state == State.Active) {
			transform.parent = null;
		}
	}

	protected override void moveActive () {
		enemyPosition.y -= flightSpeed;
		transform.position = enemyPosition;

	}*/

	// this one should also shoot at player position
}
