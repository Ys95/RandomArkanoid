using UnityEngine;
using System;

public struct LevelState
{
    int AmountOfBricks { get; }
    int DifficultyLevel { get; }

    public LevelState(int amountOfBricks, int difficultyLevel)
    {
        AmountOfBricks = amountOfBricks;
        DifficultyLevel = difficultyLevel;
    }
}

public class LevelManager : MonoBehaviour
{
    public static Action<LevelState> BroadcastLevelState;
    public static Action<int> GenerateNewLevel;

    int _currentDifficultyLevel;

    void OnLevelGenerated(int amountOfBricks)
    {
        LevelState state = new LevelState(amountOfBricks, _currentDifficultyLevel);

        BroadcastLevelState.Invoke(state);
    }
}

