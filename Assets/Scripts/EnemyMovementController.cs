using UnityEngine;
using System.Collections;

public class EnemyMovementController : MonoBehaviour {

	public EnemyMovementPattern MovementActive;
	public EnemyMovementPattern MovementWaiting;
	public EnemyMovementPattern MovementAggressive;

	private EnemyShotController shotController;

	private GameController gameController;

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
					MovementActive.Activate();
				}
				break;
			case State.Active:
				MovementActive.Move(this.gameObject);
				if (shotController != null)
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
