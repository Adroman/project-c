using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Variables;
using Random = UnityEngine.Random;


namespace Towers
{
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
            set
            {
                _collider.radius = value;
                AdjustRendererPositions(value);
            }
        }

        public int Price;
        public IntVariable PriceCurrency;

        public BoolVariable IsUnlocked;
        public TowerBehavior UpgradedTower;

        private CircleCollider2D _collider;
        private LineRenderer _rangeRenderer;
        private readonly List<EnemyBehavior> _enemiesToTarget = new();

        private float _lastFired;
        private float _reloadTime;
        private bool _hasRotatingPart;
        private bool _hasShootingPoint;
    
        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
            _rangeRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            _lastFired = float.MinValue;
            _reloadTime = 1f / FireRate;
            _hasRotatingPart = RotatingPart != null;
            _hasShootingPoint = ShootingPoint != null;
            AdjustRendererPositions(Range);
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

        public bool CanBuildIt()
        {
            if (IsUnlocked != null && !IsUnlocked.IsEnabled)
                return false;
            return PriceCurrency.IsAtLeast(Price);
        }

        public void BuyTower()
        {
            PriceCurrency.SubtractValue(Price);
        }

        private void AdjustRendererPositions(float range)
        {
            _rangeRenderer.positionCount = 36;
            _rangeRenderer.loop = true;
            for (int i = 0; i < 36; i++)
            {
                float degree =  10 * i * Mathf.Deg2Rad;
                _rangeRenderer.SetPosition(i, transform.position + new Vector3(range * Mathf.Cos(degree), range * Mathf.Sin(degree)));
            }
        }

        public void ShowRange()
        {
            _rangeRenderer.enabled = true;
        }

        public void HideRange()
        {
            _rangeRenderer.enabled = false;
        }
    }
}