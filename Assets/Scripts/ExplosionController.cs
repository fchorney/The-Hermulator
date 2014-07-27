using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour {

	public ParticleRenderer explosions;

	// Use this for initialization
	void Start () {
		explosions.sortingLayerName = "Explosions";
		explosions.particleSystem.loop = true;
	}

	public void Emit(int size, Vector3 position) {
		explosions.transform.position = position;
		explosions.particleEmitter.Emit (10);
	}
	
	// Update is called once per frame
	void Update () {
		explosions.transform.position = transform.position;
	}
}
