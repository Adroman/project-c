using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecialEffects;
using UnityEngine;
using Variables;
using Random = UnityEngine.Random;


namespace Towers
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(CircleRenderer))]
    public class TowerBehavior : MonoBehaviour
    {
        public Transform RotatingPart;
        public float RotationAngleOffset;
        public Transform ShootingPoint;
        public BulletBehavior BulletToSpawn;

        public string Name;
        public float MinDamage;
        public float MaxDamage;
        public float FireRate;
        public float PreviewRange;

        public float Range
        {
            get => _collider.radius;
            set
            {
                _collider.radius = value;
                _circleRenderer.CalculateCircle(value);
            }
        }

        public int Price;
        public IntVariable PriceCurrency;

        public BoolVariable IsUnlocked;
        public TowerBehavior UpgradedTower;

        private CircleCollider2D _collider;
        private LineRenderer _rangeRenderer;
        private CircleRenderer _circleRenderer;
        private readonly List<EnemyBehavior> _enemiesToTarget = new();
        private BaseSpecialEffect[] _specialEffects;

        private float _lastFired;
        private float _reloadTime;
        private bool _hasRotatingPart;
        private bool _hasShootingPoint;
    
        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
            _rangeRenderer = GetComponent<LineRenderer>();
            _circleRenderer = GetComponent<CircleRenderer>();
        }

        private void Start()
        {
            _lastFired = float.MinValue;
            _reloadTime = 1f / FireRate;
            _hasRotatingPart = RotatingPart != null;
            _hasShootingPoint = ShootingPoint != null;
            _specialEffects = GetComponents<BaseSpecialEffect>();
            Range = PreviewRange;
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

            foreach (var specialEffect in _specialEffects)
            {
                specialEffect.AddToGameObject(bullet.gameObject);
            }

            _lastFired = Time.time;
        }

        public bool Unlocked()
        {
            return IsUnlocked == null || IsUnlocked.IsEnabled;
        }

        public bool CanBuildIt()
        {
            return PriceCurrency.IsAtLeast(Price);
        }

        public void BuyTower()
        {
            PriceCurrency.SubtractValue(Price);
        }

        public void ShowRange()
        {
            _rangeRenderer.enabled = true;
        }

        public void HideRange()
        {
            _rangeRenderer.enabled = false;
        }

        public string PrepareInfoText()
        {
            var sb = new StringBuilder();
            sb.Append(Name).AppendLine().AppendLine();
            sb.Append("Damage: ").Append(MinDamage).Append('-').Append(MaxDamage).AppendLine();
            sb.Append("Fire rate: ").Append(FireRate).AppendLine();
            sb.Append("Range: ").Append(Range).AppendLine();
            foreach (var specialEffect in _specialEffects)
            {
                sb.Append(specialEffect.GetDescription()).AppendLine();
            }

            sb.AppendLine();

            if (UpgradedTower != null)
            {
                if (!UpgradedTower.IsUnlocked.IsEnabled)
                {
                    sb.Append("Upgrade is locked.");
                }
                else
                {
                    sb.Append("Upgrade price: ").Append(UpgradedTower.Price).Append(" gold").AppendLine();
                    sb.Append("Click on the tower to upgrade it.");
                }
            }
            else
            {
                sb.Append("Tower is at maximum level.");
            }

            return sb.ToString();
        }
    }
}