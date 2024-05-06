using System;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float Damage;
    public float Speed;
    public EnemyBehavior Target;

    public float RotationAngleOffset;

    private Transform _thisTransform;

    private void Awake()
    {
        _thisTransform = transform;
    }

    public void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }

        var direction = Target.transform.position - _thisTransform.position;
        Helpers.RotateSprite(_thisTransform, direction, RotationAngleOffset);

        float sqrDistanceLeft = direction.sqrMagnitude;
        float travelDistance = Speed * Time.deltaTime;
        
        transform.Translate(direction.normalized * travelDistance, Space.World);
        if (sqrDistanceLeft < travelDistance * travelDistance)
        {
            EnemyHit(Target);
            Destroy(gameObject);
        }
    }

    private void EnemyHit(EnemyBehavior enemy)
    {
        enemy.TakeDamage(this);
    }
}