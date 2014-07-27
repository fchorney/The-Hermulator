using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public int Lives = 5;
	public Transform Level;
	public Transform TopEdge;

	public CheckpointController[] Checkpoints;
	private int Checkpoint;

	public Renderer GameOverBanner;

	public int score;

	public void addToScore(int points){
		score += score;
	}

	public bool ShootingEnabled { get; private set; }

	private float LevelSpeed = 6f;

	public float activeTop { get; private set; }
	public float activeBottom { get; private set; }

	public float activeLeft { get; private set; }
	public float activeRight { get; private set; }

	private MusicController musicController;

	public void Start() {
		// Turn off gravity for the game
		Physics2D.gravity = Vector2.zero;

		GameObject.FindGameObjectWithTag("Player").GetComponent<ShipController>().startSpawnTimer();

		musicController = GetComponent<MusicController>();

		activeBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).y;
		activeTop = Camera.main.ViewportToWorldPoint(new Vector3(0, 1)).y;

		ShootingEnabled = true;

		Checkpoint = 0;

		score = 0;
	}

	public void Update() {

		// continue to move the level until we hit the top edge
		if (Checkpoint < Checkpoints.Length && Checkpoints[Checkpoint].transform.position.y < activeTop) {
			if (Checkpoints[Checkpoint].CheckpointComplete)
				Checkpoint++;
		} else if (TopEdge.transform.position.y > activeTop) {
			Level.transform.position -= Vector3.up * LevelSpeed * Time.deltaTime;
		}

		if (GameOverBanner.enabled && Input.GetMouseButtonDown(0))
			Application.LoadLevel (Application.loadedLevel);
	}

	public void OnDeath(GameObject ship) {
		Lives --;
		ShootingEnabled = false;

		StartCoroutine(Respawn(ship));
	}

	private IEnumerator Respawn(GameObject ship) {
		GameObject clone = null;

		// clear bullets
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Bullet"))
			obj.GetComponent<BulletController>().bulletPool.returnBullet(obj.rigidbody2D);

		if (Lives > 0) {
			clone = Instantiate(ship, new Vector3(-10000, 10000), Quaternion.identity) as  GameObject;
		} else {
			GameOverBanner.enabled = true;
		}
	
		Destroy(ship);

		if (clone != null) {
			yield return new WaitForSeconds(1);
			clone.transform.position = new Vector3(0, -9, 0);
			yield return new WaitForSeconds(1);
			clone.transform.position = new Vector3(0, -8, 0);
			clone.GetComponent<ShipController>().startSpawnTimer();
		}

		ShootingEnabled = true;

	}

	public static GameController Get() {
		GameObject root = GameObject.FindGameObjectWithTag ("GameController");
		if (root != null)
			return root.GetComponent<GameController> ();
		return null;
	}
}