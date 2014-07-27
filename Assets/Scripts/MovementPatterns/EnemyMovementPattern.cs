using UnityEngine;
using System.Collections;

public class EnemyMovementPattern : MonoBehaviour {

	protected GameObject player;

	public virtual void Move(GameObject obj) {
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
