using UnityEngine;
using System;
using UnityEngine.Events;

public class DifficultySystem : MonoBehaviour
{
    [SerializeField] int startingDifficulty;
    
    public int CurrentDifficultyLevel => _currentDifficultyLevel;
    
    int _currentDifficultyLevel;

    public void ResetDifficulty()
    {
        _currentDifficultyLevel = startingDifficulty;
    }
    
    public void IncreaseDifficultyLevel() => _currentDifficultyLevel++;
}

