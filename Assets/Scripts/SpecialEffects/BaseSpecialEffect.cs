using UnityEngine;

namespace SpecialEffects
{
    public abstract class BaseSpecialEffect : MonoBehaviour
    {
        public abstract BaseSpecialEffect AddToGameObject(GameObject go);

        public abstract void ApplySpecialEffect(EnemyBehavior enemy);

        public abstract string GetDescription();
    }
}