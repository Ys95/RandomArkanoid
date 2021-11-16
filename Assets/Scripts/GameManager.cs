using System;
using UnityEngine;

public class GameManager:MonoBehaviour
{
    GameManager _instance;
    public static GameManager Instance;
    
    [SerializeField] GameObject player;
    [SerializeField] GameObject level;

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

    public void StopGame()
    {
        player.SetActive(false);
        level.SetActive(false);
    }

    public void StartGame()
    {
        player.SetActive(true);
        level.SetActive(true);
    }
}
