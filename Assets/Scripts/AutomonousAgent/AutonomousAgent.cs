using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent {
    void Update() {
        var GameObjects = perception.GetGameObjects();
        foreach (var GameObject in GameObjects) {
            Debug.DrawLine(transform.position, GameObject.transform.position);
        }

        if (GameObjects.Length >= 1) {
            Vector3 Direction = (GameObjects[0].transform.position - transform.position).normalized;
            movement.ApplyForce(Direction * 2);
        }

        transform.position = Utilities.Wrap(transform.position, new Vector3 (-10, -10, -10), new Vector3(10, 10, 10));
    }
}