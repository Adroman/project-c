using UnityEngine;

namespace Events
{
    [RequireComponent(typeof(CircleRenderer))]
    public class HideCircleListener : GameEventListener
    {
        private CircleRenderer _circleRenderer;
        
        private void Awake()
        {
            _circleRenderer = GetComponent<CircleRenderer>();
            Response.AddListener(_circleRenderer.HideCircle);
        }
        
        
    }
}