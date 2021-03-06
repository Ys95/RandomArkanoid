using System;
using System.Collections.Generic;
using Bricks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelGeneration
{
    [Serializable]
    public class BrickTypeRandomizer
    {
        [Space]
        [SerializeField] BrickNames defaultBrick;
        [SerializeField] BrickSpawn[] possibleBricksSpawns;

        List<BrickNames> _spawnCandidates = new();
        bool ShouldNonDefaultBrickSpawn => Random.Range(0f, 1f) <= 0.4f;

        void GetSpawnCandidates(int difficulty)
        {
            _spawnCandidates.Clear();
            foreach (BrickSpawn brickSpawn in possibleBricksSpawns)
            {
                int roll = Random.Range(0, 101);
                if (roll <= brickSpawn.GetSpawnChance(difficulty)) _spawnCandidates.Add(brickSpawn.Name);
            }
        }

        public BrickNames RollBrick(int difficulty)
        {
            if (!ShouldNonDefaultBrickSpawn) return defaultBrick;

            GetSpawnCandidates(difficulty);

            if (_spawnCandidates.Count == 0) return defaultBrick;
            int roll = Random.Range(0, _spawnCandidates.Count);

            return _spawnCandidates[roll];
        }

        [Serializable]
        struct BrickSpawn
        {
            [SerializeField] BrickNames name;
            [SerializeField] int baseSpawnRate;
            [SerializeField] int spawnRatePerDifficulty;
            [Range(0, 100)] [SerializeField] int maxSpawnRate;
            public BrickNames Name => name;

            public int GetSpawnChance(int difficulty)
            {
                return Mathf.Clamp(baseSpawnRate + spawnRatePerDifficulty * difficulty, 0, maxSpawnRate);
            }
        }
    }
}