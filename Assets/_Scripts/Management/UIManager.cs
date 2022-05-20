using UnityEngine;
using TMPro;

namespace StackRunner.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameConditionTextObject;
        [SerializeField] private GameObject retryButton;
        [SerializeField] private GameObject nextLevelButton;
        [SerializeField] private GameObject tapToPlayObject;

        private TextMeshProUGUI _gameConditionText;

        private void Awake()
        {
            _gameConditionText = gameConditionTextObject.GetComponent<TextMeshProUGUI>();
        }
        public void GameAwaitingStart()
        {
            tapToPlayObject.SetActive(true);
            gameConditionTextObject.SetActive(false);
            nextLevelButton.SetActive(false);
            if (retryButton != null)
                retryButton.SetActive(false);
        }
        public void GameStarted()
        {
            tapToPlayObject.SetActive(false);
        }

        public void GameWon()
        {
            _gameConditionText.SetText("Game Won");
            if (gameConditionTextObject != null)
                gameConditionTextObject.SetActive(true);
            nextLevelButton.SetActive(true);
        }
        public void GameLost()
        {
            _gameConditionText.SetText("Game Lost");
            if(gameConditionTextObject != null)
            gameConditionTextObject.SetActive(true);
            if (retryButton != null)
                retryButton.SetActive(true);
        }
    }
}
