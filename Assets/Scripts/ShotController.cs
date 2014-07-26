using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour {

	public Transform leftGunner, rightGunner;
	public BulletPool bulletPool;
	private float shootCooldown;
	private Rigidbody2D shotTransform;
	private enum BulletType { Normal }
	private BulletType bulletType;

	enum GunnerSide { Left, Right };

	private GunnerSide gunnerToggle;
	void Start()
	{
		shootCooldown = 1.5f;
		gunnerToggle = GunnerSide.Left;
		bulletType = BulletType.Normal;
		//bulletPool = transform.GetComponent<BulletPool> ();
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
			return shootCooldown <= 1.1f;
		}
	}
	
	// Update is called once per frame
	public void Fire(){
		Debug.Log ("FIRE");
		if (CanAttack && !bulletPool.maxBullet()) {
			shotTransform = bulletPool.getBullet ();
			Debug.Log ("SHOOT");
			switch (gunnerToggle){
			case GunnerSide.Left:
				shotTransform.transform.position = leftGunner.position;

				gunnerToggle = GunnerSide.Right;
				break;

			case GunnerSide.Right:
				shotTransform.transform.position = rightGunner.position;

				gunnerToggle = GunnerSide.Left;
				break;
			}


		}
	}
}
