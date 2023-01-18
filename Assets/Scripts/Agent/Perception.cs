using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perception : MonoBehaviour {
    public string TagName = "";
    [Range(1, 40)] public float Distance = 1.0f;
    [Range(1, 180)] public float MaxAngle = 45;

    public abstract GameObject[] GetGameObjects();

    public int CompareDistance(GameObject A, GameObject B) {
        float squaredRangeA = (A.transform.position - transform.position).sqrMagnitude;
        float squaredRangeB = (B.transform.position - transform.position).sqrMagnitude;
        return squaredRangeA.CompareTo(squaredRangeB);
    }

	public void SortByDistance(List<GameObject> GameObjects) {
		GameObjects.Sort(CompareDistance);
	}
}