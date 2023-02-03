using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicMovement : Movement {
    public override void ApplyForce(Vector3 Force) {
        Acceleration += Force;
    }

    public override void MoveTowards(Vector3 Target) {
        Vector3 Direction = (Target - transform.position).normalized;
        ApplyForce(Direction * MaxForce);
    }

	public override void Stop() { 
        Velocity = Vector3.zero;
    }

	public override void Resume() {
		
	}

    void LateUpdate() {
        Velocity += Acceleration * Time.deltaTime;
        Velocity = Velocity.ClampMagnitude(MinSpeed, MaxSpeed);
        //Velocity = Utilities.ClampMagnitude(Velocity, MinSpeed, MaxSpeed);
        transform.position += Velocity * Time.deltaTime;

        if (Velocity.sqrMagnitude > 0.1f) {
            transform.rotation = Quaternion.LookRotation(Velocity);
        }
        Acceleration = Vector3.zero;
    }
}