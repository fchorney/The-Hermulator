using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Transform Level;
	public Transform TopEdge;

	private float LevelSpeed = 6f;

	public void Start() {
		// Turn off gravity for the game
		Physics2D.gravity = Vector2.zero;
	}

	public void Update() {

		float topY = Camera.main.WorldToScreenPoint(TopEdge.transform.position).y;

		if (topY > Screen.height)
			Level.transform.position -= Vector3.up * LevelSpeed * Time.deltaTime;

	}
}