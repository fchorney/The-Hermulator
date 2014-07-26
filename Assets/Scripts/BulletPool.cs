﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour {
	public Rigidbody2D prefabBullet;

	Queue<Rigidbody2D> bulletPool;
	// Use this for initialization

	public int poolsize;
	private int bulletOnScreen;



	public void returnBullet(Rigidbody2D bullet) {
		bulletOnScreen--;
		BulletController bulletController = bullet.GetComponent<BulletController> ();

		bulletController.hide ();

		bulletPool.Enqueue (bullet);
	}

	public int getPoolSize(){
		return bulletPool.Count;
	}

	public bool maxBullet(){
		if (bulletOnScreen > 20){
			return true;
		}
		return false;
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

		bulletPool = new Queue<Rigidbody2D> ();
		bulletOnScreen = 0;
		for(int i = 0; i < poolsize; i++) {
			Rigidbody2D obj = Instantiate(prefabBullet, Vector3.zero, Quaternion.identity) as Rigidbody2D;
			obj.GetComponent<BulletController>().hide();
			obj.GetComponent<BulletController>().bulletPool=this;
			bulletPool.Enqueue(obj);
		}
		
		Debug.Log ("Poolisze: " + bulletPool.Count);

	}
	

}