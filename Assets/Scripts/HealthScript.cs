using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	int hp = 1;
	public bool isEnemy = true;

	public void Damage(int damageCount)
	{
		hp -= damageCount;
		Debug.Log ("HP: " + hp);
		if (hp <= 0)
		{
			// Dead!
			Destroy(gameObject);
		}
	}

	public int getHP() {
		return hp;
	}

	public void setHP (int hp){
		this.hp = hp;
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
				// Is this a shot?
		BulletController shot = otherCollider.gameObject.GetComponent<BulletController> ();
		if (shot != null) {
			// Avoid friendly fire
			if (shot.isEnemyShot() != isEnemy) {
				Damage (shot.damage);
			
				// Destroy the shot
				Destroy (shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
			}
		}
	}

}
