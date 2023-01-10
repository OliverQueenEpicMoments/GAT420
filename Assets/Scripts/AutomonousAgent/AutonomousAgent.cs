using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent {
    void Update() {
        var GameObjects = perception.GetGameObjects();
        foreach (var GameObject in GameObjects) {
            Debug.DrawLine(transform.position, GameObject.transform.position);
        }
    }
}