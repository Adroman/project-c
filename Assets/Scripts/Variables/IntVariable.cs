using UnityEngine;

namespace Variables
{
    [CreateAssetMenu(menuName = "Project C/Variables/Int Variable")]
    public class IntVariable : Variable<int>
    {
        public void AddValue(int value)
        {
            Value += value;
        }

        public void SubtractValue(int value)
        {
            Value -= value;
        }

        public bool IsAtLeast(int value)
        {
            return Value >= value;
        }
    }
}