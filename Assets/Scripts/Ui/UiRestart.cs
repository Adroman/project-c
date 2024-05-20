using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class UiRestart : MonoBehaviour
    {
        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Level1");
        }
    }
}