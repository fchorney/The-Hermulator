using UnityEngine;
using System.Collections;

public class EnemyShotController : MonoBehaviour {

	public Transform[] shotSpots;
	private int shotIndex;
	public BulletPool bulletPool;

	public float shotTime = 0.09f;

	private float shotCooldown;

	private GameController gameController;

	void Start() {
		gameController = GameController.Get();
	}

	void Update()
	{
		if (shotCooldown > 0)
		{
			shotCooldown -= Time.deltaTime;
		}
	}
	
	public bool CanAttack
	{
		get
		{
			return gameController.ShootingEnabled && shotCooldown <= 0f;
		}
	}

	public void Fire(){
		if (bulletPool == null)
			return;
		if (CanAttack && !bulletPool.maxBullet()) {
			
			Rigidbody2D shot = bulletPool.getBullet ();
			Transform spot = shotSpots[shotIndex];

			shot.GetComponent<BulletController>().SetDirection(new Vector2(0, -8));
			shot.gameObject.layer = LayerMask.NameToLayer("EnemyBullet");
			shot.transform.position = spot.position;


			shotCooldown += shotTime;
			shotIndex = (shotIndex + 1) % shotSpots.Length;
		}
	}
}
