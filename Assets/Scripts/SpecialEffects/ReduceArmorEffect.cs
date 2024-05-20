using UnityEngine;

namespace SpecialEffects
{
    public class ReduceArmorEffect : BaseSpecialEffect
    {
        public float Amount;
        
        public override BaseSpecialEffect AddToGameObject(GameObject go)
        {
            var component = go.AddComponent<ReduceArmorEffect>();
            component.Amount = Amount;
            
            return component;
        }

        public override void ApplySpecialEffect(EnemyBehavior enemy)
        {
            enemy.Armor = Mathf.Clamp(enemy.Armor - Amount, 0, float.PositiveInfinity);
        }

        public override string GetDescription()
        {
            return $"Reduces armor by {Amount} per hit.";
        }
    }
}