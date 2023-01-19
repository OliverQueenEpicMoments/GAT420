using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    [Range(1, 10)] public float MaxSpeed = 5;
    [Range(1, 10)] public float MinSpeed = 5;
    [Range(1, 100)] public float MaxForce = 5;

    public Vector3 Velocity { get; set; } = Vector3.zero;
    public Vector3 Acceleration { get; set; } = Vector3.zero;
    public Vector3 Direction { get { return Velocity.normalized; } }

    public void ApplyForce(Vector3 Force) {
        Acceleration += Force;
    }

    public void MoveTowards(Vector3 target) {
        Vector3 direction = (target - transform.position).normalized;
        ApplyForce(direction * MaxForce);
    }

    void LateUpdate() {
        Velocity += Acceleration * Time.deltaTime;
        Velocity = Utilities.ClampMagnitude(Velocity, MinSpeed, MaxSpeed);

        transform.position += Velocity * Time.deltaTime;
        if (Velocity.sqrMagnitude > 0.1f) {
            transform.rotation = Quaternion.LookRotation(Velocity);
        }
        Acceleration = Vector3.zero;
    }

    void Start() {
        
    }

    void Update() {
        
    }
}