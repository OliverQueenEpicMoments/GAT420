using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Steering {
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
