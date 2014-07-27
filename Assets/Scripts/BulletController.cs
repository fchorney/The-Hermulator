using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	protected Vector2 speed = new Vector2(1f,1f);

	protected Vector2 direction = new Vector2(0,8f);
	protected GameObject player;
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

	protected void FindPlayer() {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	protected virtual void MoveObject() {
		//Debug.Log ("baseClass");
		transform.rigidbody2D.velocity = new Vector2(
			speed.x * direction.x,
			speed.y * direction.y);
		if (Camera.main.WorldToScreenPoint(transform.position).y > Screen.height) {
			bulletPool.returnBullet(transform.rigidbody2D);
			
		}
	}

	// Update is called once per frame
	protected virtual void Update () {
		MoveObject ();

		FindPlayer ();


	}
}
