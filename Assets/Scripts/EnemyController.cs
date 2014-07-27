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
	private ExplosionController explosionController;
	private GameController gameController;

	private EnemyShotController shotController;
	protected float flightSpeed= 2f;

	public HealthScript enemyHealth;


	protected float activeTime;

	protected Vector3 enemyPosition;
	
	public virtual void Start() {
		explosionController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<ExplosionController> ();
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();

		shotController = transform.GetComponent<EnemyShotController>();

		enemyHealth = transform.GetComponent<HealthScript> ();


		FindPlayer();

		enemyHealth.setHP (3);
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

		if (transform.position.y < gameController.activeBottom) {
			Destroy(this.gameObject);
		}
	}

	protected virtual void moveActive() {
		if(player != null) {
			float playerX = player.transform.position.x;
			float delta = flightSpeed * Time.deltaTime;
			if(enemyPosition.x > playerX + delta)
				enemyPosition.x -= delta;
			else if(enemyPosition.x < playerX - delta)
				enemyPosition.x += delta;
			else
				enemyPosition.x = playerX;
			
			transform.position = enemyPosition;

			shotController.Fire();
		}
	}

	protected virtual void moveWaiting() {
		if (transform.position.y < gameController.activeTop) {
			state = State.Active;
			activeTime = Time.time;
		}
	}

	public void kill(bool explode = false) {


		if (explode)
			explosionController.Emit (50, transform.position);

		//animationController.Explode(explode);
		Destroy (this.gameObject);
	}
}
	
