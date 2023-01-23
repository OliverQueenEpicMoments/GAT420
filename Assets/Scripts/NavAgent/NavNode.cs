using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavNode : MonoBehaviour {
	[SerializeField] public List<NavNode> Neighbors = new List<NavNode>();
	[SerializeField, Range(1, 10)] public float Radius = 1;

	private void OnValidate() {
		GetComponent<SphereCollider>().radius = Radius;
	}

	public static NavNode[] GetNodes() {
        return FindObjectsOfType<NavNode>();
    }

    public static NavNode GetRandomNode() {
        var Nodes = GetNodes();
        return (Nodes == null) ? null : Nodes[Random.Range(0, Nodes.Length)];
    }

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, Radius);

		Gizmos.color = Color.green;
		foreach (NavNode Node in Neighbors) {
			Gizmos.DrawLine(transform.position, Node.transform.position);
		}
	}
}