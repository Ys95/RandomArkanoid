using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksGrid : MonoBehaviour
{
    [SerializeField] BricksManager _bricksPool;
    [SerializeField] BricksRandomGeneration _generator;

    [Space]
    [SerializeField] Vector2Int _grid;
    [SerializeField] Vector2 _spacing;

    [Space]
    [SerializeField] float _brickWidth;
    [SerializeField] float _brickHeight;

    [ContextMenu("Generate")]
    void Generate()
    {
        DestroyCurrent();
        for (int y = 0; y < _grid.y; y++)
        {
            for (int x = 0; x < _grid.x; x++)
            {
                Vector2 brickPos = new Vector2((_brickWidth + _spacing.x) * (float)x, (_brickHeight + _spacing.y) * -(float)y);
                GameObject brick = _bricksPool.GetBrick();
                brick.transform.localScale = new Vector2(_brickWidth, _brickHeight);
                brick.transform.parent = gameObject.transform;
                brick.transform.localPosition = brickPos;

                brick.SetActive(true);
            }
        }
    }

    [ContextMenu("GenerateRandomly")]
    void GenerateRandomly()
    {
        List<Vector2Int> bricksPositions = _generator.GenerateLevel(_grid);
        if (bricksPositions == null) return;

        DestroyCurrent();
        foreach(Vector2Int brickPosition in bricksPositions)
        {
            GameObject brick = _bricksPool.GetBrick();
            brick.transform.localScale = new Vector2(_brickWidth, _brickHeight);
            brick.transform.parent = gameObject.transform;

            Vector2 brickPos = new Vector2((float)brickPosition.x*(_brickWidth + _spacing.x), (float)brickPosition.y* (_brickHeight + _spacing.y));
            brick.transform.localPosition = brickPos;

            brick.SetActive(true);
        }
    }

    void DestroyCurrent()
    {
        Transform[] children = new Transform[transform.childCount];
        int i = 0;

        foreach (Transform child in transform)
        {
            children[i] = child;
            i++;
        }

        for (int k = 0; k < children.Length; k++)
        {
            children[k].gameObject.SetActive(false);
        }
    }
}
