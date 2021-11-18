using System.Collections.Generic;
using UnityEngine;

public class BricksGrid : MonoBehaviour
{
    [SerializeField] BricksManager manager;
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
        ClearGrid();
        for (int y = 0; y < grid.y; y++)
        {
            for (int x = 0; x < grid.x; x++)
            {
                Vector2 brickPos = new Vector2((brickWidth + spacing.x) * (float)x, (brickHeight + spacing.y) * -(float)y);
                
                BrickController brickController = manager.GetBrickController();

                Transform brickTransform = brickController.transform;
                brickTransform.localScale = new Vector2(brickWidth, brickHeight);
                brickTransform.parent = gameObject.transform;
                brickTransform.localPosition = brickPos;
                
                brickController.RestoreBrick();
            }
        }
    }

    [ContextMenu("GenerateRandomly")]
    public int GenerateRandomly(LevelProperties properties, int difficultyLevel)
    {
        List<Vector2Int> bricksControllersPositions = generator.GenerateLevel(grid, properties, difficultyLevel);
        if (bricksControllersPositions == null) return 0;

        ClearGrid();
        foreach(Vector2Int brickPosition in bricksControllersPositions)
        {
            BrickController brickController = manager.GetBrickController();
            
            Transform brickControllerTransform = brickController.transform;
            brickControllerTransform.localScale = new Vector2(brickWidth, brickHeight);
            brickControllerTransform.parent = gameObject.transform;

            Vector2 brickPos = new Vector2((float)brickPosition.x*(brickWidth + spacing.x), (float)brickPosition.y* (brickHeight + spacing.y));
            brickControllerTransform.localPosition = brickPos;

            brickController.RestoreBrick();
        }

        return bricksControllersPositions.Count;
    }
    
    public void ClearGrid()
    {
        foreach (BrickController brickController in manager.AllBricks)
        {
            brickController.DisableBricks();
        }
    }
    
    [ContextMenu("Wipe")]
    public void DestroyBricks()
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
            DestroyImmediate(children[k].gameObject);
        }
    }
}
