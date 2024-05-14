using UnityEngine;

namespace Ui
{
    public class UiSelectedTower : MonoBehaviour
    {
        public GameObject EnabledSelection;

        public void SelectNewTower(GameObject newSelection)
        {
            if (EnabledSelection != null)
            {
                EnabledSelection.SetActive(false);
            }

            EnabledSelection = newSelection;

            if (EnabledSelection != null)
            {
                EnabledSelection.SetActive(true);
            }
        }
    }
}