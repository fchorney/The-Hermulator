using UnityEngine;
using System.Collections;

public class ShipAnimationController : MonoBehaviour {

	public Renderer PropRenderer;

	public Renderer BigGunLeft;
	public Renderer BigGunRight;
	public Renderer SmallGunLeft;
	public Renderer SmallGunRight;

	private float PropTimer;
	private float PropInterval = 60f;

	private float GunTimer;
	private float GunInterval = 30f;

	void Start() {
		PropTimer = 0;

		GunTimer = 0;
	}

	// Update is called once per frame
	void Update () {

		PropTimer += Time.deltaTime;

		GunTimer += Time.deltaTime;

		PropRenderer.enabled = Mathf.Sin(PropTimer * PropInterval) < 0;


		BigGunLeft.enabled = BigGunRight.enabled = Mathf.Sin(GunTimer * GunInterval) < 0;
		SmallGunLeft.enabled = SmallGunRight.enabled = Mathf.Cos(GunTimer * GunInterval) < 0;
	}
}
