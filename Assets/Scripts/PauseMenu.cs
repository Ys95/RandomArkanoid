using UnityEngine.InputSystem;

public class PauseMenu : UiState
{
    public override void HandlePauseKeyPress(InputAction.CallbackContext context) => UiController.GoToPreviousState();

    public void OnResumeBTNPress() => UiController.GoToPreviousState();
}
