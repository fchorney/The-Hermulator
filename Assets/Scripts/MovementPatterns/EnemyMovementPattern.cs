using UnityEngine;
using System.Collections;

public class EnemyMovementPattern : MonoBehaviour {

	protected GameObject player;
	protected float activeTime;

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

	public void Activate() {
		activeTime = Time.time;
	}

	protected void FindPlayer() {
		player = GameObject.FindGameObjectWithTag("Player");
	}
}
