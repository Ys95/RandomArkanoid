using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverMenu : UiState
{
    public override void HandleAnyKeyPress(InputAction.CallbackContext context)
    {
        UiController.GoToDefault();
    }

    protected override void OnStateEnter()
    {
        GameManager.PauseGame(true);
    }

    protected override void OnStateExit()
    {
        GameManager.PauseGame(false);
    }
}
