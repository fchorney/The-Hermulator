using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	protected Vector2 speed = new Vector2(1f,1f);

	protected Vector2 direction = new Vector2(0,8f);
	protected GameObject player;
	public BulletPool bulletPool;
	public int damage = 1;
	public bool SurvivesEnemyCollision = false;

	private GameController gameController;

	private Vector2 movement;

	void Start () {
		gameController = GameController.Get();
	}

	public int getDamage() {
		return damage;
	}

	public void returnToPool(){
		bulletPool.returnBullet(transform.rigidbody2D);
	}

	public void show(){
		transform.renderer.enabled = true;

		this.gameObject.SetActive (true);
		this.enabled = true;
	}

	public void SetDirection(Vector2 direction) {
		this.direction = direction;
	}

	public void hide(){
		transform.renderer.enabled = false;
		this.gameObject.SetActive (false);
		this.enabled = false;
	}

	public bool isEnemyShot() {
		return false;
	}

	protected void FindPlayer() {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	protected virtual void MoveObject() {
		//Debug.Log ("baseClass");
		transform.rigidbody2D.velocity = new Vector2(
			speed.x * direction.x,
			speed.y * direction.y);

		// off bottom
		if (transform.position.y < gameController.activeBottom) {
			returnToPool();
		}

		// off top
		if (transform.position.y > gameController.activeTop) {
			returnToPool();
		}
	}

	// Update is called once per frame
	protected virtual void Update () {
		MoveObject ();

		FindPlayer ();


	}
}
