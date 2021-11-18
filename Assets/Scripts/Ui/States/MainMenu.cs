using UnityEngine;

public class MainMenu : UiState
{
    [Space]
    [SerializeField] UiState inGameState;
    
    public void OnExitBTNPress() => Application.Quit();

    public void OnStartGameBTNPress()
    {
        GameManager.Instance.StartNewGame();
        UiController.GoToNewState(inGameState);
    }

    protected override void OnStateEnter() => GameManager.Instance.StopGame();
}
