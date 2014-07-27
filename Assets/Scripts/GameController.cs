using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public int Lives = 5;
	public Transform Level;
	public Transform TopEdge;

	private float LevelSpeed = 6f;

	public void Start() {
		// Turn off gravity for the game
		Physics2D.gravity = Vector2.zero;

		GameObject.FindGameObjectWithTag("Player").GetComponent<ShipController>().startSpawnTimer();
	}

	public void Update() {

		float topY = Camera.main.WorldToScreenPoint(TopEdge.transform.position).y;

		if (topY > Screen.height)
			Level.transform.position -= Vector3.up * LevelSpeed * Time.deltaTime;
	}

	public void OnDeath(GameObject ship) {
		Lives --;

		StartCoroutine(Respawn(ship));
	}

	private IEnumerator Respawn(GameObject ship) {
		GameObject clone = null;
		if (Lives > 0) {
			clone = Instantiate(ship, new Vector3(-10000, 10000), Quaternion.identity) as  GameObject;
			clone.transform.position = new Vector3(0, -10, 0);
			yield return new WaitForSeconds(1);
			clone.transform.position = new Vector3(0, -9, 0);
		}

		Destroy(ship);

		yield return new WaitForSeconds(1);

		if (clone != null) {
			clone.transform.position = new Vector3(0, -8, 0);

			clone.GetComponent<ShipController>().startSpawnTimer();
		}

	}
}