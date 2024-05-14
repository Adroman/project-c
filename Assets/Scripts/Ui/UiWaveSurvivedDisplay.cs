using TMPro;
using UnityEngine;
using Variables;

namespace Ui
{
    [RequireComponent(typeof(TMP_Text))]
    public class UiWaveSurvivedDisplay : MonoBehaviour
    {
        public IntVariable WaveNumber;
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            int wavesSurvived = WaveNumber.Value - 1;
            _text.text = $"You survived {wavesSurvived} {(wavesSurvived != 1 ? "waves" : "wave")}.";
        }
    }
}