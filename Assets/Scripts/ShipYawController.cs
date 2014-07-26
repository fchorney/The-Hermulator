using UnityEngine;
using System.Collections;

public class ShipYawController : MonoBehaviour {

	public Sprite RegularSprite;
	public Sprite YawSprite;
	
	public SpriteRenderer BodyRenderer;
	public Transform HitBox;

	private enum State { NORMAL, LEFT, RIGHT };
	private State yawState;
	private int yawCounter;

	private float lastYawTime;
	private float yawStraightenTime = 0.1f;
	private Vector3 lastPos;
	private int yawOn = 8;
	private int yawOff = 5;
	private int yawMax = 12;

	private float hitboxAdjAmount = 0.08832529f;
	
	private void YawLeft() {
		BodyRenderer.sprite = YawSprite;
		BodyRenderer.transform.localScale = new Vector3(-1, 1, 1);
		yawState = State.LEFT;
		HitBox.localPosition = new Vector3(-hitboxAdjAmount, HitBox.localPosition.y, HitBox.localPosition.z);

		
	}
	
	private void YawRight() {
		BodyRenderer.sprite = YawSprite;
		BodyRenderer.transform.localScale = Vector3.one;
		yawState = State.RIGHT;
		HitBox.localPosition = new Vector3(hitboxAdjAmount, HitBox.localPosition.y, HitBox.localPosition.z);
	}
	
	private void YawNormal() {
		BodyRenderer.sprite = RegularSprite;
		BodyRenderer.transform.localScale = Vector3.one;
		yawState = State.NORMAL;
		HitBox.localPosition = new Vector3(0, HitBox.localPosition.y, HitBox.localPosition.z);
	}

	void Start() {
		lastPos = BodyRenderer.transform.position;
		yawCounter = 0;
		YawNormal();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = BodyRenderer.transform.position;
		Vector3 velocity = position - lastPos;

		//Debug.Log (velocity.x);

		if (velocity.x < -0.01) {
			if (yawCounter > -yawMax)
				yawCounter--;
		} else if (velocity.x > 0.01) {
			if (yawCounter < yawMax)
				yawCounter++;
		} else if (yawCounter > 0) {
			yawCounter--;
		} else if (yawCounter < 0) {
			yawCounter++;
		}

		switch (yawState) {
		case State.NORMAL:
			if (yawCounter > yawOn) 
				YawRight();
			else if (yawCounter < -yawOn)
				YawLeft();
			break;
		case State.RIGHT:
			if (yawCounter < yawOff)
				YawNormal();
			break;
		case State.LEFT:
			if (yawCounter > -yawOff)
				YawNormal();
			break;
		}

		lastPos = position;
	
	}
}
