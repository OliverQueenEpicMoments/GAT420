using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Steering {
    public static Vector3 Wander(AutonomousAgent agent) {
        // Randomly adjusts angle +/- displacement
        agent.WanderAngle = agent.WanderAngle + UnityEngine.Random.Range(-agent.WanderDisplacement, agent.WanderDisplacement);

        // Create rotation quaternion around y-axis (up)
        Quaternion Rotation = Quaternion.AngleAxis(agent.WanderAngle, Vector3.up);

        // Calculate point on circle radius 
        Vector3 Point = Rotation * (Vector3.forward * agent.WanderRadius);

        // Set point in front of agent at distance length 
        Vector3 Forward = agent.transform.forward * agent.WanderDistance;

        Vector3 Force = CalculateSteering(agent, Forward + Point);

        return Force;
    }

    public static Vector3 Seek(Agent agent, GameObject target) {
        Vector3 Force = CalculateSteering(agent, (target.transform.position - agent.transform.position));
        return Force;
    }

    public static Vector3 Flee(Agent agent, GameObject target) {
        Vector3 Force = CalculateSteering(agent, (agent.transform.position - target.transform.position));
        return Force;
    }

    public static Vector3 CalculateSteering(Agent agent, Vector3 direction) {
        Vector3 Desired = direction.normalized * agent.movement.MaxSpeed;
        Vector3 Steer = Desired - agent.movement.Velocity;
        Vector3 Force = Vector3.ClampMagnitude(Steer, agent.movement.MaxForce);

        return Force;
    }
}
