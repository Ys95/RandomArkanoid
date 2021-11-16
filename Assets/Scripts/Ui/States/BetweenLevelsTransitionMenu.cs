using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BetweenLevelsTransitionMenu : UiState
{
    [SerializeField] UiState inGameState;
    
    [Space]
    [SerializeField] LevelController levelController;
    
    public override void HandleAnyKeyPress(InputAction.CallbackContext context)
    {
        base.HandleAnyKeyPress(context);
        UiController.GoToNewState(inGameState);
    }

    public override void HandlePauseKeyPress(InputAction.CallbackContext context)
    {
        base.HandlePauseKeyPress(context);
        UiController.GoToNewState(inGameState);
    }

    protected override void OnStateEnter()
    {
        GameManager.Instance.StartNewLevel();
    }
}
