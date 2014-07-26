using UnityEngine;
using System.Collections;

public class ShipAnimationController : MonoBehaviour {

	public Renderer PropRenderer;

	public Renderer BigGunLeft;
	public Renderer BigGunRight;
	public Renderer SmallGunLeft;
	public Renderer SmallGunRight;

	public ParticleRenderer explode;
	
	private float PropTimer;
	private float PropInterval = 60f;

	private float GunTimer;
	private float GunInterval = 30f;

	private ShipController shipController;

	void Awake() {
		explode.sortingLayerName = "Explosions";
	}

	void Start() {
		PropTimer = 0;
		GunTimer = 0;

		explode.particleEmitter.emit = false;

		shipController = transform.GetComponentInParent<ShipController>();
	}

	// Update is called once per frame
	void Update () {

		if(!shipController.isAlive())
			return;

		PropTimer += Time.deltaTime;

		GunTimer += Time.deltaTime;

		PropRenderer.enabled = Mathf.Sin(PropTimer * PropInterval) < 0;

		BigGunLeft.enabled = BigGunRight.enabled = Mathf.Sin(GunTimer * GunInterval) < 0;
		SmallGunLeft.enabled = SmallGunRight.enabled = Mathf.Cos(GunTimer * GunInterval) < 0;
	}

	public void Explode() {
		foreach (Renderer r in transform.GetComponentsInChildren<Renderer>()) {
			r.enabled = false; 
		}

		explode.enabled = true;
		explode.particleEmitter.Emit (50);
	}
}
