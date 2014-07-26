﻿using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	// speed at which the ship moves to the cursor
	// when the cursor is "far" from the player when it's
	// in "loose" state
	private float moveSpeed = 20f;

	// once a ships magnitude squared
	// is greater than this, the ship goes to "locked" state
	private float lockMagnitude = 1f;

	
	// possible states
	private enum State { 
		// the ship is "loose", i.e. is not locked to a current touch
		// it will move slowly until it does
		loose=0, 

		// the shipped is locked onto a touch, it will stay snapped hard to the touch
		locked 
	};

	// current state
	private State state;

	// Use this for initialization
	void Start () {
		state = State.loose;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButton (0)) {

			// Grab the current mouse pos
			Vector2 screenPosition = Input.mousePosition;

			// Convert the position from screen coordinates to world coordinates
			Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

			switch (state) {
			case State.loose:


				// Figure out the vector between our current position and
				// where the user is clicking
				Vector2 diff = worldPosition - (Vector2)transform.position;

				// Switch to locked if we're under the threshold
				if (diff.sqrMagnitude < lockMagnitude)
					state = State.locked;

				// And let the physics engine know we want to move to that location
				// It's possibly faster technically to not use the physics engine at all here,
				// but this is one line of code
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
		
		} 
		// The mouse pos is not currently touching
		else {
			// Reset the state
			state = State.loose;
			rigidbody2D.velocity = Vector2.zero;
		}
	}
}
