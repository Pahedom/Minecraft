using System;
using UnityEngine;

public static class ExtensionMethods
{
    // float
    public static void Clamp(ref this float obj, float min, float max)
    {
        obj = Mathf.Clamp(obj, min, max);
    }
    public static void Clamp(ref this float obj, Range range)
    {
        obj = Mathf.Clamp(obj, range.min, range.max);
    }
    public static void Round(ref this float obj, int digits)
    {
        obj = MathF.Round(obj, digits);
    }
    public static void Round(ref this float obj)
    {
        obj = MathF.Round(obj);
    }
    public static void Snap(ref this float obj, float interval)
    {
        obj = MathF.Round(obj / interval) * interval;
    }
    public static bool IsBetween(this float obj, float min, float max)
    {
        return obj >= min && obj <= max;
    }

    // int
    public static bool IsBetween(this int obj, int min, int max)
    {
        return obj >= min && obj <= max;
    }

    // Vector3
    public static void Round(ref this Vector3 obj, int digits)
    {
        obj.x = MathF.Round(obj.x, digits);
        obj.y = MathF.Round(obj.y, digits);
        obj.z = MathF.Round(obj.z, digits);
    }
    public static void Round(ref this Vector3 obj)
    {
        obj.x = MathF.Round(obj.x);
        obj.y = MathF.Round(obj.y);
        obj.z = MathF.Round(obj.z);
    }

    // GameObject
    public static bool IsInLayerMask(this GameObject obj, LayerMask layerMask)
    {
        return (layerMask.value & (1 << obj.layer)) > 0;
    }
}