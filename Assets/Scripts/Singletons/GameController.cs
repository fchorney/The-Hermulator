using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public int Lives = 5;
	public Transform Level;
	public Transform Cloud;
	public Transform TopEdge;
	public GameObject PlayerPrefab;
	public Transform PlayerSpawnPoint;
	public BulletPool PlayerBulletPool;

	public CheckpointController[] Checkpoints;

	private int Checkpoint;

	public Renderer GameOverBanner;
	public Renderer WinBanner;

	public int score;

	public void addToScore(int points){
		score += points;
	}

	public bool EnemiesEnabled { get; private set; }

	private float LevelSpeed = 4f;
	private float CloudSpeed = 6;

    // time the plane animates "launching" from the bottom of the screen
    // the player cannot move the plane while launching
    private float SpawnNoShootTime = 0.5f;
    // time the plane is frozen after launching,
    // but the plane can move
	private float SpawnInvisibleTime = 1f;
	// seconds after death before player can respawn
	private float WaitAfterDeathTime = 0.5f;

	public float activeTop { get; private set; }
	public float activeBottom { get; private set; }

	public float activeLeft { get; private set; }
	public float activeRight { get; private set; }

	private MusicController musicController;

	private ShipController player;

	private enum State { PLAYING, PLAYING_INVINCIBLE, LOST, WON, PLAYING_INVINCIBLE_NO_SHOOTING, WAIT_SPAWN, WAIT_DIED, WAIT_UP };
	private State state;
	private float stateTime;

	public void Start() {
		// Turn off gravity for the game
		Physics2D.gravity = Vector2.zero;

		Checkpoints = GameObject.FindGameObjectWithTag("Level").GetComponentsInChildren<CheckpointController>();

		musicController = GetComponent<MusicController>();

		activeBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).y;
		activeTop = Camera.main.ViewportToWorldPoint(new Vector3(0, 1)).y;
		activeLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).x;
		activeRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x;

		EnemiesEnabled = false;

		Transition(State.WAIT_SPAWN);

		Checkpoint = 0;
		score = 0;

		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Detransform")) {
			obj.transform.parent = null;
		}
	}

	private void Transition(State state) {
		this.state = state;
		this.stateTime = Time.time;

		Debug.Log ("Transition to " + state);
	}

	private void GameOver(bool Won=false) { 
		// show win banner if player isn't dead
		WinBanner.enabled = Won;
		GameOverBanner.enabled = !Won;

		Transition(Won ? State.WON : State.LOST);
	}
	
	public void Update() {


		switch (state) 
		{

		// wait a sec before allowing respawn

		case State.WAIT_DIED:
			if (Time.time - stateTime > this.WaitAfterDeathTime)
				Transition(State.WAIT_SPAWN);
			break;

		
		// wait for click to spawn
		case State.WAIT_SPAWN:
			if (Input.GetMouseButton(0)) {
				player = SpawnShip();
				Transition(State.PLAYING_INVINCIBLE_NO_SHOOTING);
			} else {
				EnemiesEnabled = false;
			}

			break;

		// "spawning" means ship is moving toward finger, and invinc
		// but player cannot shoot yet
		case State.PLAYING_INVINCIBLE_NO_SHOOTING:
			if (Time.time - stateTime > this.SpawnNoShootTime) {
				player.ShootingEnabled = true;
				EnemiesEnabled = true;
				Transition(State.PLAYING_INVINCIBLE);
			}
			break;

		// player is invincible & shooting
		case State.PLAYING_INVINCIBLE:
			if (Time.time - stateTime > this.SpawnInvisibleTime) {
				player.Invincible = false;
				Transition(State.PLAYING);
			}

			goto case State.PLAYING;
		
		// regular play
		case State.PLAYING:

			Cloud.transform.position -= Vector3.up * CloudSpeed * Time.deltaTime;

			// all checkpoints are done, game must be over
			if (Checkpoint == Checkpoints.Length) {
				GameOver(true);
			}

			// the current checkpoint is done
			else if (Checkpoints[Checkpoint].CheckpointComplete) {
				// advance to the next one
				Checkpoint++;
			}
			// scroll the level until a checkpoint is in view
			else if (Checkpoints[Checkpoint].transform.position.y > activeTop) {
				Level.transform.position -= Vector3.up * LevelSpeed * Time.deltaTime;
			}
			// else
				// player is at checkpoint right now

			break;

		case State.WON:
		case State.LOST:

			// wait for a down and up before loading menu
			if ( Input.GetMouseButtonDown(0) ) {
				Transition(State.WAIT_UP);
			}
			break;

		case State.WAIT_UP:
			
			if ( Input.GetMouseButtonUp(0) ) {
				Application.LoadLevel ("Menu");
			}
			break;
		}

	}

	public void ClearBullets() {
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Bullet"))
		{
			BulletController bc = obj.GetComponent<BulletController>();

			if (bc != null)
				bc.returnToPool();
		}
	}
	
	public void OnPlayerDeath(GameObject ship) {
		Lives --;
		EnemiesEnabled = false;
		ClearBullets();

		if (Lives >= 0)
			Transition(State.WAIT_DIED);
		else
			GameOver();
	}

	private ShipController SpawnShip() {

		GameObject obj = Instantiate(PlayerPrefab, PlayerSpawnPoint.position, Quaternion.identity) as GameObject;
		obj.GetComponent<ShotController>().bulletPool = this.PlayerBulletPool;

		return obj.GetComponent<ShipController>();
		
	}
	/*
	private IEnumerator PlayerDeathCoroutine(GameObject ship) {
		/*
		GameObject clone = null;

		// clear bullets
		ClearBullets();

		// if he still has lives then clone the ship...
		// this is kind of a 
		if (Lives > 0) {
			//clone = Instantiate(ship, PlayerSpawnPoint, Quaternion.identity) as  GameObject;
		}
		else {
			GameOver();
		}
	
		Destroy(ship);

		if (clone != null) {
			yield return new WaitForSeconds(1);
			clone.transform.position = new Vector3(0, -9, 0);
			yield return new WaitForSeconds(1);
			clone.transform.position = new Vector3(0, -8, 0);
			clone.GetComponent<ShipController>().startSpawnTimer();
		}

		EnemyShootingEnabled = true;
		

	}
	*/

	public static GameController Get() {
		GameObject root = GameObject.FindGameObjectWithTag ("GameController");
		if (root != null)
			return root.GetComponent<GameController> ();
		return null;
	}

}
