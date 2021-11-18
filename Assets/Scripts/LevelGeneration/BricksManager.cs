using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class BricksManager : MonoBehaviour
{
    [SerializeField] UnityEvent<Vector2, int> onBrickDestroyed;

    [Space]
    [SerializeField] GameObject brickControllerPrefab;
    [SerializeField] BricksGrid grid;

    List<BrickController> _allBricks = new List<BrickController>();

    int _bricksLeft;

    public List<BrickController> AllBricks => _allBricks;

    void OnEnable()
    {
        BrickController.OnBrickDestroyed += TriggerEvent;
    }
    
    public int GenerateNewLevel(LevelProperties leveProperties, int difficultyLevel)
    {
        int bricksAmount = grid.GenerateRandomly(leveProperties, difficultyLevel);
        return bricksAmount;
    }
    
    void TriggerEvent(Vector2 pos, int score)
    {
        onBrickDestroyed?.Invoke(pos, score);
    }

    [ContextMenu("WipePool")]
    public void WipePool() => _allBricks = new List<BrickController>();

    BrickNames RollBrick()
    {
        return BrickNames.DurableBrick;
    }
    
    public BrickController GetBrickController()
    {
        BrickController controller = GetControllerFromPool();
        
        controller.ChangeBrickType(RollBrick());

        return controller;
    }

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

    void OnDisable()
    {
        BrickController.OnBrickDestroyed -= TriggerEvent;
    }
}
