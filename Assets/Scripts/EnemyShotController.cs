using UnityEngine;
using System.Collections;

public class EnemyShotController : MonoBehaviour {

	public Transform[] shotSpots;
	private int shotIndex;
	private float shootCooldown;
	public BulletPool bulletPool;

	private GameController gameController;

	void Start() {
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	void Update()
	{
		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
	}
	
	public bool CanAttack
	{
		get
		{
			return gameController.ShootingEnabled && shootCooldown <= 0f;
		}
	}

	public void Fire(){
		if (CanAttack && !bulletPool.maxBullet()) {
			
			Rigidbody2D shot = bulletPool.getBullet ();
			Transform spot = shotSpots[shotIndex];

			shot.GetComponent<BulletController>().SetDirection(new Vector2(0, -8));
			shot.gameObject.layer = LayerMask.NameToLayer("EnemyBullet");
			shot.transform.position = spot.position;

			shotIndex = (shotIndex + 1) % shotSpots.Length;
			shootCooldown = .09f;
		}
	}
}
