using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class WaypointNavNode : NavNode {

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent<NavAgent>(out NavAgent navagent)) {
            if (navagent.TargetNode == this && Neighbors.Count > 0) {
                navagent.TargetNode = navagent.GetNextTarget(navagent.TargetNode);
            }
        }

		if (other.gameObject.TryGetComponent<Navigation>(out Navigation navigation)) {
			if (navigation.targetNode == this && Neighbors.Count > 0) {
				navigation.targetNode = navigation.GetNextNavTarget(navigation.targetNode);
			}
		}
	}

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.TryGetComponent<NavAgent>(out NavAgent navagent)) {
            if (navagent.TargetNode == this && Neighbors.Count > 0) {
                navagent.TargetNode = navagent.GetNextTarget(navagent.TargetNode);
            }
        }

		if (other.gameObject.TryGetComponent<Navigation>(out Navigation navigation)) {
			if (navigation.targetNode == this && Neighbors.Count > 0) {
				navigation.targetNode = navigation.GetNextNavTarget(navigation.targetNode);
			}
		}
	}
}