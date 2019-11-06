using System;
using UnityEngine;

public static class Util {
    
    public const double TOLERANCE = 0.0002;
    public static bool FloatEquals(double d1, double d2) => Math.Abs(d1 - d2) < TOLERANCE;
}