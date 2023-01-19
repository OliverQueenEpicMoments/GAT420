using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavAgent : MonoBehaviour {
    [SerializeField] NavNode startNode;
    public NavNode targetNode { get; set; }

    void Start()
    {
        targetNode = (startNode != null) ? startNode : NavNode.GetRandomNode();
    }

    void Update()
    {
        if (targetNode != null)
        {
            //Movement.MoveTowards(targetNode.transform.position);
        }
    }
}
