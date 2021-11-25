using UnityEngine;
using UnityEngine.InputSystem;

namespace GameStates.States
{
    public class LevelClearedState : GameState
    {
        [SerializeField] GameState transitionScreen;

        public override void HandlePauseKeyPress(InputAction.CallbackContext context)
        {
            base.HandlePauseKeyPress(context);
            GameStateController.GoToNewState(transitionScreen);
        }

        public override void HandleAnyKeyPress(InputAction.CallbackContext context)
        {
            base.HandleAnyKeyPress(context);
            GameStateController.GoToNewState(transitionScreen);
        }
    }
}