using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.EnemyBar
{
    public class UiEnemyBar : MonoBehaviour
    {
        public Slider Slider;

        [PublicAPI]
        public void UpdateSlider(EnemyBehavior enemy)
        {
            float value = enemy.Hp / enemy.MaxHp;
            Slider.value = value;
        }
    }
}