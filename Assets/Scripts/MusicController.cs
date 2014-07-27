using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	public AudioClip boss = null;
	public AudioClip intro = null;
	public AudioClip loop = null;
	public float introDelay = 0f;
	public float loopDelay = 0f;
	public float fadeDelay = 0f;
	public float fadeSpeed = 0f;
	public Transform bossTrigger;

	private bool enableBoss = false;
	private GameController gameController;

	private AudioSource audio_intro = null;
	private AudioSource audio_loop = null;
	private AudioSource audio_boss = null;

	enum StageState { level = 0, boss = 1 };
	private StageState stagestate;

	// Volume Fade for the loop music going into the boss music
	private float fade;

	// Determine if the boss music has been triggered
	private bool boss_started;

	// Use this for initialization
	void Start () {
		// Initialize our Audio Sources
		audio_intro = gameObject.AddComponent<AudioSource> ();
		audio_loop = gameObject.AddComponent<AudioSource> ();
		audio_boss = gameObject.AddComponent<AudioSource> ();

		gameController = GameController.Get();
	
		// Initialize our audio clip properties
		audio_intro.clip = intro;
		audio_intro.loop = false;
		audio_intro.Stop ();

		audio_loop.clip = loop;
		audio_loop.loop = true;
		audio_loop.Stop ();

		audio_boss.clip = boss;
		audio_boss.loop = true;
		audio_boss.Stop ();

		// Initialize our private variables
		fade = 1.0f; // Initial fade is 1.0f, aka: 100% volume
		boss_started = false; //At the start of the level, obviously we havent started the boss
		enableBoss = false;
		// Assume we dont start at a boss (because that's fucking stupid)
		stagestate = StageState.level;
	}
	
	// Update is called once per frame
	void Update () {
		switch (stagestate) {
			case StageState.level:
				// Assuming nothing is playing yet, start the intro and loop
				if (!audio_intro.isPlaying && !audio_loop.isPlaying) {
						// Start playing the intro and main loop using the delay variables
						audio_intro.PlayDelayed (introDelay);
						audio_loop.PlayDelayed (loopDelay + introDelay);
				}
			break;
			case StageState.boss:
			// If the boss music hasn't started to play yet...
				if (!boss_started) {
						// Start playing the boss music with the fadeout delay
						audio_boss.PlayDelayed (fadeDelay);
						boss_started = true;
				}

			// If either the intro or the main loop is still playing...
				if (audio_intro.isPlaying || audio_loop.isPlaying) {
						// If the fade has gone all the way down to 0, then we can stop the intro/loop music
						if (fade <= 0.0f) {
								audio_intro.Stop ();
								audio_loop.Stop ();
						} else { // Intro/Loop are still fading out, continue to lower the volume for the fade
								audio_intro.volume = fade;
								audio_loop.volume = fade;
								fade -= fadeSpeed;
						}
				}
			break;
		}

		// If at any time the enableBoss boolean is set to true, transition to the boss state
		if (enableBoss) {
				stagestate = StageState.boss;
		} else {
			if (bossTrigger != null && gameController != null)
				if (bossTrigger.position.y < gameController.activeTop)
					enableBoss = true;
		}
	}
}
// Written by The Biggest
