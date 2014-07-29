using UnityEngine;
using System.Collections;

public class Bullet5Controller : BulletController {


	// Update is called once per frame
	void MoveObject(){

		if(player != null) {

			float playerX = player.transform.position.x;

			if (transform.position.x > (playerX - 10) || transform.position.x < (playerX + 10)) {
				//Debug.Log ("!ZERO");
				transform.rigidbody2D.velocity = new Vector2(
					speed.x * -direction.x,
					speed.y * -direction.y);
			}else {
				//Debug.Log ("ZERO");
				transform.rigidbody2D.velocity = Vector2.zero;
			}

		}
	}

	void Update() {
		MoveObject ();

		FindPlayer ();
	}
}
