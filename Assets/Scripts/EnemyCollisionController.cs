using UnityEngine;
using System.Collections;

public class EnemyCollisionController : MonoBehaviour {

	private EnemyController enemyController;
	//private BulletPool bulletPool;
	private enum CollisionType { Player, PlayerBullet };

	// Use this for initialization
	void Start () {
		this.enabled = true;
		enemyController = transform.GetComponentInParent<EnemyController>();
		//bulletPool = transform.GetComponentInParent<BulletPool>();
	}

	void OnCollisionEnter2D(Collision2D collision) {

		CollisionType ct = (CollisionType)System.Enum.Parse(typeof(CollisionType), collision.collider.tag);
		switch (ct) {
		case CollisionType.Player:
			ShipController ship = collision.collider.GetComponent<ShipController>();

			// if ship already destroyed, don't do anything
			if (ship) {
				if (ship.isInvincible())
					// when ship is invisible, don't register a collision
					Physics2D.IgnoreCollision(this.collider2D, collision.collider);
				else {
					enemyController.kill(true);
					this.enabled = false;
				}
			}
			break;
		case CollisionType.PlayerBullet:
			enemyController.kill(true);
			//bulletPool.returnBullet(collision.collider.rigidbody2D);
			break;
		}
		//if (collision.collider.tag == "Player") {

	}


}
