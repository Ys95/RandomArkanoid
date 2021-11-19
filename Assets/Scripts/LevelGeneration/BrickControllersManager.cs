using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BrickControllersManager : MonoBehaviour
{
    [SerializeField] UnityEvent<Vector2, int> onBrickDestroyed;

    [Space]
    [SerializeField] GameObject brickControllerPrefab;

    List<BrickController> _allBricks = new();

    int _bricksLeft;
    
    public List<BrickController> AllBricks => _allBricks;

    void OnEnable()
    {
        BrickController.OnBrickDestroyed += TriggerEvent;
    }
    
    void TriggerEvent(Vector2 pos, int score)
    {
        onBrickDestroyed?.Invoke(pos, score);
    }

    [ContextMenu("WipePool")]
    public void WipePool() => _allBricks = new List<BrickController>();
    
    BrickController GetControllerFromPool()
    {
        foreach (BrickController brickController in _allBricks)
        {
            if (!brickController.IsBrickActive) return brickController;
        }

        GameObject newBrick = Instantiate(brickControllerPrefab, transform);
        BrickController controller = newBrick.GetComponent<BrickController>();
        _allBricks.Add(controller);
        return controller;
    }

    public BrickController GetBrickController() => GetControllerFromPool();

    void OnDisable()
    {
        BrickController.OnBrickDestroyed -= TriggerEvent;
    }
}
