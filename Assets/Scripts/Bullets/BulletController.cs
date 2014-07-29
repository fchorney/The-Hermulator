using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	protected Vector2 speed = new Vector2(1f,1f);

	protected Vector2 direction = new Vector2(0,8f);
	protected GameObject player;
	public BulletPool bulletPool;
	public int damage = 1;
	public bool SurvivesEnemyCollision = false;

	float stateTime;

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
		stateTime = Time.time;
	}

	public void SetDirection(Vector2 direction) {
		this.direction = direction;
	}

	public void hide(){
		transform.renderer.enabled = false;
		this.gameObject.SetActive (false);
		this.enabled = false;
		state = 0;
	}

	public bool isEnemyShot() {
		return false;
	}

	protected void FindPlayer() {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	int state;
	protected virtual void MoveObjectWave(){
		if (Mathf.Sin ((Time.time-stateTime)*3) > 0)
			state = 2;
		else
			state = -2;
		transform.rigidbody2D.velocity = new Vector2((speed.x * direction.x)+state, speed.y * direction.y );

		//transform.rigidbody2D.position = new Vector2(speed.x * direction.x + Mathf.Sin (Time.time+.6f)*10, speed.y * direction.y );

	}

	protected virtual void MoveObjectZigZag(){
		if (Mathf.Sin (Time.time*3) > 0)
			state = 2;
		else
			state = -2;
		transform.rigidbody2D.velocity = new Vector2((speed.x * direction.x)+state, speed.y * direction.y );
	}

	protected virtual void MoveObject() {
		//Debug.Log ("baseClass");
		transform.rigidbody2D.velocity = new Vector2(speed.x * direction.x, speed.y * direction.y );
		
	}


	protected bool boundsCheck(){
			if (transform.position.y < gameController.activeBottom || transform.position.y > gameController.activeTop) {
				return true;
			}
			return false;


		}

	// Update is called once per frame
	protected virtual void Update () {


		BulletPool.BulletType bulletType = bulletPool.getBulletType ();
		switch (bulletType) {
		case BulletPool.BulletType.Normal:
			MoveObject ();
			break;
		case BulletPool.BulletType.Wave:
			MoveObjectWave ();
			break;
		case BulletPool.BulletType.Zigzag:
			MoveObjectZigZag();
			break;
		}
		//if (collision.collider.tag == "Enemy") {
		if (boundsCheck ()) {
			returnToPool();
		}


		FindPlayer ();

	}
}
