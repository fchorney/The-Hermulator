using UnityEngine;
using System.Collections;

public class EnemyAnimationController : MonoBehaviour {

	public ParticleRenderer explode;
	public Renderer ship;

	void Awake() {
		explode.sortingLayerName = "Explosions";
	}

	// Use this for initialization
	void Start () {
		explode.particleEmitter.emit = false;
	}
	
	public void Explode() {
		ship.enabled = false;
		explode.enabled = true;
		explode.particleEmitter.Emit (10);

	}
}
