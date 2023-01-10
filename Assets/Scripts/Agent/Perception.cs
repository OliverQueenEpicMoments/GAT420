using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : MonoBehaviour {
    [Range(0, 40)] public float Distance = 1.0f;
    [Range(0, 180)] public float MaxAngle = 45;

    public GameObject[] GetGameObjects() {
        List<GameObject> result = new List<GameObject>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, Distance);
        foreach (Collider collider in colliders) {
            if (collider.gameObject == gameObject) continue;

            result.Add(collider.gameObject);
        }

        return result.ToArray();
    }
}