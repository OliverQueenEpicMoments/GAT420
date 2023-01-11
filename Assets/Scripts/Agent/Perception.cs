using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : MonoBehaviour {
    [Range(0, 40)] public float Distance = 1.0f;
    [Range(0, 180)] public float MaxAngle = 45;

    public string TagName = "";

    public GameObject[] GetGameObjects() {
        List<GameObject> Result = new List<GameObject>();

        Collider[] Colliders = Physics.OverlapSphere(transform.position, Distance);
        foreach (Collider collider in Colliders) {
            if (collider.gameObject == gameObject) continue;
            if (TagName == "" || collider.CompareTag(TagName)) {
                Vector3 Direction = (collider.transform.position - transform.position).normalized;
                float Cos = Vector3.Dot(transform.forward, Direction);
                float Angle = Mathf.Acos(Cos) * Mathf.Rad2Deg;

                if (Angle <= MaxAngle) {
                    Result.Add(collider.gameObject);
                }
            }
        }
        Result.Sort(CompareDistance);

        return Result.ToArray();
    }

    public int CompareDistance(GameObject a, GameObject b) {
        float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
        float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
        return squaredRangeA.CompareTo(squaredRangeB);
    }
}