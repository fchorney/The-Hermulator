using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	public bool Alive { get; private set; }
	public bool Invincible;
	public bool Frozen;
	public bool ShootingEnabled;

	private ExplosionController explosionController;
	private GameController gameController;

	public Transform Target;

	// Use this for initialization
	void Start () {
		Alive = true;
		Invincible = true;
		Frozen = false;
		ShootingEnabled = false;

		gameController = GameController.Get();
		explosionController = gameController.GetComponent<ExplosionController>();

	}

	public void kill() {
		if (Alive) {
			gameController.OnPlayerDeath(this.gameObject);
			Alive = false;

			explosionController.Emit(100, transform.position);

			Destroy(this.gameObject);
		}

	}
}
