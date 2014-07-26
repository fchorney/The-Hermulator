using UnityEngine;
using System.Collections;

public class ShipCollisionController : MonoBehaviour {

	public ParticleRenderer explode;
	public GameObject ship;

	void Awake() {
		explode.sortingLayerName = "Explosions";
	}

	void Start() {
		explode.enabled = false;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag == "Enemy") {
			Debug.Log ("Hit enemy");

			foreach (Renderer r in ship.GetComponents<Renderer>()) {
				r.enabled = false;
			}
			explode.enabled = true;
		}
	}
}
