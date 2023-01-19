using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ObstaclePerception : MonoBehaviour {
	public Transform RaycastTransform;
	[Range(1, 40)] public float Distance = 1;
	[Range(0, 180)] public float MaxAngle = 45;
	[Range(2, 50)] public int NumRaycast = 2;

	public LayerMask layermask;

	public bool IsObstacleInFront() {
		// Check if object is in front fo agent
		Ray ray = new Ray(RaycastTransform.position, RaycastTransform.forward);

		Debug.DrawRay(ray.origin, ray.direction * Distance, Color.green);
		return Physics.SphereCast(ray, 2, Distance, layermask);
	}

	public Vector3 GetOpenDirection() {
		Vector3[] Directions = Utilities.GetDirectionsInCircle(NumRaycast, MaxAngle);
		foreach (Vector3 Direction in Directions) {
			Ray ray = new Ray(RaycastTransform.position, RaycastTransform.rotation * Direction);
			if (!Physics.SphereCast(ray, 2, Distance, layermask)) {
				Debug.DrawRay(ray.origin, ray.direction * Distance, Color.white);
				return ray.direction;
			} else {
				Debug.DrawRay(ray.origin, ray.direction * Distance, Color.red);
			}
		}
		return transform.forward;
	}
}