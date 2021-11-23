using System;
using UnityEngine;

[Serializable]
public class LevelProperties
{
    [SerializeField] int bricksInCluster;
    [SerializeField] int minClustersPerDifficultyLvl;
    [SerializeField] int maxClustersPerDifficultyLvl;
    
    public int BricksInCluster => bricksInCluster;
    public int MINClustersPerDifficultyLvl => minClustersPerDifficultyLvl;
    public int MAXClustersPerDifficultyLvl => maxClustersPerDifficultyLvl;
}

public class LevelController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] LevelBuilder levelBuilder;
    [SerializeField] DifficultySystem difficultySystem;
    
    [Space]
    [SerializeField] LevelProperties levelProperties;
    
    int _activeBricks;
    int _currentLevel;

    public void ResetLevel()
    {
        _currentLevel = 1;
    }

    public void OnBrickDestroyed()
    {
        _activeBricks--;
        Debug.Log("Brick left: " + _activeBricks);
        if (_activeBricks <= 0) GameManager.Instance.LevelCleared();
    }

    public void RequestNewLevel()
    {
        _currentLevel++;
        _activeBricks = levelBuilder.BuildRandomLevel(levelProperties, difficultySystem.CurrentDifficultyLevel);
        Debug.Log("New level " + difficultySystem.CurrentDifficultyLevel + " : Bricks: " + _activeBricks);
    }
}