using UnityEngine;
using System.Collections;

public class HudController : MonoBehaviour {

	public Texture backgroundTexture;
	public Texture lifeIndicator;

	public GUIStyle lifeStyle;

	private int expectedHeight = 127;
	private int expectedLifeWidth = 199;
	private int expectedLifeHeight = 121;

	private Rect bgRect;
	private Rect lifeRect;

	private GameController gameController;
	private int OrigFontSize;

	private void ResizeRects() {
		int height = (int)((expectedHeight / 1920.0f) * Screen.height);
		bgRect = new Rect(0, Screen.height - height, Screen.width, height);
		
		
		int lifeWidth  = (int)((expectedLifeWidth / 1080.0f) * Screen.width);
		int lifeHeight = (int)((expectedLifeHeight / 1920.0f) * Screen.height);
		int lifeBuffer = (height - lifeHeight) / 2;
		
		lifeRect = new Rect(Screen.width - lifeWidth - 5, Screen.height - lifeHeight - lifeBuffer, lifeWidth, lifeHeight);

		lifeStyle.fontSize = OrigFontSize / Screen.width;
	}

	public void Awake() {
		OrigFontSize = lifeStyle.fontSize;
		ResizeRects();
	}

	public void Start() {
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	public void Update() {
		if (Application.isEditor)
			ResizeRects();
	}

	public void OnGUI() {
		GUI.DrawTexture(bgRect, backgroundTexture);
		GUI.DrawTexture(lifeRect, lifeIndicator);

		GUI.Label(lifeRect, "" + gameController.Lives, lifeStyle);

	}
}
