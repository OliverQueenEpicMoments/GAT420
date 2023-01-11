using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent {
    void Update() {
        var GameObjects = perception.GetGameObjects();
        foreach (var GameObject in GameObjects) {
            Debug.DrawLine(transform.position, GameObject.transform.position);
        }

        if (GameObjects.Length > 0) {
            movement.ApplyForce(Steering.Seek(this, GameObjects[0]) * 0);
            movement.ApplyForce(Steering.Flee(this, GameObjects[0]) * 1);
        }
        transform.position = Utilities.Wrap(transform.position, new Vector3 (-10, -10, -10), new Vector3(10, 10, 10));
    }
}