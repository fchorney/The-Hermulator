using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	private bool on_credits = false;

	public BoxCollider2D StartB;
	public BoxCollider2D Credits;

	private Rect rStart;
	private Rect rCredits;

	Rect MakeRect(BoxCollider2D box) {
		Bounds b = box.bounds;
		
		Vector2 min = Camera.main.WorldToScreenPoint(new Vector2(b.min.x, b.max.y));
		Vector2 max = Camera.main.WorldToScreenPoint(new Vector2(b.max.x, b.min.y));
		
		Rect r = new Rect(min.x, Screen.height - min.y, max.x - min.x, Mathf.Abs(max.y - min.y));
		
		return r;
	}

	void Start() {
		rStart = MakeRect(StartB);
		rCredits = MakeRect(Credits);
	}

	void OnGUI() {
		if (on_credits) {
			if (Input.GetMouseButtonDown(0)) {
				Vector3 pos = camera.transform.position;
				pos.y = 0f;
				camera.transform.position = pos;
				on_credits = false;
			}
		} else {
			if (GUI.Button (rStart, "Start")) {
				Application.LoadLevel ("Level 1");
			}
			if (GUI.Button (rCredits, "Credits")) {
				Vector3 pos = camera.transform.position;
				pos.y = -19.2f;
				camera.transform.position = pos;
				on_credits = true;
			}
		}
	}
}
