using UnityEngine;

namespace Ui
{
    public class UiTutorialEnablerOnce : MonoBehaviour
    {
        public Canvas CanvasToEnable;

        private bool _alreadyEnabled = false;

        public void Enable()
        {
            if (!_alreadyEnabled)
            {
                _alreadyEnabled = true;
                CanvasToEnable.gameObject.SetActive(true);
            }
        }
    }
}