using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameStates.States
{
    public class InGameState : GameState
    {
        [SerializeField] PlayerControlsDisabler playerControlsDisabler;

        [Space]
        [SerializeField] GameState pauseState;
        [SerializeField] GameState levelClearedState;

        public override void HandlePauseKeyPress(InputAction.CallbackContext context)
        {
            GameStateController.GoToNewState(pauseState);
        }

        public void OnLevelCleared()
        {
            GameStateController.GoToNewState(levelClearedState);
        }

        protected override void OnStateEnter()
        {
            playerControlsDisabler.EnableControls();
            GameManager.PauseGame(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        protected override void OnStateExit()
        {
            playerControlsDisabler.DisableControls();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}