using UnityEngine;
using System;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
    [SerializeField] UnityEvent<LevelController> onLevelCleared;

    [SerializeField] BricksManager bricksManager;
    
    [Space]
    [SerializeField] LevelProperties levelProperties;
    
    GameStatus _gameStatus;
    int _currentDifficultyLevel;
    
    int CurrentDifficultyLevel => _currentDifficultyLevel;

    void OnEnable()
    {
        GameManager.OnGameStart += OnNewGameStarted;
    }
    
    void OnNewGameStarted()
    {
        _currentDifficultyLevel = 1;
        RequestLevelGeneration();
    }
        
    void OnNewLevelGenerated(int amountOfBricks)
    {
        _gameStatus = new GameStatus(amountOfBricks, _currentDifficultyLevel);
    }
    
    void IncreaseDifficultyLevel() => _currentDifficultyLevel++;
    public void OnBrickDestroyed()
    {
        _gameStatus.OnBrickDestroyed();;
        
        if (_gameStatus.AllBricksDestroyed)
        {
            onLevelCleared?.Invoke(this);
            IncreaseDifficultyLevel();
        }
    }
    
    public void RequestLevelGeneration()
    {
        int bricksAmount = bricksManager.GenerateNewLevel(levelProperties, _currentDifficultyLevel);
        OnNewLevelGenerated(bricksAmount);
    }

    void OnDisable()
    {
        GameManager.OnGameStart -= OnNewGameStarted;
    }
}

