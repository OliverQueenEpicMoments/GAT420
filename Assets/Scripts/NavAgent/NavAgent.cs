using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavAgent : Agent {
    [SerializeField] NavNode StartNode;
    public NavNode TargetNode { get; set; }

    void Start() {
        TargetNode = (StartNode != null) ? StartNode : NavNode.GetRandomNode();
    }

    void Update() {
        if (TargetNode != null) {
            movement.MoveTowards(TargetNode.transform.position);
        }
    }
}
