using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour {

	public Transform leftGunner, rightGunner;
	public BulletPool bulletPool;
	private float shootCooldown;
	private Rigidbody2D shotTransform;
	private enum BulletType { Normal }
	private BulletType bulletType;
	public ShipAnimationController shipAnimationController;

	enum GunnerSide { Left, Right };

	private GunnerSide gunnerToggle;
	void Start()
	{
		shootCooldown = 0f;
		gunnerToggle = GunnerSide.Left;
		bulletType = BulletType.Normal;
		//bulletPool = transform.GetComponent<BulletPool> ();
		shipAnimationController = transform.GetComponent<ShipAnimationController>();
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
			return shootCooldown <= 0f;
		}
	}
	
	// Update is called once per frame
	public void Fire(){
		if (CanAttack && !bulletPool.maxBullet()) {

			shotTransform = bulletPool.getBullet ();
			shotTransform.GetComponent("AudioSource").audio.Play();

			switch (gunnerToggle){
			case GunnerSide.Left:
				shotTransform.transform.position = leftGunner.position;
				shipAnimationController.toggleBigGunLeft();

				gunnerToggle = GunnerSide.Right;
				break;

			case GunnerSide.Right:
				shotTransform.transform.position = rightGunner.position;
				shipAnimationController.toggleBigGunRight();

				gunnerToggle = GunnerSide.Left;
				break;
			}
			shootCooldown = .09f;

		}
	}
}
