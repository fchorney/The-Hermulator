using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	private bool on_credits = false;

	void OnGUI() {
		if (on_credits) {
			if (GUI.Button (new Rect (640, 550, 80, 30), "Back")) {
				Vector3 pos = camera.transform.position;
				pos.y = 0f;
				camera.transform.position = pos;
				on_credits = false;
			}
		} else {
			if (GUI.Button (new Rect (450, 540, 100, 40), "Start")) {
				Application.LoadLevel (1);
			}
			if (GUI.Button (new Rect (600, 540, 100, 40), "Credits")) {
				Vector3 pos = camera.transform.position;
				pos.y = -19.2f;
				camera.transform.position = pos;
				on_credits = true;
			}
		}
	}
}
