using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistancePerception : Perception {
	public override GameObject[] GetGameObjects() {
		List<GameObject> Result = new List<GameObject>();

		Collider[] Colliders = Physics.OverlapSphere(transform.position, Distance);
		foreach (Collider collider in Colliders) {
			if (collider.gameObject == gameObject) continue;
			if (TagName == "" || collider.CompareTag(TagName)) {
				Vector3 Direction = (collider.transform.position - transform.position).normalized;
				float Angle = Vector3.Angle(transform.forward, Direction);

				if (Angle <= MaxAngle) {
					Result.Add(collider.gameObject);
				}
			}
		}
		Result.Sort(CompareDistance);

		return Result.ToArray();
	}
}
