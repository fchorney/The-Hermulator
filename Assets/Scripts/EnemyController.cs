using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	private ExplosionController explosionController;
	private GameController gameController;

	protected EnemyShotController shotController;


	public virtual void Start() {
		explosionController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<ExplosionController> ();
		gameController = GameController.Get();
		shotController = transform.GetComponent<EnemyShotController>();
	}



	// Update is called once per frame
	void Update () {
		if (transform.position.y < gameController.activeBottom) {
			Destroy(this.gameObject);
		}
	}

	public void kill(bool explode = false) {


		if (explode) {
			explosionController.Emit (50, transform.position);
			Debug.Log ("Explode @ " + transform.position);
		}

		//animationController.Explode(explode);
		Destroy (this.gameObject);
	}

}
	
