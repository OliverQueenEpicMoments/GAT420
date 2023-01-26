using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class NavPath : MonoBehaviour {
	List<NavNode> path = new List<NavNode>();

	public NavNode StartNode { get; set; }
	public NavNode EndNode { get; set; }

	public void StartPath()	{
		GeneratePath();
	}

	public NavNode GetNextNode(NavNode navNode)	{
		if (path.Count == 0) return null;

		int Index = path.FindIndex(node => node == navNode);
		// check if noode index is at the end of the path
		if (Index == path.Count - 1) {
			SetRandomEndNode();
			// generate new path
			GeneratePath();

			Index = 0;
		}

		// get the next node using index + 1
		NavNode NextNode = path[Index + 1];

		return NextNode;
	}

	private void SetRandomEndNode()	{
		// set the start node to the current end node
		StartNode = EndNode;
		// find a new random end node that isn't the start node
		do {
			EndNode = NavNode.GetRandomNode();
		} while (StartNode == EndNode);
	}

	private void GeneratePath() {
		NavNode.ResetNodes();
		Path.Dijkstra(StartNode, EndNode, ref path);
	}

	private void OnDrawGizmos()	{
		foreach (NavNode Node in path) {
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(Node.gameObject.transform.position, Node.Radius);
		}
		if (StartNode != null) Gizmos.DrawIcon(StartNode.transform.position + Vector3.up, "nav_nodeA.png", true, Color.green);
		if (EndNode != null) Gizmos.DrawIcon(EndNode.transform.position + Vector3.up, "nav_nodeA.png", true, Color.red);
	}
}