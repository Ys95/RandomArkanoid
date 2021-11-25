using UnityEngine;

namespace GameStates.States
{
    public class MainMenuState : GameState
    {
        [Space]
        [SerializeField] GameState inGameState;

        public void OnExitBTNPress()
        {
            Application.Quit();
        }

        public void OnStartGameBTNPress()
        {
            GameManager.Instance.StartNewGame();
            GameStateController.GoToNewState(inGameState);
        }

        protected override void OnStateEnter()
        {
            GameManager.Instance.StopGame();
        }
    }
}