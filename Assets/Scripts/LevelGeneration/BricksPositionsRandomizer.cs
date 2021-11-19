using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class BricksPositionsRandomizer
{
    [SerializeField] List<Vector2Int> generatedBricks;
    [SerializeField] readonly List<Vector2Int[]> _emptyBrickClusters = new();
    [SerializeField] Vector2Int[] emptyBrickSpotsD;

    int CalculateClustersAmount(LevelProperties properties, int difficultyLvl)
    {
        int brickClustersAmount = Random.Range(properties.MINClustersPerDifficultyLvl, properties.MAXClustersPerDifficultyLvl + 1);
        brickClustersAmount *= difficultyLvl;

        return brickClustersAmount;
    }
    
    bool ReachedGridCapacity(int totalAmountOfBricks, Vector2Int gridSize) => totalAmountOfBricks > (gridSize.x * gridSize.y);

    public List<Vector2Int> GetBricksPositions(Vector2Int gridSize, LevelProperties properties, int difficultyLevel)
    {
        int brickClustersAmount = CalculateClustersAmount(properties, difficultyLevel);
        int totalAmountOfBricks = brickClustersAmount * properties.BricksInCluster;

        if (ReachedGridCapacity(totalAmountOfBricks, gridSize))
        {
            Debug.LogError("Reached grid capacity");
            brickClustersAmount = (gridSize.x * gridSize.y) / properties.BricksInCluster;
            totalAmountOfBricks = brickClustersAmount * properties.BricksInCluster;
        }

        CreateEmptyClusters(properties.BricksInCluster, gridSize);
       
        generatedBricks = new List<Vector2Int>();

        for (int i = 0; i < brickClustersAmount; i++)
        {
            Vector2Int[] cluster = PickClusterToFill(gridSize, properties.BricksInCluster);

            foreach (Vector2Int brick in cluster)
            {
                generatedBricks.Add(brick);
            }

            _emptyBrickClusters.Remove(cluster);
        }

        return generatedBricks;
    }
    
    Vector2Int[] PickClusterToFill(Vector2Int gridSize, int bricksInCluster)
    {
        int pickRandomCluster = Random.Range(0, _emptyBrickClusters.Count);
        Vector2Int[] pickedCluster = _emptyBrickClusters[pickRandomCluster];

        return pickedCluster;
    }

    void CreateEmptyClusters(int bricksInCluster, Vector2Int gridSize)
    {
        _emptyBrickClusters.Clear();
        int clustersAmount = gridSize.x * gridSize.y;
        Vector2Int[] emptyBricksSpots = new Vector2Int[gridSize.x * gridSize.y];

        GenerateEmptyBrickSpots(gridSize, emptyBricksSpots);
        SplitIntoClusters(bricksInCluster, emptyBricksSpots, gridSize);
    }

    void GenerateEmptyBrickSpots(Vector2Int gridSize, Vector2Int[] emptyBricksSpots)
    {
        for (int i = 0, y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++, i++)
            {
                emptyBricksSpots[i] = new Vector2Int(x, -y);
            }
        }
        emptyBrickSpotsD = emptyBricksSpots;
    }

    void SplitIntoClusters(int bricksInCluster, Vector2Int[] bricks, Vector2Int gridSize)
    {
        int xMod = gridSize.x;
        int xCount = 0;

        for (int i = 0; i < bricks.Length;)
        {
            Vector2Int[] cluster = new Vector2Int[bricksInCluster];
            for (int j = 0; (j < cluster.Length); j += 2, i++, xCount++)
            {
                if (i >= (bricks.Length)) return;

                cluster[j] = bricks[i];

                if ((i + xMod) < bricks.Length)
                {
                    cluster[j + 1] = bricks[i + xMod];
                }
            }

            if (xCount == xMod)
            {
                i += (xMod);
                xCount = 0;
            }

            _emptyBrickClusters.Add(cluster);
        }
    }
}