using UnityEngine;
using System.Collections;

public class EnemyMovementPattern : MonoBehaviour {
	public float FlightSpeed = 2f;

	protected GameObject player;

	public virtual void Move(GameObject self) {
		// do nothing;
	}

	void Start() {
		FindPlayer();
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
