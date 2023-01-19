using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perception : MonoBehaviour {
    public string TagName = "";
    [Range(1, 40)] public float Distance = 1.0f;
    [Range(1, 180)] public float MaxAngle = 45;

    public abstract GameObject[] GetGameObjects();

	public void SortByDistance(List<GameObject> GameObjects) {
		GameObjects.Sort(CompareDistance);
	}

    public int CompareDistance(GameObject A, GameObject B) {
        float SquaredRangeA = (A.transform.position - transform.position).sqrMagnitude;
        float SquaredRangeB = (B.transform.position - transform.position).sqrMagnitude;
        return SquaredRangeA.CompareTo(SquaredRangeB);
    }
}