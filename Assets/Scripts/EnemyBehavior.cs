using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBehavior : MonoBehaviour
{
    public float MaxHp;
    public float Hp;
    public float Armor;
    public float Speed;

    public Transform SpriteTransform;
    public float RotationAngleOffset;
    
    public List<Transform> Waypoints = new();

    private Transform _currentWaypoint;
    private bool _dead = false;
    
    public int WaypointIndex { get; private set; }
    public float SqrDistanceToNextWaypoint => (_currentWaypoint.position - transform.position).sqrMagnitude;

    public UnityEvent<EnemyBehavior> OnDie;
    public UnityEvent<EnemyBehavior> OnFinish;
    public UnityEvent<EnemyBehavior> OnTakeDamage;

    private void Start()
    {
        if (Waypoints.Count == 0)
        {
            Debug.LogError("No waypoints");
            return;
        }

        WaypointIndex = 0;
        _currentWaypoint = Waypoints[0];
        
        var animator = GetComponent<Animator>();
        animator.Play("Walking");
        Hp = MaxHp;
    }

    private void FixedUpdate()
    {
        MoveEnemy();
    }
    
    private void MoveEnemy()
    {
        var direction = _currentWaypoint.position - transform.position;
        Helpers.RotateSprite(SpriteTransform, direction, RotationAngleOffset);

        float distanceLeft = direction.sqrMagnitude;
        float travelDistance = Speed * Time.fixedDeltaTime;
        
        transform.Translate(direction.normalized * travelDistance, Space.World);
        if (distanceLeft < travelDistance * travelDistance)
        {
            NextTarget();
        }
    }

    private void NextTarget()
    {
        if (++WaypointIndex >= Waypoints.Count)
        {
            // reached the goal
            OnFinish.Invoke(this);
        }
        else
        {
            _currentWaypoint = Waypoints[WaypointIndex];
        }
    }

    public void TakeDamage(BulletBehavior bullet)
    {
        float damage = bullet.Damage - Armor;
        if (damage < 1) damage = 1; 
        Hp -= damage;
        Hp = Mathf.Clamp(Hp, 0, MaxHp);
        OnTakeDamage.Invoke(this);
        if (Hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (!_dead)
        {
            _dead = true;
            OnDie.Invoke(this);
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}