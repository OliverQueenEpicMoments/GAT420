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
				float Cos = Vector3.Dot(transform.forward, Direction);
				float Angle = Mathf.Acos(Cos) * Mathf.Rad2Deg;

				if (Angle <= MaxAngle) {
					Result.Add(collider.gameObject);
				}
			}
		}
		Result.Sort(CompareDistance);

		return Result.ToArray();
	}

	void Start() {
        
    }

    void Update() {
        
    }
}
