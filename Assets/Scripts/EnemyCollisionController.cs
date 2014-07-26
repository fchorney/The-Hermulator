using UnityEngine;
using System.Collections;

public class EnemyCollisionController : MonoBehaviour {

	private EnemyController enemyController;

	// Use this for initialization
	void Start () {
		enemyController = transform.GetComponentInParent<EnemyController>();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag == "Player") {
			Debug.Log ("Hit player");
			
			enemyController.kill();
			this.enabled = false;
		}
	}
}
