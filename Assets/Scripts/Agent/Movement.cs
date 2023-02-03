using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour {
    [Range(1, 10)] public float MaxSpeed = 5;
    [Range(1, 10)] public float MinSpeed = 5;
    [Range(1, 100)] public float MaxForce = 5;
    [Range(1, 180)] public float TurnRate = 90;

    public virtual Vector3 Velocity { get; set; } = Vector3.zero;
    public virtual Vector3 Acceleration { get; set; } = Vector3.zero;
    public virtual Vector3 Direction { get { return Velocity.normalized; } }
    public virtual Vector3 Destination { get; set; } = Vector3.zero;

	public abstract void ApplyForce(Vector3 Force);
    public abstract void MoveTowards(Vector3 Target);
    public abstract void Stop();
    public abstract void Resume();
}