using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NavAgent : Agent {
    [SerializeField] NavPath navpath;
    [SerializeField] NavNode NavPathStartNode;
    [SerializeField] NavNode NavPathEndNode;

    public NavNode TargetNode { get; set; }

    void Start() {
        if (navpath == null) TargetNode = NavNode.GetRandomNode();
        else {
            navpath.StartNode = (NavPathStartNode != null) ? NavPathStartNode : GetNearestNode();
            navpath.EndNode = (NavPathEndNode != null) ? NavPathEndNode : NavNode.GetRandomNode();
            navpath.StartPath();
            TargetNode = navpath.StartNode;
        }   
    }

    void Update() {
        if (TargetNode != null) {
            movement.MoveTowards(TargetNode.transform.position);
        } else { 
            movement.Stop();
        }
    }

    public NavNode GetNextTarget(NavNode Node) {
        if (navpath == null) return Node.Neighbors[Random.Range(0, Node.Neighbors.Count)];
        else return navpath.GetNextNode(Node);
    }

    public NavNode GetNearestNode() {
        var Nodes = NavNode.GetNodes().ToList();
        SortByDistance(Nodes);

        return (Nodes.Count == 0) ? null : Nodes[0];
    }


    public void SortByDistance(List<NavNode> Nodes) {
        Nodes.Sort(CompareDistance);
    }

    public int CompareDistance(NavNode A, NavNode B) {
        float SquaredRangeA = (A.transform.position - transform.position).sqrMagnitude;
        float SquaredRangeB = (B.transform.position - transform.position).sqrMagnitude;
        return SquaredRangeA.CompareTo(SquaredRangeB);
    }

    public void SetDestination(NavNode destinationNode) {
        navpath.StartNode = GetNearestNode();
        navpath.EndNode = destinationNode;
        navpath.StartPath();
        TargetNode = navpath.StartNode;
    }
}