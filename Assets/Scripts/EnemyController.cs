using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	protected enum State {
		Waiting = 0,
		Active,
		Agressive
	};

	protected State state;
	private GameObject player;
	private EnemyAnimationController animationController;
	protected float flightSpeed= 0.05f;

	protected Vector3 enemyPosition;

	void Start() {
		animationController = transform.GetComponentInChildren<EnemyAnimationController>();
		FindPlayer();
	}

	void FindPlayer() {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () {
		if (player == null)
			FindPlayer();

		enemyPosition = transform.position;
		switch(state) {
		case State.Waiting:
			moveWaiting();
			break;
		case State.Active:
			moveActive();
			break;
		case State.Agressive:
			// maybe shoot your guns or something idk
			break;
		}
	}

	protected virtual void moveActive() {
		if(player != null) {
			float playerX = player.transform.position.x;
			if(enemyPosition.x > playerX + flightSpeed)
				enemyPosition.x -= flightSpeed;
			else if(enemyPosition.x < playerX - flightSpeed)
				enemyPosition.x += flightSpeed;
			else
				enemyPosition.x = playerX;
			
			transform.position = enemyPosition;
			//transform.position += Vector3.up * LevelSpeed * Time.deltaTime;
		}
	}

	protected virtual void moveWaiting() {
		float screenY = Camera.main.WorldToScreenPoint(transform.position).y;
		
		if (screenY < Screen.height - 100)
			state = State.Active;
	}

	public void kill(bool explode = false) {
		animationController.Destroy(explode);
	}
}
	
