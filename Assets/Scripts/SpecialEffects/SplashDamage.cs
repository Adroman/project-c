using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpecialEffects
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class SplashDamage : BaseSpecialEffect
    {
        public float Radius;
        public float Damage;

        private readonly List<EnemyBehavior> _enemiesInRange = new();
        private CircleCollider2D _collider;

        private void OnEnable()
        {
            _collider = GetComponent<CircleCollider2D>();
            _collider.isTrigger = true;
        }

        public override BaseSpecialEffect AddToGameObject(GameObject go)
        {
            var component = go.AddComponent<SplashDamage>();
            component.Radius = Radius;
            component.Damage = Damage;
            component._collider.radius = Radius;

            return component;
        }

        public override void ApplySpecialEffect(EnemyBehavior enemy)
        {
            foreach (var enemyInRange in _enemiesInRange.Where(enemyInRange => enemy != enemyInRange).ToArray())
            {
                if (enemyInRange != null)
                {
                    enemyInRange.TakeDamage(Damage);
                }
            }
        }

        public override string GetDescription()
        {
            return $"Does {Damage} splash damage on {Radius} radius.";
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemy = other.GetComponent<EnemyBehavior>();
            if (enemy != null)
            {
                _enemiesInRange.Add(enemy);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var enemy = other.GetComponent<EnemyBehavior>();
            if (enemy != null)
            {
                _enemiesInRange.Remove(enemy);
            }
        }
    }
}