using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour {
	public Rigidbody2D prefabBullet;
	public int bulletLimit;

	private GameController gameController;

	Queue<Rigidbody2D> bulletPool;
	// Use this for initialization

	public int poolsize;
	private int bulletOnScreen;
	public enum BulletType
		{ Normal, Zigzag, Wave	};
	public BulletType bulletType;

	public void returnBullet(Rigidbody2D bullet) {
		BulletExplosionController bec = gameController.GetComponent<BulletExplosionController> ();

		bec.Emit (1, bullet.position);

		bulletOnScreen--;
		BulletController bulletController = bullet.GetComponent<BulletController> ();

		bulletController.hide ();

		bulletPool.Enqueue (bullet);
	}

	public int getPoolSize(){
		return bulletPool.Count;
	}

	public bool maxBullet(){
		if (bulletOnScreen > bulletLimit){
			return true;
		}
		return false;
	}

	public BulletType getBulletType(){
		return bulletType;
	}

	public Rigidbody2D getBullet(){
		//grab bullet
		bulletOnScreen++;
		Rigidbody2D bullet = bulletPool.Dequeue ();

		//grab controller
		BulletController bulletController = bullet.GetComponent<BulletController> ();

		//bulletcontroller.start
		bulletController.show ();

		//return bullet
		return bullet;
	}

	void Start () {
		gameController = GameController.Get ();

		bulletPool = new Queue<Rigidbody2D> ();

		for(int i = 0; i < poolsize; i++) {
			Rigidbody2D obj = Instantiate(prefabBullet, Vector3.zero, Quaternion.identity) as Rigidbody2D;
			obj.GetComponent<BulletController>().hide();
			obj.GetComponent<BulletController>().bulletPool=this;
			obj.transform.parent = this.transform;
			bulletPool.Enqueue(obj);
		}
		
		Debug.Log ("Poolisze: " + bulletPool.Count);

	}
	

}
