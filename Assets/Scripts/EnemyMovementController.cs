﻿using UnityEngine;
using System.Collections;

public class EnemyMovementController : MonoBehaviour {

	public EnemyMovementPattern MovementActive;
	public EnemyMovementPattern MovementWaiting;
	public EnemyMovementPattern MovementAggressive;

	public float FlightSpeed = 2f;

	private EnemyShotController shotController;

	private GameController gameController;
	private float activeTime;

	protected enum State {
		Waiting = 0,	
		Active,
		Agressive
	};
	protected State state;



	// Use this for initialization
	void Start () {
		state = State.Waiting;
		shotController = transform.GetComponentInParent<EnemyShotController>();
		gameController = GameController.Get ();
	}
	
	// Update is called once per frame
	void Update () {
		switch(state) {
			case State.Waiting:
				MovementWaiting.Move(this.gameObject);
				if (transform.position.y < gameController.activeTop) {
					state = State.Active;
					activeTime = Time.time;
				}
				break;
			case State.Active:
				MovementActive.Move(this.gameObject);
				shotController.Fire();
				break;
			case State.Agressive:
				// maybe shoot your guns or something idk
				break;
		}
	}

	protected virtual void moveActive() {

	}
}