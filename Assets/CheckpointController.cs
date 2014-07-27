using UnityEngine;
using System.Collections;

public class CheckpointController : MonoBehaviour {

	public GameObject WaitForKill;

	public bool CheckpointComplete { get; private set; }

	// Use this for initialization
	void Start () {
		CheckpointComplete = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (WaitForKill == null)
			CheckpointComplete = true;
	
	}
}
