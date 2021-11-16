using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelClearedOverlay : UiState
{
    [SerializeField] UiState transitionScreen;
    
    public override void HandlePauseKeyPress(InputAction.CallbackContext context)
    {
        base.HandlePauseKeyPress(context);
        UiController.GoToNewState(transitionScreen);
    }

    public override void HandleAnyKeyPress(InputAction.CallbackContext context)
    {
        base.HandleAnyKeyPress(context);
        UiController.GoToNewState(transitionScreen);
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        //GameManager.PauseGame(true);
    }
}
