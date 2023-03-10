using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;
using System.Linq;

public static class Path {
	public static bool Dijkstra(NavNode start, NavNode end, ref List<NavNode> path) {
		bool found = false;

		// create priority queue
		var nodes = new SimplePriorityQueue<NavNode>();

		// set source cost to 0
		start.Cost = 0;
		// enqueue source node with the source cost as the priority
		nodes.EnqueueWithoutDuplicates(start, start.Cost);

        // update until found or no nodes in queue
        while (!found && nodes.Count > 0) {
			// dequeue node
			var node = nodes.Dequeue(); 

			// check if node is the destination node
			if (node == end) {
				// set found to true
				found = true;
				break;
			}

			foreach (var neighbor in node.Neighbors) {
				// calculate cost to neighbor = node cost + distance to neighbor
				float cost = node.Cost + Vector3.Distance(node.transform.position, neighbor.transform.position);
				// if cost < neighbor cost, add to priority queue
				if (cost < neighbor.Cost) {
					// set neighbor cost to cost
					neighbor.Cost = cost;
					// set neighbor parent to node
					neighbor.Parent = node;
					// enqueue without duplicates neighbor with cost as priority
					nodes.EnqueueWithoutDuplicates(neighbor, neighbor.Cost);
				}
			}
		}

		if (found) {
			// create path from destination to source using node parents
			path = new List<NavNode>();
			CreatePathFromParents(end, ref path);
		} else {
			path = nodes.ToList();
		}

		return found;
	}

    public static bool AStar(NavNode start, NavNode end, ref List<NavNode> path) {
        bool Found = false;

        // create priority queue
        var nodes = new SimplePriorityQueue<NavNode>();

        // set source cost to 0
        start.Cost = 0;
		float Heuristic = Vector3.Distance(start.transform.position, end.transform.position);
        // enqueue source node with the source cost as the priority
        nodes.EnqueueWithoutDuplicates(start, start.Cost + Heuristic);

        // update until found or no nodes in queue
        while (!Found && nodes.Count > 0) {
            // dequeue node
            var Node = nodes.Dequeue();

            // check if node is the destination node
            if (Node == end) {
                // set found to true
                Found = true;
                break;
            }

            foreach (var neighbor in Node.Neighbors) {
                // calculate cost to neighbor = node cost + distance to neighbor
                float cost = Node.Cost + Vector3.Distance(Node.transform.position, neighbor.transform.position);
                // if cost < neighbor cost, add to priority queue
                if (cost < neighbor.Cost) {
                    // set neighbor cost to cost
                    neighbor.Cost = cost;
                    // set neighbor parent to node
                    neighbor.Parent = Node;

                    Heuristic = Vector3.Distance(neighbor.transform.position, end.transform.position);
                    // enqueue without duplicates neighbor with cost as priority
                    nodes.EnqueueWithoutDuplicates(neighbor, neighbor.Cost + Heuristic);
                }
            }
        }

        if (Found) {
            // create path from destination to source using node parents
            path = new List<NavNode>();
            CreatePathFromParents(end, ref path);
        } else {
            path = nodes.ToList();
        }

        return Found;
    }

    public static void CreatePathFromParents(NavNode node, ref List<NavNode> path) {
		// while node not null
		while (node != null) {
			// add node to list path
			path.Add(node);
			// set node to node parent
			node = node.Parent;
		}

		// reverse path
		path.Reverse();
	}
}