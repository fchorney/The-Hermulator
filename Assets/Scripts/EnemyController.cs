using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	private enum State {
		Waiting = 0,
		Active,
		Agressive
	};

	private State state;
	private GameObject player;
	private EnemyAnimationController animationController;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		animationController = transform.GetComponentInChildren<EnemyAnimationController>();
	}

	// Update is called once per frame
	void Update () {
		switch(state) {
		case State.Waiting:
			float screenY = Camera.main.WorldToScreenPoint(transform.position).y;

			if (screenY < Screen.height - 100) {
				transform.parent = null;
				state = State.Active;
			}

			break;
		case State.Active:
			Vector3 p = transform.position;

			p.x = player.transform.position.x;

			transform.position = p;

			//transform.position += Vector3.up * LevelSpeed * Time.deltaTime;
			break;

		}
	}

	public void kill(bool explode = false) {
		animationController.Destroy(explode);
	}
}
	