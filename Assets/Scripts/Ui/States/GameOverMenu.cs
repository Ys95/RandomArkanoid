using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverMenu : UiState
{
    [SerializeField] UiState sendScore;
    
    public override void HandleAnyKeyPress(InputAction.CallbackContext context)
    {
        UiController.GoToNewState(sendScore);
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
