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
	private Rect scoreRect;

	private GameController gameController;

	private int OrigFontSize;
	private int myFontSize;

	private void ResizeRects() {
		Rect pr = Camera.main.pixelRect;
		int sw = (int)pr.width;
		int sh = (int)pr.height;

		int height = (int)((expectedHeight / 1920.0f) * sh);
		bgRect = new Rect(pr.x, pr.y + sh - height, sw, height);

		scoreRect = new Rect (pr.x, pr.y + sh - height, sw, height);
		
		int lifeWidth  = (int)((expectedLifeWidth / 1080.0f) * sw);
		int lifeHeight = (int)((expectedLifeHeight / 1920.0f) * sh);
		int lifeBuffer = (height - lifeHeight) / 2;
		
		lifeRect = new Rect(pr.x + sw - lifeWidth - 5, pr.y + sh - lifeHeight - lifeBuffer, lifeWidth, lifeHeight);

		myFontSize = OrigFontSize / sw;
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
		lifeStyle.fontSize = myFontSize;
		GUI.DrawTexture(bgRect, backgroundTexture);
		GUI.DrawTexture(lifeRect, lifeIndicator);

		GUI.Label(lifeRect, "" + gameController.Lives, lifeStyle);
		GUI.Label(scoreRect, "Score: " + gameController.score, lifeStyle);
		lifeStyle.fontSize = OrigFontSize;
	}
}
