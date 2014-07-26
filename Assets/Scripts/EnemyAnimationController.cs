using UnityEngine;
using System.Collections;

public class EnemyAnimationController : MonoBehaviour {

	public ParticleRenderer explosion;
	public Renderer ship;

	void Awake() {
		explosion.sortingLayerName = "Explosions";
	}

	// Use this for initialization
	void Start () {
		explosion.particleEmitter.emit = false;
	}
	
	public void Destroy(bool explode = false) {
		ship.enabled = false;
		if(!explode)
			return;
		explosion.enabled = true;
		explosion.particleEmitter.Emit (10);

	}
}
