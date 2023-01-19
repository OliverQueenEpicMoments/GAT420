using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavNode : MonoBehaviour {
    public static NavNode[] GetNodes() {
        return FindObjectsOfType<NavNode>();
    }

    public static NavNode GetRandomNode() {
        var Nodes = GetNodes();
        return (Nodes == null) ? null : Nodes[Random.Range(0, Nodes.Length)];
    }
}