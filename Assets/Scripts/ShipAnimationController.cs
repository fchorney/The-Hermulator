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

	private float LeftGunTimer, RightGunTimer;
	private float muzzleDisplayTime = .06f;

	private float InvisInterval = 60f;
	private ShipController shipController;

	void Awake() {
		explode.sortingLayerName = "Explosions";
	}

	void Start() {
		Reset();
	}

	private void ToggleRenderers(bool val) {
		foreach (Renderer r in transform.GetComponentsInChildren<Renderer>()) {
			r.enabled = val; 
		}
	}

	public void Reset() {
		PropTimer = 0;
		GunTimer = 0;

		LeftGunTimer = 0f;
		RightGunTimer = 0f;

		explode.particleEmitter.emit = false;
		
		shipController = transform.GetComponentInParent<ShipController>();

		ToggleRenderers(true);

		BigGunLeft.enabled = false;
		BigGunRight.enabled = false;
	
	}

	// Update is called once per frame
	void Update () {

		if(!shipController.isAlive())
			return;

		if (shipController.isInvincible())
			ToggleRenderers(Mathf.Sin (Time.time * InvisInterval) < 0);
		else
			ToggleRenderers(true);

		float tmpTime = Time.time;
		if (tmpTime >= LeftGunTimer) {
			BigGunLeft.enabled = false;
		} else {
			BigGunLeft.enabled = true;
		}

		if (tmpTime >= RightGunTimer) {
			BigGunRight.enabled = false;	
		} else {
			BigGunRight.enabled = true;
		}

		PropTimer += Time.deltaTime;
		GunTimer += Time.deltaTime;

		PropRenderer.enabled = Mathf.Sin(PropTimer * PropInterval) < 0;

		//BigGunLeft.enabled = BigGunRight.enabled = Mathf.Sin(GunTimer * GunInterval) < 0;
		//SmallGunLeft.enabled = SmallGunRight.enabled = Mathf.Cos(GunTimer * GunInterval) < 0;
	}

	public void toggleBigGunLeft(){
		LeftGunTimer = Time.time + muzzleDisplayTime;
	}

	public void toggleBigGunRight(){
		RightGunTimer = Time.time + muzzleDisplayTime; //t.enabled = !BigGunRight.enabled;
	}


	public void Explode() {
		ToggleRenderers(false);
		explode.enabled = true;
		explode.particleEmitter.Emit (50);
	}
}
