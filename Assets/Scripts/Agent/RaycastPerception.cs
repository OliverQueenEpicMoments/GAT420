using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaycastPerception : Perception {
	public Transform RaycastTransform;
	[Range(2, 50)] public int NumRaycast = 2;

	public override GameObject[] GetGameObjects() {
		List<GameObject> Result = new List<GameObject>();

		Vector3[] Directions = Utilities.GetDirectionsInCircle(NumRaycast, MaxAngle);
		foreach (var Direction in Directions) {
			// Cast ray from transform position towards direction
			Ray ray = new Ray(RaycastTransform.position, RaycastTransform.rotation * Direction);
			if (Physics.Raycast(ray, out RaycastHit raycasthit, Distance)) {
				// Don't perceive self
				if (raycasthit.collider.gameObject == gameObject) continue;

				// Check for tag match
				if (TagName == "" || raycasthit.collider.CompareTag(TagName)) {
					// Add game object if ray hit and tag matches
					Result.Add(raycasthit.collider.gameObject);
				}
			}
		}
		// Remove duplicates
		Result = Result.Distinct().ToList();

		// Sort results by distance
		Result.Sort(CompareDistance);

		return Result.ToArray();
	}
}