﻿using UnityEngine;
using System.Collections;

public class SpawnerMovementPattern : EnemyMovementPattern {

	public float DoorCloseWaitTime = 1f;
	public float DoorAnimTime = 2f;

	public Transform[] enemies;
	public BulletPool bulletPool;

	public Transform SpawnDoorLeft, SpawnDoorRight;
	
	private enum State { WAITING=0, OPENING, CLOSING }
	private State state;
	private float stateTime;
	private int enemyIndex;

	void Start () {
		state = State.WAITING;
		enemyIndex = 0;
	}


	public override void Activate() {
		base.Activate();

		state = State.OPENING;
		stateTime = Time.time;
	}
	
	public override void Move(GameObject obj) {
		Vector3 pos;
		float progress;

		pos = SpawnDoorLeft.transform.localPosition;

		switch(state) {
		case State.WAITING:
			if (Time.time > stateTime + DoorCloseWaitTime) {
				stateTime = Time.time;
				state = State.OPENING;
			}
			pos.x = 0;
		break;
		case State.OPENING:
			if (Time.time > stateTime + DoorAnimTime)
			{
				SpawnEnemy();
				state = State.CLOSING;
				stateTime = Time.time;
			}
			progress = (Time.time - stateTime) / DoorAnimTime;
			pos.x = Mathf.Sin(progress * Mathf.PI / 2) * 0.5f;
			break;

		case State.CLOSING:
			if (Time.time > stateTime + DoorAnimTime)
			{
				state = State.WAITING;
				stateTime = Time.time;
				pos.x = 0;
			} else {
				progress = (Time.time - stateTime) / DoorAnimTime;
				pos.x = Mathf.Sin(progress * Mathf.PI / 2 + Mathf.PI/2) * 0.5f;
			}
			break;
		}

		SpawnDoorRight.transform.localPosition = pos;
		pos.x *= -1;
		SpawnDoorLeft.transform.localPosition = pos;

	}

	public void SpawnEnemy() {
	
		Debug.Log ("Spawn");
		if (enemies.Length > 0) {

			Transform obj = Instantiate(enemies[enemyIndex], transform.position, Quaternion.identity) as Transform;
		
			enemyIndex = (enemyIndex + 1) % enemies.Length;

			if (obj != null) {
				EnemyShotController shoots = obj.GetComponent<EnemyShotController>();

				if (shoots != null) {
					shoots.bulletPool = bulletPool;
				}
			}

		}
	}
}
