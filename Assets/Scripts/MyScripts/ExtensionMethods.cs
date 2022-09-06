using System;
using UnityEngine;

public static class ExtensionMethods
{
    public static bool IsInLayerMask(this GameObject obj, LayerMask layerMask)
    {
        return (layerMask.value & (1 << obj.layer)) > 0;
    }

    public static void Clamp(ref this float obj, float min, float max)
    {
        obj = Math.Clamp(obj, min, max);
    }
    public static void Clamp(ref this float obj, Range range)
    {
        obj = Math.Clamp(obj, range.min, range.max);
    }

    public static bool IsBetween(this float obj, float min, float max)
    {
        return obj >= min && obj <= max;
    }
    public static bool IsBetween(this int obj, int min, int max)
    {
        return obj >= min && obj <= max;
    }
}