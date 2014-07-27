using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour {

	public ParticleRenderer explosions;
	public AudioClip sfx;

	private AudioSource audiosource;

	// Use this for initialization
	void Start () {
		explosions.sortingLayerName = "Explosions";

		audiosource = gameObject.AddComponent<AudioSource> ();
		audiosource.clip = sfx;
		audiosource.loop = false;
	}

	public void Emit(int size, Vector3 position) {
		explosions.transform.position = position;
		explosions.particleEmitter.Emit (10);
		audiosource.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		explosions.transform.position = transform.position;
	}
}
