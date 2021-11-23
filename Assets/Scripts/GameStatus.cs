using System;

[Serializable]
public class GameStatus
{
    public readonly int DifficultyLevel;
    public readonly int TotalBricksAmount;

    public GameStatus(int totalBricks, int difficulty)
    {
        TotalBricksAmount = totalBricks;
        BricksLeft = totalBricks;
        DifficultyLevel = difficulty;
    }

    public int BricksLeft { get; private set; }
    public bool AllBricksDestroyed => BricksLeft <= 0;

    public void OnBrickDestroyed()
    {
        BricksLeft--;
    }
}