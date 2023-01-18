using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent {
    public Perception FlockPerception;
    public ObstaclePerception obstaclePerception;
    public AutonomousAgentData Data;

    public float WanderAngle { get; set; } = 0;

    void Update() {
        var GameObjects = perception.GetGameObjects();

        // Seek/Flee
        if (GameObjects.Length > 0) { 
            movement.ApplyForce(Steering.Seek(this, GameObjects[0]) * Data.SeekWeight);
            movement.ApplyForce(Steering.Flee(this, GameObjects[0]) * Data.FleeWeight);
        }

        GameObjects = FlockPerception.GetGameObjects();
        if (GameObjects.Length > 0) {
			foreach (var GameObject in GameObjects) {
				Debug.DrawLine(transform.position, GameObject.transform.position);
			}

			movement.ApplyForce(Steering.Cohesion(this, GameObjects) * Data.CohesionWeight);
            movement.ApplyForce(Steering.Separation(this, GameObjects, Data.SeparationRadius) * Data.SeparationWeight);
            movement.ApplyForce(Steering.Allignment(this, GameObjects) * Data.AlignmentWeight);
        }

        // Obstacle Avoidance
        if (obstaclePerception.IsObstacleInFront()) {
            Vector3 Direction = obstaclePerception.GetOpenDirection();
            movement.ApplyForce(Steering.CalculateSteering(this, Direction) * Data.ObstacleWeight);
        }

        // Wander
        if (movement.Acceleration.sqrMagnitude <= movement.MaxForce * 0.1f) {
            movement.ApplyForce(Steering.Wander(this));
        }
        transform.position = Utilities.Wrap(transform.position, new Vector3 (-20, -20, -20), new Vector3(20, 20, 20));
    }
}