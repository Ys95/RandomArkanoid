using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Action OnGamePause;
    public static Action OnGameUnpause;
    
    [SerializeField] UnityEvent onNewGameStart;
    
    [Space]
    [SerializeField] UnityEvent onLevelCleared;
    [SerializeField] UnityEvent onLifeLost;
    [SerializeField] UnityEvent onNewLevelStarted;
    [SerializeField] UnityEvent onGameOver;
    [SerializeField] GameObject player;
    [SerializeField] GameObject level;
    
    [Space]
    [SerializeField] DifficultySystem difficultySystem;
    
    GameManager _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Instance = _instance;
        }
        else
        {
            Destroy(this);
        }
    }

    void DisableGameArea()
    {
        player.SetActive(false);
        level.SetActive(false);
    }

    void EnableGameArea()
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

    public void StartNewLevel()
    {
        onNewLevelStarted?.Invoke();
    }

    public void LevelCleared()
    {
        onLevelCleared?.Invoke();
    }

    public void LoseLife()
    {
        onLifeLost?.Invoke();
    }

    public void GameOver()
    {
        onGameOver?.Invoke();
    }

    public static void PauseGame(bool pause)
    {
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