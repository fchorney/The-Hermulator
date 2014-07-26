using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	private bool alive;
	private ShipAnimationController animationController;

	// Use this for initialization
	void Start () {
		alive = true;
		animationController = transform.GetComponentInChildren<ShipAnimationController>();
	}

	public bool isAlive() {
		return alive;
	}

	public void kill() {
		alive = false;
		animationController.Explode();
	}
}
