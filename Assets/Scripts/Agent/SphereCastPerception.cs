using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCastPerception : Perception {
	public Transform RaycastTransform;
	[Range(2, 50)] public int NumRaycast = 2;
	[Range(0.1f, 10)] public float Radius = 1;

	public override GameObject[] GetGameObjects() {
		List<GameObject> Result = new List<GameObject>();

		Vector3[] Directions = Utilities.GetDirectionsInCircle(NumRaycast, MaxAngle);
		foreach (var Direction in Directions) {
			// Cast ray from transform position towards direction
			Ray ray = new Ray(RaycastTransform.position, RaycastTransform.rotation * Direction);

			//Debug.DrawRay(ray.origin, ray.direction * Distance);
			if (Physics.SphereCast(ray, Radius, out RaycastHit raycasthit, Distance)) {
				// Don't perceive self
				if (raycasthit.collider.gameObject == gameObject) continue;

				// Check for tag match
				if (TagName == "" || raycasthit.collider.CompareTag(TagName)) {
					Debug.DrawRay(ray.origin, ray.direction * raycasthit.distance, Color.red);
					// Add game object if ray hit and tag matches
					Result.Add(raycasthit.collider.gameObject);
				} else {
					Debug.DrawRay(ray.origin, ray.direction * raycasthit.distance, Color.green);
				}
			} else {
				Debug.DrawRay(ray.origin, ray.direction * Distance);
			}
		}
		// Sort results by distance
		Result.Sort(CompareDistance);

		return Result.ToArray();
	}
}