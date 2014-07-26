using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Transform Level;
	private float LevelSpeed = 6f;

	public void Start() {
		// Turn off gravity for the game
		Physics2D.gravity = Vector2.zero;
	}

	public void Update() {
		Level.transform.position -= Vector3.up * LevelSpeed * Time.deltaTime;
	}
}