using UnityEngine;
using System.Collections;

public class MidbossController : EnemyController {
	float amplitude = 3f;
	float frequency = 0.5f;

	public void Start() {
		base.Start();
		enemyHealth.setHP(100);
	}
	protected override void moveActive() {
		Vector3 enemyPosition = transform.position;
		enemyPosition.x = amplitude *(Mathf.Sin(2*Mathf.PI*frequency*(Time.time - activeTime)));

		transform.position = enemyPosition;

		shotController.Fire();
	}
}
