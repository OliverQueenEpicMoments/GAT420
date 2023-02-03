using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {
    public static Vector3 Wrap(Vector3 V, Vector3 Min, Vector3 Max) {
        Vector3 Result = V;

        if (Result.x > Max.x) { Result.x = Min.x; }
        else if (Result.x < Min.x) { Result.x = Max.x; }

        if (Result.y > Max.y) { Result.y = Min.y; }
        else if (Result.y < Min.y) { Result.y = Max.y; }

        if (Result.z > Max.z) { Result.z = Min.z; }
        else if (Result.z < Min.z) { Result.z = Max.z; }

        return Result;
    }

    public static Vector3 ClampMagnitude(this Vector3 V, float Min, float Max) {
        return V.normalized * Mathf.Clamp(V.magnitude, Min, Max);
    }

	public static Vector3[] GetDirectionsInCircle(int num, float angle)	{
		List<Vector3> Result = new List<Vector3>();

		// if odd number, set first direction as forward (0, 0, 1)
		if (num % 2 == 1) Result.Add(Vector3.forward);

		// compute the angle between rays
		float AngleOffset = (angle * 2) / num;
		// add the +/- directions around the circle
		for (int i = 1; i <= num / 2; i++) {
			float modifier = (i == 1 && num % 2 == 0) ? 0.65f : 1;
			Result.Add(Quaternion.AngleAxis(+AngleOffset * i * modifier, Vector3.up) * Vector3.forward);
			Result.Add(Quaternion.AngleAxis(-AngleOffset * i * modifier, Vector3.up) * Vector3.forward);
		}
		return Result.ToArray();
	}
}