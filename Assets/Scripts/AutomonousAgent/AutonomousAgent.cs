using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent {
    public Perception FlockPerception;

    [Range(0, 3)] public float SeekWeight;
    [Range(0, 3)] public float FleeWeight;

    [Range(0, 3)] public float CohesionWeight;
    [Range(0, 3)] public float SeparationWeight;
    [Range(0, 3)] public float AllignmentWeight;

    [Range(0, 10)] public float SeperationRadius;

    public float WanderDistance = 1;
    public float WanderRadius = 3;
    public float WanderDisplacement = 5;

    public float WanderAngle { get; set; } = 0;

    void Update() {
        var GameObjects = perception.GetGameObjects();

        if (GameObjects.Length > 0) { 
            movement.ApplyForce(Steering.Seek(this, GameObjects[0]) * SeekWeight);
            movement.ApplyForce(Steering.Flee(this, GameObjects[0]) * FleeWeight);
        }

        GameObjects = FlockPerception.GetGameObjects();
        if (GameObjects.Length > 0) {
			foreach (var GameObject in GameObjects) {
				Debug.DrawLine(transform.position, GameObject.transform.position);
			}

			movement.ApplyForce(Steering.Cohesion(this, GameObjects) * CohesionWeight);
            movement.ApplyForce(Steering.Separation(this, GameObjects, SeperationRadius) * SeparationWeight);
            movement.ApplyForce(Steering.Allignment(this, GameObjects) * AllignmentWeight);
        }

        if (movement.Acceleration.sqrMagnitude <= movement.MaxForce * 0.1f) {
            movement.ApplyForce(Steering.Wander(this));
        }
        transform.position = Utilities.Wrap(transform.position, new Vector3 (-10, -10, -10), new Vector3(10, 10, 10));
    }
}