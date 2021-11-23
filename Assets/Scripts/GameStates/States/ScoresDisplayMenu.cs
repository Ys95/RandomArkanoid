using UnityEngine;

public class ScoresDisplayMenu : GameState
{
    [SerializeField] LeaderboardDisplay display;

    public void OnExitBTNPress()
    {
        GameStateController.GoToPreviousState();
    }
}