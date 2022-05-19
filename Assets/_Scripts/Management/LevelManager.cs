using UnityEngine;
using UnityEngine.SceneManagement;

namespace StackRunner.Managers
{
    public class LevelManager : MonoBehaviour
    {
        private string _currentScene;

        private void Start()
        {
            _currentScene = SceneManager.GetActiveScene().name;
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(_currentScene);
        }
        public void NextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
