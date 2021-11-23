using UnityEngine.InputSystem;

public class PauseMenuState : GameState
{
    public override void HandlePauseKeyPress(InputAction.CallbackContext context)
    {
        GameStateController.GoToPreviousState();
    }

    public void OnResumeBTNPress()
    {
        GameStateController.GoToPreviousState();
    }

    protected override void OnStateExit()
    {
        GameManager.PauseGame(false);
    }

    protected override void OnStateEnter()
    {
        GameManager.PauseGame(true);
    }
}