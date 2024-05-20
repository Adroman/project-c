using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Ui
{
    public class UiErrorMessage : MonoBehaviour
    {
        public static UiErrorMessage Instance { get; private set; }

        public TMP_Text TextToUpdate;

        private Coroutine _activeCoroutine;
        
        private void Awake()
        {
            Instance = this;
            gameObject.SetActive(false);
        }

        public void ShowMessage(string message)
        {
            if (_activeCoroutine != null)
            {
                StopCoroutine(_activeCoroutine);
            }
            gameObject.SetActive(true);
            _activeCoroutine = StartCoroutine(Message(message));
        }

        private IEnumerator Message(string message)
        {
            TextToUpdate.text = message;
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
            _activeCoroutine = null;
        }
    }
}