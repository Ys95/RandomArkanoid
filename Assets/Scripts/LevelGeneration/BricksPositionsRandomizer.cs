using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class BricksPositionsRandomizer
{
    [SerializeField] List<Vector2Int> generatedBricks;
    readonly List<Vector2Int[]> _emptyBrickClusters = new();

    Vector2Int[] _emptyBrickSpots;

    int CalculateClustersAmount(LevelProperties properties, int difficultyLvl)
    {
        var brickClustersAmount = Random.Range(properties.MINClustersPerDifficultyLvl,
            properties.MAXClustersPerDifficultyLvl + 1);
        brickClustersAmount *= difficultyLvl;

        return brickClustersAmount;
    }

    bool ReachedGridCapacity(int totalAmountOfBricks, Vector2Int gridSize)
    {
        return totalAmountOfBricks > gridSize.x * gridSize.y;
    }

    public List<Vector2Int> GetBricksPositions(Vector2Int gridSize, LevelProperties properties, int difficultyLevel)
    {
        var brickClustersAmount = CalculateClustersAmount(properties, difficultyLevel);
        var totalAmountOfBricks = brickClustersAmount * properties.BricksInCluster;

        if (ReachedGridCapacity(totalAmountOfBricks, gridSize))
        {
            Debug.Log("Reached grid capacity");
            brickClustersAmount = gridSize.x * gridSize.y / properties.BricksInCluster;
            totalAmountOfBricks = brickClustersAmount * properties.BricksInCluster;
        }

        CreateEmptyClusters(properties.BricksInCluster, gridSize);

        generatedBricks = new List<Vector2Int>();

        for (var i = 0; i < brickClustersAmount; i++)
        {
            var cluster = PickClusterToFill(gridSize, properties.BricksInCluster);

            foreach (var brick in cluster) generatedBricks.Add(brick);

            _emptyBrickClusters.Remove(cluster);
        }

        return generatedBricks;
    }

    Vector2Int[] PickClusterToFill(Vector2Int gridSize, int bricksInCluster)
    {
        var pickRandomCluster = Random.Range(0, _emptyBrickClusters.Count);
        var pickedCluster = _emptyBrickClusters[pickRandomCluster];

        return pickedCluster;
    }

    void CreateEmptyClusters(int bricksInCluster, Vector2Int gridSize)
    {
        _emptyBrickClusters.Clear();
        var clustersAmount = gridSize.x * gridSize.y;
        var emptyBricksSpots = new Vector2Int[gridSize.x * gridSize.y];

        GenerateEmptyBrickSpots(gridSize, emptyBricksSpots);
        SplitIntoClusters(bricksInCluster, emptyBricksSpots, gridSize);
    }

    void GenerateEmptyBrickSpots(Vector2Int gridSize, Vector2Int[] emptyBricksSpots)
    {
        for (int i = 0, y = 0; y < gridSize.y; y++)
        for (var x = 0; x < gridSize.x; x++, i++)
            emptyBricksSpots[i] = new Vector2Int(x, -y);
        _emptyBrickSpots = emptyBricksSpots;
    }

    void SplitIntoClusters(int bricksInCluster, Vector2Int[] bricks, Vector2Int gridSize)
    {
        var xMod = gridSize.x;
        var xCount = 0;

        for (var i = 0; i < bricks.Length;)
        {
            var cluster = new Vector2Int[bricksInCluster];
            for (var j = 0; j < cluster.Length; j += 2, i++, xCount++)
            {
                if (i >= bricks.Length) return;

                cluster[j] = bricks[i];

                if (i + xMod < bricks.Length) cluster[j + 1] = bricks[i + xMod];
            }

            if (xCount == xMod)
            {
                i += xMod;
                xCount = 0;
            }

            _emptyBrickClusters.Add(cluster);
        }
    }
}