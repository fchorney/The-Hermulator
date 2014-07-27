using UnityEngine;
using System.Collections;

public class Bullet5Controller : MonoBehaviour {


	private GameObject player;

	protected Vector2 speed = new Vector2(1f,1f);
	
	protected Vector2 direction = new Vector2(0,8f);

	void FindPlayer() {
		player = GameObject.FindGameObjectWithTag("Player");
	}
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
