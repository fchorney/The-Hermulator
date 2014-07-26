using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	private Vector2 speed = new Vector2(1f,1f);

	private Vector2 direction = new Vector2(0,8f);

	public BulletPool bulletPool;

	private Vector2 movement;

	public void show(){
		transform.renderer.enabled = true;

		this.gameObject.SetActive (true);
		this.enabled = true;
	}

	public void hide(){
		transform.renderer.enabled = false;
		this.gameObject.SetActive (false);
		this.enabled = false;
	}





	// Update is called once per frame
	void Update () {

		transform.rigidbody2D.velocity = new Vector2(
			speed.x * direction.x,
			speed.y * direction.y);
		if (Camera.main.WorldToScreenPoint(transform.position).y > Screen.height) {
			bulletPool.returnBullet(transform.rigidbody2D);

				}

	}
}
