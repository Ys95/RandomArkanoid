using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class BricksManager : MonoBehaviour
{
    [SerializeField] UnityEvent<Vector2> onBrickDestroyed;

    [Space]
    [SerializeField] GameObject brickPrefab;

    List<GameObject> _bricksPool = new List<GameObject>();

    void OnEnable()
    {
        BrickScript.OnBrickDestroyed += TriggerEvent;
    }

    void TriggerEvent(Vector2 pos)
    {
        onBrickDestroyed?.Invoke(pos);
    }

    [ContextMenu("WipePool")]
    public void WipePool() => _bricksPool = new List<GameObject>();

    public GameObject GetBrick()
    {
        foreach (GameObject brick in _bricksPool)
        {
            if (!brick.activeInHierarchy) return brick;
        }
        GameObject newBrick = Instantiate(brickPrefab, transform);
        newBrick.SetActive(false);
        _bricksPool.Add(newBrick);
        return newBrick;
    }

    void OnDisable()
    {
        BrickScript.OnBrickDestroyed -= TriggerEvent;

    }
}
