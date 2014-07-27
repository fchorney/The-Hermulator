using UnityEngine;
using System.Collections;

public class BulletExplosionController : MonoBehaviour {

	public ParticleRenderer explosions;
	
	// Use this for initialization
	void Start () {
		explosions.sortingLayerName = "Explosions";
	}
	
	public void Emit(int size, Vector3 position) {
		explosions.transform.position = position;
		explosions.particleEmitter.Emit (size);
	}
	
	// Update is called once per frame
	void Update () {
		explosions.transform.position = transform.position;
	}
}
