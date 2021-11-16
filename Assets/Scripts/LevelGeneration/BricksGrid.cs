using System.Collections.Generic;
using UnityEngine;

public class BricksGrid : MonoBehaviour
{
    [SerializeField] BricksManager bricksPool;
    [SerializeField] BricksRandomGeneration generator;

    [Space]
    [SerializeField] Vector2Int grid;
    [SerializeField] Vector2 spacing;

    [Space]
    [SerializeField] float brickWidth;
    [SerializeField] float brickHeight;

    [ContextMenu("Generate")]
    void Generate()
    {
        DestroyCurrent();
        for (int y = 0; y < grid.y; y++)
        {
            for (int x = 0; x < grid.x; x++)
            {
                Vector2 brickPos = new Vector2((brickWidth + spacing.x) * (float)x, (brickHeight + spacing.y) * -(float)y);
                GameObject brick = bricksPool.GetBrick();
                brick.transform.localScale = new Vector2(brickWidth, brickHeight);
                brick.transform.parent = gameObject.transform;
                brick.transform.localPosition = brickPos;

                brick.SetActive(true);
            }
        }
    }

    [ContextMenu("GenerateRandomly")]
    void GenerateRandomly()
    {
        List<Vector2Int> bricksPositions = generator.GenerateLevel(grid);
        if (bricksPositions == null) return;

        DestroyCurrent();
        foreach(Vector2Int brickPosition in bricksPositions)
        {
            GameObject brick = bricksPool.GetBrick();
            brick.transform.localScale = new Vector2(brickWidth, brickHeight);
            brick.transform.parent = gameObject.transform;

            Vector2 brickPos = new Vector2((float)brickPosition.x*(brickWidth + spacing.x), (float)brickPosition.y* (brickHeight + spacing.y));
            brick.transform.localPosition = brickPos;

            brick.SetActive(true);
        }
    }

    [ContextMenu("Wipe")]
    public void DestroyCurrent()
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
