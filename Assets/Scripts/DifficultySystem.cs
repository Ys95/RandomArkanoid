using UnityEngine;
using System;
using UnityEngine.Events;

public class DifficultySystem : MonoBehaviour
{
    public int CurrentDifficultyLevel => _currentDifficultyLevel;
    
    int _currentDifficultyLevel;

    public void ResetDifficulty()
    {
        _currentDifficultyLevel = 1;
    }
    
    public void IncreaseDifficultyLevel() => _currentDifficultyLevel++;
}

