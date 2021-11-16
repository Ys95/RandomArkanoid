using UnityEngine;
using UnityEngine.InputSystem;

public class InGame : UiState
{
    [Space]
    [SerializeField] UiState pauseState;
    [SerializeField] UiState levelClearedState;
    
    
    public override void HandlePauseKeyPress(InputAction.CallbackContext context) => UiController.GoToNewState(pauseState);

    protected override void OnStateEnter()
    {
        GameManager.PauseGame(false);
        Cursor.visible = false;
    }

    protected override void OnStateExit()
    {
        GameManager.PauseGame(true);
        Cursor.visible = true;
    }

    public void OnLevelCleared()
    {
        UiController.GoToNewState(levelClearedState);
    }
}
