using TMPro;
using Towers;
using UnityEngine;

namespace Ui
{
    public class UiTowerInfo : MonoBehaviour
    {
        public TMP_Text TextToUpdate;
        
        public void Show(TowerBehavior tower)
        {
            TextToUpdate.text = tower.PrepareInfoText();
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}