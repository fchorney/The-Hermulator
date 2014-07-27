using UnityEngine;
using System.Collections;

public class EnemyMovementPattern : MonoBehaviour {

	protected GameObject player;
	public float activeTime;
	public float FlightSpeed = 6f;
	private GameController gameController;

	public virtual void Move(GameObject obj) {
		// do nothing;
	}

	void Update() {
		if (player == null) {
			FindPlayer();
		}
	}

	void Start() {
		gameController = GameController.Get();
	}

	public virtual void Activate() {
		activeTime = Time.time;
	}

	protected void FindPlayer() {
		player = GameObject.FindGameObjectWithTag("Player");
	}
}
