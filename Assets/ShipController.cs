using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	private float moveSpeed = 20f;
	private float instaMagnitude = 1f;
	private enum State { loose=0, locked };
	private State state;

	// Use this for initialization
	void Start () {
		Physics2D.gravity = Vector2.zero;
		state = State.loose;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButton (0)) {
			Vector2 screenPosition = Input.mousePosition;
			Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

			Vector2 diff = worldPosition - (Vector2)transform.position;

			switch (state) {
			case State.loose:

				if (diff.sqrMagnitude < instaMagnitude)
					state = State.locked;

				rigidbody2D.velocity = diff.normalized * moveSpeed;

				break;
			
			case State.locked:
				Vector3 tPos = transform.position;
				tPos.x = worldPosition.x;
				tPos.y = worldPosition.y;
				transform.position = tPos;

				rigidbody2D.velocity = Vector2.zero;

				break;
			}
		
		} else {
			state = State.loose;
			rigidbody2D.velocity = Vector2.zero;
		}
	}
}
