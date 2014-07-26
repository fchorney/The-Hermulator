using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public void Start() {
		// Turn off gravity for the game
		Physics2D.gravity = Vector2.zero;
	}
}