using UnityEngine;
using System.Collections;

public class EnemyMovementPattern : MonoBehaviour {

	protected GameObject player;
	public float TimeActive;
	public float FlightSpeed = 6f;

	public virtual void Move(GameObject obj) {
		// do nothing;
	}

	void Update() {
		if (player == null) {
			FindPlayer();
		}
	}

	protected void FindPlayer() {
		player = GameObject.FindGameObjectWithTag("Player");
	}
}
