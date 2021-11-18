using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager:MonoBehaviour
{
    [SerializeField] UnityEvent onNewGameStart;
    
    [Space]
    [SerializeField] UnityEvent onLevelCleared;
    [SerializeField] UnityEvent onLifeLost;
    [SerializeField] UnityEvent onNewLevelStarted;
    [SerializeField] UnityEvent onGameOver;
    
    
    GameManager _instance;
    public static GameManager Instance;
    
    [SerializeField] GameObject player;
    [SerializeField] GameObject level;

    [Space]
    [SerializeField] DifficultySystem difficultySystem;

    public static Action OnGamePause;
    public static Action OnGameUnpause;

    static bool _isGamePaused;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Instance = _instance;
        }
        else Destroy(this);
    }

    public void DisableGameArea()
    {
        player.SetActive(false);
        level.SetActive(false);
    }

    public void EnableGameArea()
    {
        player.SetActive(true);
        level.SetActive(true);
    }
    
    public void StartNewGame()
    {
        EnableGameArea();
        onNewGameStart?.Invoke();
    }
    
    public void StopGame()
    {
        DisableGameArea();
    }

    public void StartNewLevel() => onNewLevelStarted?.Invoke();
    
    public void LevelCleared() => onLevelCleared?.Invoke();

    public void LoseLife() => onLifeLost?.Invoke();
    
    public void GameOver() =>onGameOver?.Invoke();
    
    public static void PauseGame(bool pause)
    {
        _isGamePaused = pause;
        if (pause)
        {
            Time.timeScale = 0f;
            OnGamePause?.Invoke();
            return;
        }
        OnGameUnpause?.Invoke();
        Time.timeScale = 1f;
    }
}
