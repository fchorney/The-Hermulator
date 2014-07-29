using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public GUISkin skin;

	public BoxCollider2D StartB;
	public BoxCollider2D Options;

	public BoxCollider2D Credits;
	public BoxCollider2D Back;
	public BoxCollider2D Sound;

	private Rect rStart;
	private Rect rOptions;

	private Rect rSound;
	private Rect rBack;
	private Rect rCredits;

	private enum State { MAIN, OPTIONS, CREDITS };

	private State state;

	Rect MakeRect(BoxCollider2D box) {
		Bounds b = box.bounds;
		
		Vector2 min = Camera.main.WorldToScreenPoint(new Vector2(b.min.x, b.max.y));
		Vector2 max = Camera.main.WorldToScreenPoint(new Vector2(b.max.x, b.min.y));
		
		Rect r = new Rect(min.x, Screen.height - min.y, max.x - min.x, Mathf.Abs(max.y - min.y));
		
		return r;
	}

	void Start() {
		state = State.MAIN;
		rStart = MakeRect(StartB);
		rOptions = MakeRect(Options);

		rSound = MakeRect(Sound);
		rBack = MakeRect(Back);
		rCredits = MakeRect(Credits);
	}

	void OnGUI() {
		GUI.skin = skin;
		skin.button.fontSize = Screen.width / 25;

		switch (state) {
		case State.CREDITS:
			if (Input.GetMouseButtonUp(0) || Input.GetKeyDown (KeyCode.Escape)) {
				Vector3 pos = camera.transform.position;
				pos.y = -38.4f;
				camera.transform.position = pos;
				state = State.OPTIONS;
			}
			break;
		case State.MAIN:

			if (GUI.Button (rStart, "START")) {
				Application.LoadLevel ("Level 1");
			}

			if (GUI.Button (rOptions, "OPTIONS")) {
				Vector3 pos = camera.transform.position;
				pos.y = -38.4f;
				camera.transform.position = pos;
				state = State.OPTIONS;
			}

			break;

		case State.OPTIONS:
			if (Input.GetKeyDown (KeyCode.Escape) || GUI.Button (rBack, "BACK")) {
				Vector3 pos = camera.transform.position;
				pos.y = 0f;
				camera.transform.position = pos;
				state = State.MAIN;
			}

			if (GUI.Button(rCredits, "CREDITS")) {
				Vector3 pos = camera.transform.position;
				pos.y = -19.2f;
				camera.transform.position = pos;
				state = State.CREDITS;
			}

			if (GUI.Button (rSound, "SOUND: " + (AudioListener.pause ? "Off" : "On"))){
				AudioListener.pause ^=  true;
				PlayerPrefs.SetInt("Mute", AudioListener.pause ? 1 : 0);
			}

			break;
		}
	}
}
