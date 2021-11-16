using System;
using UnityEngine;

public class GameManager:MonoBehaviour
{
    GameManager _instance;
    public static GameManager Instance;
    
    [SerializeField] GameObject player;
    [SerializeField] GameObject level;

    [Space]
    [SerializeField] LevelController levelController;

    public static Action OnGamePause;
    public static Action OnGameUnpause;
    
    public static Action OnGameStart;
    public static Action OnGameOver;
    
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
        OnGameStart?.Invoke();
    }
    
    public void StopGame()
    {
        DisableGameArea();
    }

    public void StartNewLevel()
    {
        levelController.RequestLevelGeneration();
    }
    
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
    
    public static void GameOver()
    {
        OnGameOver?.Invoke();   
    }
}
