using UnityEngine;
using System.Collections;

public class PlayerCollisionController : MonoBehaviour {

	
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag == "Enemy") {
			Debug.Log ("Hit enemy");
		}
	}
}
