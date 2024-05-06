using UnityEngine;

public static class Helpers
{
    public static void RotateSprite(Transform transform, Vector3 direction, float offset)
    {
        if (direction.sqrMagnitude < float.Epsilon) return;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + offset, Vector3.forward);
    } 
}