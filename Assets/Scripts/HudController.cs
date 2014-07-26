using UnityEngine;
using System.Collections;

public class HudController : MonoBehaviour {

	public Texture backgroundTexture;
	public Texture lifeIndicator;

	private int expectedHeight = 127;
	private int expectedLifeWidth = 108;
	private int expectedLifeHeight = 74;

	private Rect bgRect;
	private Rect lifeRect;

	public void Awake() {
		int height = (int)((expectedHeight / 1920.0f) * Screen.height);
		bgRect = new Rect(0, Screen.height - height, Screen.width, height);


		int lifeWidth  = (int)((expectedLifeWidth / 1080.0f) * Screen.width);
		int lifeHeight = (int)((expectedLifeHeight / 1920.0f) * Screen.height);
		int lifeBuffer = (height - lifeHeight) / 2;

		lifeRect = new Rect(Screen.width - lifeWidth - 5, Screen.height - lifeHeight - lifeBuffer, lifeWidth, lifeHeight);
	}

	public void OnGUI() {
		GUI.DrawTexture(bgRect, backgroundTexture);
		GUI.DrawTexture(lifeRect, lifeIndicator);

	}
}
