using UnityEngine;
using System.Collections;

public class AntMovementPattern : EnemyMovementPattern {

	public Transform HeadJoint;
	public Transform LeftAntennaJoint1, LeftAntennaJoint2;
	public Transform RightAntennaJoint1, RightAntennaJoint2;

	public Transform LeftPincerJoint, RightPincerJoint;
	
	private Quaternion targetRotation;
	private float rotSpeed = 1f;
	private bool alive;

	private AudioSource audio;
	public AudioClip wakeup_sfx;
	public AudioClip die_sfx;


	public override void Activate() {
		alive = true;

		audio = gameObject.AddComponent<AudioSource>();
		audio.clip = wakeup_sfx;
		audio.loop = false;
		audio.PlayDelayed (0.3f);
	}

	public override void Move(GameObject self) {

		if (!alive)
			return;
	

		targetRotation = Quaternion.AngleAxis(Mathf.Sin(Mathf.PI * 2 * Time.time/3) * 90, Vector3.forward);

		HeadJoint.transform.rotation = Quaternion.Slerp(HeadJoint.transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
	
		float t = Time.time - activeTime;

		LeftAntennaJoint1.localRotation = RightAntennaJoint1.localRotation = Quaternion.Euler(new Vector3(0, 0, 20*Mathf.Sin (t/7f * Mathf.PI * 2)));
		LeftAntennaJoint2.localRotation = RightAntennaJoint2.localRotation = Quaternion.Euler(new Vector3(0, 0, 30*Mathf.Sin ((t+Mathf.PI/2)/5f * Mathf.PI * 2)));


		LeftPincerJoint.localRotation = Quaternion.Euler(new Vector3(0, 0, 15*Mathf.Sin (t/13f * Mathf.PI * 2 ) - 10));
		RightPincerJoint.localRotation = Quaternion.Euler(new Vector3(0, 0, 15*Mathf.Sin (t/13f * Mathf.PI * 2 + Mathf.PI) + 10));

		foreach (EnemyShotController esc in self.GetComponentsInChildren<EnemyShotController>()) {
			esc.Fire();
		}

		if (self.GetComponentsInChildren<EnemyController>().Length == 0)
		{
			alive = false;
			audio.clip = die_sfx;
			audio.Play ();
			StartCoroutine(StartExplosions(self));
		}
	}

	private IEnumerator StartExplosions (GameObject self) {

		ExplosionController explosions = GameObject.FindGameObjectWithTag("GameController").GetComponent<ExplosionController>();

		float r = 2.0f;

		for (int i = 0; i < 20; i ++) {
			Vector3 position = new Vector3(Random.Range(-r, r) + transform.position.x, Random.Range(-r, r) + transform.position.y);
			explosions.Emit(50, position);
			yield return new WaitForSeconds(0.1f);
		}


		GameController gc = GameController.Get();

		// show win banner if player isn't dead
		gc.WinBanner.enabled = !gc.GameOverBanner.enabled;

		Destroy(self);
	}
}
