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
                Debug.Log("Not enough resources to build the tower or the tower is not unlocked.");
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

            if (!upgradedTowerPrefab.CanBuildIt())
            {
                Debug.Log("Not enough resources to upgrade the tower or the upgrade is not unlocked.");
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