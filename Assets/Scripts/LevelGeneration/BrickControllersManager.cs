using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrickControllersManager : MonoBehaviour
{
    [SerializeField] UnityEvent<Vector2, int> onBrickDestroyed;
  
    [Space]
    [SerializeField] GameObject brickControllerPrefab;
    
    int _bricksLeft;
    
    public List<BrickController> AllBricks { get; private set; } = new();

    void OnEnable()
    {
        BrickController.OnBrickDestroyed += TriggerEvent;
    }

    void OnDisable()
    {
        BrickController.OnBrickDestroyed -= TriggerEvent;
    }

    void TriggerEvent(Vector2 pos, int score)
    {
        onBrickDestroyed?.Invoke(pos, score);
    }

    [ContextMenu("WipePool")]
    public void WipePool()
    {
        AllBricks = new List<BrickController>();
    }

    BrickController GetControllerFromPool()
    {
        foreach (var brickController in AllBricks)
            if (!brickController.IsBrickActive)
                return brickController;

        var newBrick = Instantiate(brickControllerPrefab, transform);
        var controller = newBrick.GetComponent<BrickController>();
        AllBricks.Add(controller);
        return controller;
    }

    public BrickController GetBrickController()
    {
        return GetControllerFromPool();
    }
}