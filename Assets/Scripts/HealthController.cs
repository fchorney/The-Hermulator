using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

	public int hp = 1;
	private EnemyController enemyController;

	void Start() {
		enemyController = GetComponent<EnemyController>();
	}

	public void ShowDamage() {
		StartCoroutine(showDamageCoroutine());
	}
	
	private IEnumerator showDamageCoroutine() {
		renderer.material.color = Color.red;
		yield return new WaitForSeconds(0.5f);
		renderer.material.color = Color.white;
	}

	public void Damage(int damageCount)
	{
		hp -= damageCount;
		Debug.Log ("HP: " + hp);
		if (hp <= 0)
		{
			enemyController.kill(true);
		} else {
			ShowDamage();
		}
	}

	public int getHP() {
		return hp;
	}

	public void setHP (int hp){
		this.hp = hp;
	}

}
