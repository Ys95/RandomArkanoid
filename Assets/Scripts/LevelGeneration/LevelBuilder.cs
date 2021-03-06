using System.Collections.Generic;
using Bricks;
using UnityEngine;

namespace LevelGeneration
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] BrickControllersManager brickControllersManager;
        [SerializeField] BricksPositionsRandomizer bricksPositionsRandomizer;
        [SerializeField] BrickTypeRandomizer brickTypeRandomizer;

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
            for (int x = 0; x < grid.x; x++)
            {
                Vector2 brickPos = new Vector2((brickWidth + spacing.x) * x, (brickHeight + spacing.y) * -(float) y);

                BrickController brickController = brickControllersManager.GetBrickController();

                Transform brickTransform = brickController.transform;
                brickTransform.localScale = new Vector2(brickWidth, brickHeight);
                brickTransform.parent = gameObject.transform;
                brickTransform.localPosition = brickPos;

                brickController.RestoreBrick();
            }
        }

        [ContextMenu("GenerateRandomly")]
        public int BuildRandomLevel(LevelProperties properties, int difficultyLevel)
        {
            List<Vector2Int> bricksControllersPositions =
                bricksPositionsRandomizer.GetBricksPositions(grid, properties, difficultyLevel);
            if (bricksControllersPositions == null) return 0;

            int brickCount = 0;

            ClearGrid();
            foreach (Vector2Int brickPosition in bricksControllersPositions)
            {
                BrickController brickController = brickControllersManager.GetBrickController();
                brickController.ChangeBrickType(brickTypeRandomizer.RollBrick(difficultyLevel));

                Transform brickControllerTransform = brickController.transform;
                brickControllerTransform.localScale = new Vector2(brickWidth, brickHeight);
                brickControllerTransform.parent = gameObject.transform;

                Vector2 brickPos = new Vector2(brickPosition.x * (brickWidth + spacing.x),
                    brickPosition.y * (brickHeight + spacing.y));
                brickControllerTransform.localPosition = brickPos;

                brickController.RestoreBrick();
                if (!brickController.Brick.IsObstacle) brickCount++;
            }

            return brickCount;
        }

        public void ClearGrid()
        {
            foreach (BrickController brickController in brickControllersManager.AllBricks)
            {
                brickController.DisableBricks();
            }

            brickControllersManager.AllBricks.Clear();
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
}