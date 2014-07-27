using UnityEngine;
using System.Collections;

public class EnemyAnimationController : MonoBehaviour {

	public ParticleRenderer explosion;

	void Awake() {
		explosion.sortingLayerName = "Explosions";
	}

	// Use this for initialization
	void Start () {
		explosion.particleEmitter.emit = false;
	}
	
	public void Explode(bool explode = false) {
		if(!explode)
			return;

		explosion.enabled = true;
		explosion.particleEmitter.Emit (10);

	}
}
