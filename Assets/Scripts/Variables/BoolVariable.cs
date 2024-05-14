using UnityEngine;

namespace Variables
{
    [CreateAssetMenu(menuName = "Project C/Variables/Boolean Variable")]
    public class BoolVariable : Variable<bool>
    {
        public void Enable()
        {
            Value = true;
        }

        public void Disable()
        {
            Value = false;
        }
        
        public bool IsEnabled => Value;
    }
}