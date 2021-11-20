using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoresDisplayMenu : UiState
{
    [SerializeField] LeaderboardDisplay display;

    public void OnExitBTNPress() => UiController.GoToPreviousState();
}
