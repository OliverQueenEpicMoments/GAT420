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

    public static Vector3 ClampMagnitude(Vector3 V, float Min, float Max) {
        return V.normalized * Mathf.Clamp(V.magnitude, Min, Max);
    }

    public static Vector3[] GetDirectionsInCircle (int Num, float Angle) {
        List<Vector3> Result = new List<Vector3>();

        // If odd number, set first direction as forward (0, 0, 1) 
        if (Num % 2 != 0) Result.Add(Vector3.forward);

        // Compute angles between rays
        float AngleOffset = Angle / (Num - 1);

        // Add the +/- directions around the circle 
        for (int I = 0; I < Num / 2; I++) {
            Result.Add(Quaternion.AngleAxis(+AngleOffset * I, Vector3.up) * Vector3.forward);
            Result.Add(Quaternion.AngleAxis(-AngleOffset * I, Vector3.up) * Vector3.forward);
        }
		return Result.ToArray();
    }
}
