using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using Variables;

namespace Ui
{
    [RequireComponent(typeof(TMP_Text))]
    public class UiTextUpdater : MonoBehaviour
    {
        public IntVariable VariableToRead;
        public string Prefix;
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            UpdateText();
        }

        [PublicAPI]
        public void UpdateText()
        {
            _text.text = Prefix + VariableToRead.Value;
        }
    }
}