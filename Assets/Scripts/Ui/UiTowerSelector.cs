using JetBrains.Annotations;
using Towers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui
{
    public class UiTowerSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public TowerBehavior TowerToSelect;
        public TowerManager TowerManager;

        public Canvas TowerInfo;

        [PublicAPI]
        public void SelectTower()
        {
            TowerManager.SelectedTower = TowerToSelect;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            TowerInfo.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TowerInfo.gameObject.SetActive(false);
        }
    }
}