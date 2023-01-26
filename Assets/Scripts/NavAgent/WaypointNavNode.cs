using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class WaypointNavNode : NavNode {

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent<NavAgent>(out NavAgent NavAgent)) {
            if (NavAgent.TargetNode == this && Neighbors.Count > 0) {
                NavAgent.TargetNode = NavAgent.GetNextTarget(NavAgent.TargetNode);
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.TryGetComponent<NavAgent>(out NavAgent NavAgent)) {
            if (NavAgent.TargetNode == this && Neighbors.Count > 0) {
                NavAgent.TargetNode = NavAgent.GetNextTarget(NavAgent.TargetNode);
            }
        }
    }
}