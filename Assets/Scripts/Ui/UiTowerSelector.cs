using JetBrains.Annotations;
using Towers;
using UnityEngine;

namespace Ui
{
    public class UiTowerSelector : MonoBehaviour
    {
        public TowerBehavior TowerToSelect;
        public TowerManager TowerManager;

        [PublicAPI]
        public void SelectTower()
        {
            TowerManager.SelectedTower = TowerToSelect;
        }
    }
}