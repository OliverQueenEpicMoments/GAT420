using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour {
    public Agent agent;
    public LayerMask layermask;

    void Start() {
        
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit HitInfo, 100, layermask)) {
                Instantiate(agent, HitInfo.point, Quaternion.identity);
            }
        }
    }
}
