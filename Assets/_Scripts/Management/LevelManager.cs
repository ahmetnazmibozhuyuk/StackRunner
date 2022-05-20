using UnityEngine;
using UnityEngine.SceneManagement;

namespace StackRunner.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] Levels;

        private string _currentScene;
        private int _currentLevelIndex;

        private GameObject _currentLevel;
        private void Awake()
        {
            _currentLevelIndex = PlayerPrefs.GetInt("_currentLevelIndex");
        }
        private void Start()
        {
            _currentScene = SceneManager.GetActiveScene().name;
            OpenLevel();
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(_currentScene);
        }
        public void NextLevel()
        {
            PlayerPrefs.SetInt("_currentLevelIndex", _currentLevelIndex + 1);
            _currentLevelIndex = PlayerPrefs.GetInt("_currentLevelIndex");
            Destroy(_currentLevel);
            _currentLevel = null;
            OpenLevel();
        }
        private void OpenLevel()
        {
            if(_currentLevelIndex > Levels.Length-1)
            {
                PlayerPrefs.SetInt("_currentLevelIndex", 0);
                _currentLevelIndex = PlayerPrefs.GetInt("_currentLevelIndex");
            }
            _currentLevel = Instantiate(Levels[_currentLevelIndex], Vector3.zero, Quaternion.identity);
            GameManager.instance.Player.transform.position = Vector3.up;
            GameManager.instance.Player.GetComponent<Rigidbody>().isKinematic = false;
            GameManager.instance.Gatherer.ResetColor();
            GameManager.instance.Gatherer.ResetCounter();
            GameManager.instance.ChangeState(GameState.GameAwaitingStart);
        }
    }
}
