using Events;
using UnityEngine;

namespace Variables
{
    public class Variable<T> : ScriptableObject
    {
        public string Name;
        
        public GameEvent OnValueChanged;
        
        [SerializeField] private T _value;
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                if (OnValueChanged != null)
                {
                    OnValueChanged.RaiseEvent();
                }
            }
        }
    }
}