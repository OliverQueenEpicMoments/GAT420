using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class WaypointNavNode : NavNode {
    [SerializeField] private NavNode[] Nodes;
    [SerializeField, Range(1, 10)] private float Radius = 1;

    private void OnValidate() {
        GetComponent<SphereCollider>().radius = Radius;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent<NavAgent>(out NavAgent NavAgent)) {
            if (NavAgent.TargetNode == this) {
                NavAgent.TargetNode = Nodes[Random.Range(0, Nodes.Length)];
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.TryGetComponent<NavAgent>(out NavAgent NavAgent)) {
            if (NavAgent.TargetNode == this) {
                NavAgent.TargetNode = Nodes[Random.Range(0, Nodes.Length)];
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Radius);

        Gizmos.color = Color.green;
        foreach (NavNode Node in Nodes) {
            Gizmos.DrawLine(transform.position, Node.transform.position);
        }
    }
}