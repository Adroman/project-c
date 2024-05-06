using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Variables;

using Random = UnityEngine.Random;


[RequireComponent(typeof(CircleCollider2D))]
public class TowerBehavior : MonoBehaviour
{
    public Transform RotatingPart;
    public float RotationAngleOffset;
    public Transform ShootingPoint;
    public BulletBehavior BulletToSpawn;
    
    public float MinDamage;
    public float MaxDamage;
    public float FireRate;

    public float Range
    {
        get => _collider.radius;
        set => _collider.radius = value;
    }

    public int Price;
    public IntVariable PriceCurrency;

    private CircleCollider2D _collider;
    private readonly List<EnemyBehavior> _enemiesToTarget = new();

    private float _lastFired;
    private float _reloadTime;
    private bool _hasRotatingPart;
    private bool _hasShootingPoint;
    
    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        _lastFired = float.MinValue;
        _reloadTime = 1f / FireRate;
        _hasRotatingPart = RotatingPart != null;
        _hasShootingPoint = ShootingPoint != null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<EnemyBehavior>();
        if (enemy != null)
        {
            _enemiesToTarget.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var enemy = other.GetComponent<EnemyBehavior>();
        if (enemy != null)
        {
            _enemiesToTarget.Remove(enemy);
        }
    }

    private void Update()
    {
        if (_lastFired + _reloadTime > Time.time) return;
        if (_enemiesToTarget.Count == 0) return;

        var target = _enemiesToTarget
            .OrderByDescending(enemy => enemy.WaypointIndex)
            .ThenBy(enemy => enemy.SqrDistanceToNextWaypoint)
            .First();

        var direction = target.transform.position - transform.position;
        
        if (_hasRotatingPart)
            Helpers.RotateSprite(RotatingPart, direction, RotationAngleOffset);
        
        Fire(target);
    }

    private void Fire(EnemyBehavior target)
    {
        var point = _hasShootingPoint ? ShootingPoint : transform;
        var bullet = Instantiate(BulletToSpawn, point.position, point.rotation);
        bullet.Damage = Random.Range(MinDamage, MaxDamage);
        bullet.Target = target;

        _lastFired = Time.time;
    }
}