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
}
