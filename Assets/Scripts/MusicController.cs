using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	public AudioClip boss;
	public AudioClip intro;
	public AudioClip loop;
	public float introDelay;
	public float loopDelay;
	public float fadeDelay;
	public float fadeSpeed;

	public bool enableBoss = false;

	private AudioSource audio_one = null;
	private AudioSource audio_two = null;

	enum StageState { level = 0, boss = 1 };

	private StageState stagestate;

	private float fade;

	private bool boss_started;

	// Use this for initialization
	void Start () {
		stagestate = StageState.level;

		audio_one = gameObject.AddComponent<AudioSource> ();
		audio_two = gameObject.AddComponent<AudioSource> ();

		audio_one.clip = intro;
		audio_one.loop = false;

		audio_two.clip = loop;
		fade = 1.0f;

		boss_started = false;

		audio_one.Stop ();
		audio_two.Stop ();


	}
	
	// Update is called once per frame
	void Update () {
		switch (stagestate) {
			case StageState.level:
			// If neither intro nor loop are playing...
					if (!audio_one.isPlaying && !audio_two.isPlaying) {
							// Play intro and set up loop clip
							audio_one.PlayDelayed (introDelay);
							audio_two.loop = true;
							audio_two.PlayDelayed (introDelay + loopDelay);
					}

			// If the loop is playing
					if (!audio_one.isPlaying) {
							// Load up the boss music in audio one
							audio_one.clip = boss;
							audio_one.loop = true;
					}
					break;
			case StageState.boss:
			// Loop music is still playing and boss music isn't
					if (!boss_started) {
							// Fade out loop and playdelay boss music?
							
							audio_one.PlayDelayed (fadeDelay);
							boss_started = true;
					}

					if (audio_two.isPlaying) {
							if (fade <= 0.0f) {
									audio_two.Stop ();
							} else {
									audio_two.volume = fade;
									fade -= fadeSpeed;
							}
					}
					break;
			}
		if (enableBoss) {
				stagestate = StageState.boss;
		}
	}
}
