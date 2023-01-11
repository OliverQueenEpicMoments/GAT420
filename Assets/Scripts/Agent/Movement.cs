using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    [Range(0, 10)] public float MaxSpeed = 5;

    public Vector3 Velocity { get; set; } = Vector3.zero;
    public Vector3 Acceleration { get; set; } = Vector3.zero;

    public void ApplyForce(Vector3 Force) {
        Acceleration += Force;
    }

    void LateUpdate() {
        Velocity += Acceleration * Time.deltaTime;
        Velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);
        transform.position += Velocity * Time.deltaTime;

        Acceleration = Vector3.zero;

        if (Velocity.sqrMagnitude > 0.1f) {
            transform.rotation = Quaternion.LookRotation(Velocity);
        }
    }

    void Start() {
        
    }

    void Update() {
        
    }
}