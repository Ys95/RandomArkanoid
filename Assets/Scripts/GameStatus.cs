using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameStatus
{
    public readonly int TotalBricksAmount;
    public readonly int DifficultyLevel;

    int _bricksLeft;
    public int BricksLeft => _bricksLeft;

    public void OnBrickDestroyed() 
    {
        _bricksLeft--;
    }

    public bool AllBricksDestroyed => _bricksLeft <= 0;

    public GameStatus(int totalBricks, int difficulty)
    {
        TotalBricksAmount = totalBricks;
        _bricksLeft = totalBricks;
        DifficultyLevel = difficulty;
    }
}
