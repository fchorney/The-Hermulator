using UnityEngine;
using System.Collections;

public class ShipCollisionController : MonoBehaviour {
	
	private ShipController shipController;
	
	void Start() {
		this.enabled = true;
		shipController = transform.GetComponentInParent<ShipController>();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag == "Enemy") {
			shipController.kill();
			this.enabled = false;
		}
	}
}
