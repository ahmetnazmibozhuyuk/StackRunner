using UnityEngine;
using StackRunner.Interactable;

namespace StackRunner.Managers
{
    [RequireComponent(typeof(UIManager),typeof(LevelManager))]
    public class GameManager : Singleton<GameManager>
    {
        public GameState CurrentState { get; private set; }


        public GameObject Player
        {
            get { return player; }
            set { player = value; }
        }
        [SerializeField] private GameObject player;

        public GameObject GoalPoint
        {
            get { return goalPoint; }
            set { goalPoint = value; }
        }
        [SerializeField] private GameObject goalPoint;

        public Gatherer Gatherer { get; private set; }

        private UIManager _uiManager;

        protected override void Awake()
        {
            base.Awake();

            _uiManager = GetComponent<UIManager>();
            Gatherer = Player.GetComponent<Gatherer>();
        }
        private void Start()
        {
            ChangeState(GameState.GameAwaitingStart);
        }
        public void ChangeState(GameState newState)
        {
            if (CurrentState == newState) return;

            CurrentState = newState;
            switch (newState)
            {
                case GameState.GameAwaitingStart:
                    GameAwaitingStartState();
                    break;
                case GameState.GameStarted:
                    GameStartedState();
                    break;
                case GameState.GameCheckingResults:
                    GameCheckingResultsState();
                    break;
                case GameState.GameWon:
                    GameWonState();
                    break;
                case GameState.GameLost:
                    GameLostState();
                    break;
                case GameState.StopMovement:
                    StopMovement();
                    break;
                default:
                    throw new System.ArgumentException("Invalid game state selection.");
            }
        }
        private void GameAwaitingStartState()
        {
            _uiManager.GameAwaitingStart();
        }
        private void GameStartedState()
        {
            _uiManager.GameStarted();
            GoalPoint = GameObject.FindGameObjectWithTag("Goal");
        }
        private void GameCheckingResultsState()
        {
            Gatherer.WinningScreen();
        }
        private void GameWonState()
        {
            _uiManager.GameWon();

        }
        private void GameLostState()
        {
            _uiManager.GameLost();
        }
        private void StopMovement()
        {

        }
    }
    public enum GameState
    {
        GamePreStart = 0,
        GameAwaitingStart = 1,
        GameStarted = 2,
        GameCheckingResults = 3,
        GameWon = 4,
        GameLost = 5,
        StopMovement = 6
    }
}
