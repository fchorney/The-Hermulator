using UnityEngine;
using System.Collections;

public class EnemyCollisionController : MonoBehaviour {

	public Color damageColor = Color.red;
	public Color normalColor = Color.white;

	public bool DamageEnabled = true;

	private EnemyController enemyController;
	private GameController gameController;

	// time that we started displaying damage indicator
	private float DamageTime;

	// time to display damage
	private float DamageDisplayTime = 0.05f;



	//private BulletPool bulletPool;
	private enum CollisionType { Player, Bullet };

	// Use this for initialization
	void Start () {
		this.enabled = true;
		enemyController = transform.GetComponentInParent<EnemyController>();
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		//bulletPool = transform.GetComponentInParent<BulletPool> ();
		//bulletPool = transform.GetComponentInParent<BulletPool>();
	}

	public void ShowDamage() {
		if (!this.renderer)
			return;

		DamageTime = Time.time;
	}

	void Update() {
		SpriteRenderer renderer = this.renderer as SpriteRenderer;
		if (renderer != null) {
			if (Time.time < DamageTime + DamageDisplayTime) {
				renderer.color = damageColor;
			} else {
				renderer.color = normalColor;
			}
		}
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
					if (DamageEnabled) {

						enemyController.kill(true);
						this.enabled = false;
					}
				}
			}
			break;
		case CollisionType.Bullet:
			BulletController bullet = collision.collider.GetComponent<BulletController>();
			HealthController hp = this.GetComponentInParent<HealthController>();


			if (DamageEnabled) {
				gameController.addToScore(1000);
				hp.Damage(bullet.getDamage());
				ShowDamage();
			}

			if (!bullet.SurvivesEnemyCollision)
				bullet.returnToPool();
			break;
		}

	}


}
