﻿using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	private bool alive;
	private bool invincible;
	private bool frozen;
	private float spawnTimer;

	private float invincibleTime = 1.0f;
	
	private ExplosionController explosionController;
	private GameController gameController;

	public Transform Target;

	// Use this for initialization
	void Start () {
		alive = true;
		invincible = true;
		frozen = true;

		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		explosionController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ExplosionController>();

	}

	void Update () {
		if (!frozen && invincible && Time.time > spawnTimer + invincibleTime) {
			invincible = false;
		}
	}

	public bool isAlive() {
		return alive;
	}

	public bool isInvincible() {
		return invincible;
	}
	
	public bool isFrozen() {
		return frozen;
	}

	public void startSpawnTimer() {
		frozen = false;
		spawnTimer = Time.time;
	}

	public void kill() {
		if (alive) {
			gameController.OnDeath(this.gameObject);
			alive = false;

			explosionController.Emit(100, transform.position);
		}

	}
}
