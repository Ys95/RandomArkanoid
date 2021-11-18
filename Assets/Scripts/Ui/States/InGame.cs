using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGame : UiState
{
    [SerializeField] PlayerControlsDisabler playerControlsDisabler;
    
    [Space]
    [SerializeField] UiState pauseState;
    [SerializeField] UiState levelClearedState;
    [SerializeField] UiState gameOverState;
    
    
    public override void HandlePauseKeyPress(InputAction.CallbackContext context) => UiController.GoToNewState(pauseState);

    public void OnGameOver()
    {
        UiController.GoToNewState(gameOverState);
    }

    public void OnLevelCleared()
    {
        UiController.GoToNewState(levelClearedState);
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
