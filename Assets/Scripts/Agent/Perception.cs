using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : MonoBehaviour {
    [Range(0, 40)] public float Distance = 1.0f;
    [Range(0, 180)] public float MaxAngle = 45;

    public string TagName = "";

    public GameObject[] GetGameObjects() {
        List<GameObject> result = new List<GameObject>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, Distance);
        foreach (Collider collider in colliders) {
            if (collider.gameObject == gameObject) continue;
            if (TagName == "" || collider.CompareTag(TagName)) {
                result.Add(collider.gameObject);
            }

            Vector3 Direction = (collider.transform.position - transform.position).normalized;
            float Cos = Vector3.Dot(transform.forward, Direction);
            float Angle = Mathf.Acos(Cos) * Mathf.Rad2Deg;

            if (Angle <= MaxAngle) { 
                result.Add(collider.gameObject);
            }
        }
        return result.ToArray();
    }
}