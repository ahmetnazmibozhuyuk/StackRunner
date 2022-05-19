using UnityEngine;
using TMPro;

namespace StackRunner.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameConditionTextObject;
        [SerializeField] private GameObject retryButton;
        [SerializeField] private GameObject nextLevelButton;

        private TextMeshProUGUI _gameConditionText;

        private void Awake()
        {
            _gameConditionText = gameConditionTextObject.GetComponent<TextMeshProUGUI>();
        }

        public void GameWon()
        {
            _gameConditionText.SetText("Game Won");
            gameConditionTextObject.SetActive(true);
            retryButton.SetActive(true);
        }
        public void GameLost()
        {
            _gameConditionText.SetText("Game Lost");
            gameConditionTextObject.SetActive(true);
            retryButton.SetActive(true);
        }
    }
}
