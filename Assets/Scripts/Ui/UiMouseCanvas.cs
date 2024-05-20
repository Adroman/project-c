using Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui
{
    public class UiMouseCanvas : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameEvent OnMouseEntered;
        public GameEvent OnMouseExited;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnMouseEntered.RaiseEvent();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnMouseExited.RaiseEvent();
        }
    }
}