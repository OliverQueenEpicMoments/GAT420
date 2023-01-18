using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour {
    public Agent[] Agents;
    public LayerMask layermask;

    int Index = 0;

    void Update() {
        if (Input.GetKey(KeyCode.Alpha1)) Index = 0;
        if (Input.GetKey(KeyCode.Alpha2)) Index = 1;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl)) { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit HitInfo, 100, layermask)) {
                Instantiate(Agents[Index], HitInfo.point, Quaternion.identity);
            }
        }
    }
}
