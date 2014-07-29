using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public GUISkin skin;

	private bool on_credits = false;

	public BoxCollider2D StartB;
	public BoxCollider2D Options;

	private Rect rStart;
	private Rect rOptions;

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
	}

	void OnGUI() {
		GUI.skin = skin;
		skin.button.fontSize = Screen.width / 25;
		if (on_credits) {
			if (Input.GetMouseButtonUp(0)) {
				Vector3 pos = camera.transform.position;
				pos.y = 0f;
				camera.transform.position = pos;
				on_credits = false;
			}
		} else {
			if (GUI.Button (rStart, "START")) {
				Application.LoadLevel ("Level 1");
			}
			if (GUI.Button (rOptions, "OPTIONS")) {
				Vector3 pos = camera.transform.position;
				pos.y = -19.2f;
				camera.transform.position = pos;
				on_credits = true;
			}
		}
	}
}
