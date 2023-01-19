using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent {
    public Perception FlockPerception;
    public ObstaclePerception obstaclePerception;
    public AutonomousAgentData Data;

    public float WanderAngle { get; set; } = 0;

    void Update() {
        // Seek/Flee
        var GameObjects = perception.GetGameObjects();
        if (GameObjects.Length > 0) { 
            movement.ApplyForce(Steering.Seek(this, GameObjects[0]) * Data.SeekWeight);
            movement.ApplyForce(Steering.Flee(this, GameObjects[0]) * Data.FleeWeight);
        }

        // Flocking
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
        Vector3 Position = transform.position;
        Position = Utilities.Wrap(Position, new Vector3(-20, -20, -20), new Vector3(20, 20, 20));
        Position.y = 0;
        transform.position = Position;
    }
}