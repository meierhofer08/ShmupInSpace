using System;

public static class FloatExtensions
{
    public static bool AlmostEquals(this float float1, float float2, double precision)
    {
        return (Math.Abs(float1 - float2) <= precision);
    }
}