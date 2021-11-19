using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelProperties
{
    public int BricksInCluster => bricksInCluster;
    public int MINClustersPerDifficultyLvl => minClustersPerDifficultyLvl;
    public int MAXClustersPerDifficultyLvl => maxClustersPerDifficultyLvl;
    
    [SerializeField] int bricksInCluster;
    [SerializeField] int minClustersPerDifficultyLvl;
    [SerializeField] int maxClustersPerDifficultyLvl;
}

public class LevelController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] LevelBuilder levelBuilder;
    [SerializeField] DifficultySystem difficultySystem;

    [Space]
    [SerializeField] LevelProperties levelProperties;
    
    int _currentLevel;
    int _activeBricks;
    
    public void ResetLevel() => _currentLevel = 1;

    public void OnBrickDestroyed()
    {
        _activeBricks--;
        Debug.Log("Brick left: " + _activeBricks);
        if(_activeBricks<=0) GameManager.Instance.LevelCleared();
    }

    public void RequestNewLevel()
    {
        _currentLevel++;
        _activeBricks = levelBuilder.BuildRandomLevel(levelProperties, difficultySystem.CurrentDifficultyLevel);
        Debug.Log("New level " + difficultySystem.CurrentDifficultyLevel + " : Bricks: " + _activeBricks);
    }
}
