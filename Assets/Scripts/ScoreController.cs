using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

	private int score;
	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	public void addToScore(int points){
		score += points;
	}
}
