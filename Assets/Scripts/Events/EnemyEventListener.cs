using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class EnemyEventListener : MonoBehaviour
    {
        public EnemyEvent Event;
        public UnityEvent<EnemyBehavior> Response;
        
        private void OnEnable()
        {
            Event.AddListener(this);
        }

        private void OnDisable()
        {
            Event.RemoveListener(this);
        }

        public void Raise(EnemyBehavior enemy)
        {
            Response.Invoke(enemy);
        }
    }
}