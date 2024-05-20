using Ui;
using UnityEngine;

namespace Towers
{
    public class TowerManager : MonoBehaviour
    {
        public TowerBehavior SelectedTower;

        public TowerBehavior BuildTower(TileBehavior tileBehavior)
        {
            if (SelectedTower == null)
            {
                Debug.Log("No tower selected.");
                return null;
            }
            if (!SelectedTower.CanBuildIt())
            {
                Debug.Log("Not enough resources to build the tower.");
                UiErrorMessage.Instance.ShowMessage("Not enough gold to build the tower.");
                return null;
            }

            SelectedTower.BuyTower();
            var tileTransform = tileBehavior.transform;
            var tilePosition = tileTransform.position;
            var tower = Instantiate(SelectedTower, 
                new Vector3(tilePosition.x, tilePosition.y),
                tileTransform.rotation,
                transform);
            
            return tower;
        }

        public TowerBehavior UpgradeTower(TileBehavior tileBehavior)
        {
            var towerToUpgrade = tileBehavior.BuiltTower;
            if (towerToUpgrade.UpgradedTower == null)
            {
                Debug.Log("Tower is at maximum level.");
                return towerToUpgrade;
            }

            var upgradedTowerPrefab = towerToUpgrade.UpgradedTower;

            if (!upgradedTowerPrefab.Unlocked())
            {
                Debug.Log("Tower is not unlocked for upgrade.");
                UiErrorMessage.Instance.ShowMessage("Tower upgrade is locked.");
                return towerToUpgrade;
            }
            
            if (!upgradedTowerPrefab.CanBuildIt())
            {
                Debug.Log("Not enough resources to upgrade the tower.");
                UiErrorMessage.Instance.ShowMessage("Not enough gold to upgrade the tower.");
                return towerToUpgrade;
            }

            upgradedTowerPrefab.BuyTower();
            var originalTowerTransform = towerToUpgrade.transform;
            var upgradedTower = Instantiate(upgradedTowerPrefab, originalTowerTransform.position,
                originalTowerTransform.rotation, transform);
            
            Destroy(towerToUpgrade.gameObject);
            return upgradedTower;
        }

        public void HideRanges()
        {
            foreach (var tower in GetComponentsInChildren<TowerBehavior>())
            {
                tower.HideRange();
            }
        }
    }
}