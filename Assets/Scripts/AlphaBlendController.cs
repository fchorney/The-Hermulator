using UnityEngine;
using System.Collections;

public class AlphaBlendController : MonoBehaviour {

	private SpriteRenderer sprite;
	private Color a;
	private Color b;
	private float t;
	private bool up;

	public float duration = 2f;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
		a = new Color (1f, 0f, 0f, 0.5f);
		b = new Color (1f, 0f, 0f, 1f);

		sprite.color = a;
		t = 0f;
		up = true;
	}
	
	// Update is called once per frame
	void Update () {
		sprite.color = Color.Lerp (a, b, t);
		if (up && t < 1) { // While t is below the end limit...
			// Increment it at the desired rate every update
			t += Time.deltaTime / duration;
		} else if (!up && t > 0) {
			t -= Time.deltaTime / duration;
		}

		if (t >= 1) {
			up = false;
		}

		if (t <= 0) {
			up = true;
		}
	}
}