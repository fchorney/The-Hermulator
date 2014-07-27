using UnityEngine;
using System.Collections;

public class EnemyCollisionController : MonoBehaviour {

	private EnemyController enemyController;

	//private BulletPool bulletPool;
	private enum CollisionType { Player, Bullet };

	// Use this for initialization
	void Start () {
		this.enabled = true;
		enemyController = transform.GetComponentInParent<EnemyController>();

		if (enemyController == null)
			Debug.Log ("ZOMG no controller");
		//bulletPool = transform.GetComponentInParent<BulletPool> ();
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
		case CollisionType.Bullet:
			BulletController bullet = collision.collider.GetComponent<BulletController>();
			HealthController hp = this.GetComponentInParent<HealthController>();

			hp.Damage(bullet.getDamage());

			if (!bullet.SurvivesEnemyCollision)
				bullet.returnToPool();
			break;
		}

	}


}
