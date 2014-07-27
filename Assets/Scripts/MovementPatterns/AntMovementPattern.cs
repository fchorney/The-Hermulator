using UnityEngine;
using System.Collections;

public class AntMovementPattern : EnemyMovementPattern {

	public Transform HeadJoint;
	public Transform LeftAntennaJoint1, LeftAntennaJoint2;
	public Transform RightAntennaJoint1, RightAntennaJoint2;

	public Transform LeftPincerJoint, RightPincerJoint;
	
	private Quaternion targetRotation;
	private float rotSpeed = 1f;
	
	public override void Move(GameObject self) {
		Transform target = null;

		if (player != null) 
		{
			ShipController sc = player.GetComponent<ShipController>();

			if (sc && sc.Target != null) {
				target = sc.Target.transform;
			}
		}

		if (target != null) {
			Vector3 dir = target.position - HeadJoint.position;
			float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg + 90;
			targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
		} else {
			targetRotation = Quaternion.AngleAxis(0, Vector3.forward);
		}

		HeadJoint.transform.rotation = Quaternion.Slerp(HeadJoint.transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
	
		float t = Time.time - activeTime;

		LeftAntennaJoint1.localRotation = RightAntennaJoint1.localRotation = Quaternion.Euler(new Vector3(0, 0, 20*Mathf.Sin (t/7f * Mathf.PI * 2)));
		LeftAntennaJoint2.localRotation = RightAntennaJoint2.localRotation = Quaternion.Euler(new Vector3(0, 0, 30*Mathf.Sin ((t+Mathf.PI/2)/5f * Mathf.PI * 2)));


		LeftPincerJoint.localRotation = Quaternion.Euler(new Vector3(0, 0, 15*Mathf.Sin (t/13f * Mathf.PI * 2 ) - 10));
		RightPincerJoint.localRotation = Quaternion.Euler(new Vector3(0, 0, 15*Mathf.Sin (t/13f * Mathf.PI * 2 + Mathf.PI) + 10));
	}
}
