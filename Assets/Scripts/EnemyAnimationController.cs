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
		explode.enabled = false;
	}
	
	public void Explode() {
		explode.enabled = true;
		ship.enabled = false;
	}
}
