using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelProperties
{
    #region Getters
    
    public int BricksPerCluster => bricksPerCluster;
    public int MINClustersPerDifficultyLevel => minClustersPerDifficultyLevel;
    public int MAXClustersPerDifficultyLevel => maxClustersPerDifficultyLevel;

    #endregion
    
    [Space]
    [SerializeField] int bricksPerCluster;
    [SerializeField] int minClustersPerDifficultyLevel;
    [SerializeField] int maxClustersPerDifficultyLevel;
}