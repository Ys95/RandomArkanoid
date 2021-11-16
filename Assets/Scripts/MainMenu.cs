using UnityEngine;

public class MainMenu : UiState
{
    [Space]
    [SerializeField] UiState inGameState;
    
    public void OnExitBTNPress() => Application.Quit();

    public void OnStartGameBTNPress()
    {
        UiController.GoToNewState(inGameState);
        GameManager.Instance.StartGame();
    }

    protected override void OnStateEnter() => GameManager.Instance.StopGame();
}
