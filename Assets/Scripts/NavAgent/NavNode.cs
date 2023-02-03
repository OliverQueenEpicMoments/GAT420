using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NavNode : MonoBehaviour {
	[SerializeField] public List<NavNode> Neighbors = new List<NavNode>();
	[SerializeField, Range(1, 10)] public float Radius = 1;

    public NavNode Parent { get; set; } = null;
    public bool Visited { get; set; } = false;
    public float Cost { get; set; } = float.MaxValue;

    private void OnValidate() {
		GetComponent<SphereCollider>().radius = Radius;
	}

	public static NavNode[] GetNodes() {
        return FindObjectsOfType<NavNode>();
    }

	public static NavNode[] GetNodesWithTag(string tag)	{
		var gameObjects = GameObject.FindGameObjectsWithTag(tag);
		List<NavNode> nodes = new List<NavNode>();
		foreach (var go in gameObjects) {
			if (go.TryGetComponent<NavNode>(out NavNode navNode)) {
				nodes.Add(navNode);
			}
		}
		return nodes.ToArray();
	}

	public static NavNode GetRandomNode() {
        var Nodes = GetNodes();
        return (Nodes == null) ? null : Nodes[Random.Range(0, Nodes.Length)];
    }

    public static void ShowNodes() {
        var Nodes = GetNodes();
        Nodes.ToList().ForEach(node => node.GetComponent<Renderer>().enabled = true);
    }

    public static void HideNodes() {
        var Nodes = GetNodes();
        Nodes.ToList().ForEach(node => node.GetComponent<Renderer>().enabled = false);
    }

    public static void ResetNodes() {
        var Nodes = GetNodes();
        Nodes.ToList().ForEach(Node => {
            Node.Visited = false;
            Node.Parent = null;
            Node.Cost = float.MaxValue;
        });
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