using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(menuName = "Project C/Events/Enemy Event")]
    public class EnemyEvent : ScriptableObject
    {
        private readonly List<EnemyEventListener> _listeners = new();

        public void AddListener(EnemyEventListener listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(EnemyEventListener listener)
        {
            _listeners.Remove(listener);
        }
        
        public void RaiseEvent(EnemyBehavior enemy)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].Raise(enemy);
            }
        }
    }
}