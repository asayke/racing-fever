using UnityEngine;

public static class Tools
{
    public static bool IsInLayerMask(this GameObject obj, LayerMask layerMask) => ((layerMask.value & (1 << obj.layer)) > 0);
}