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
            for (var y = 0; y < grid.y; y++)
            for (var x = 0; x < grid.x; x++)
            {
                var brickPos = new Vector2((brickWidth + spacing.x) * x, (brickHeight + spacing.y) * -(float) y);

                var brickController = brickControllersManager.GetBrickController();

                var brickTransform = brickController.transform;
                brickTransform.localScale = new Vector2(brickWidth, brickHeight);
                brickTransform.parent = gameObject.transform;
                brickTransform.localPosition = brickPos;

                brickController.RestoreBrick();
            }
        }

        [ContextMenu("GenerateRandomly")]
        public int BuildRandomLevel(LevelProperties properties, int difficultyLevel)
        {
            var bricksControllersPositions =
                bricksPositionsRandomizer.GetBricksPositions(grid, properties, difficultyLevel);
            if (bricksControllersPositions == null) return 0;

            var brickCount = 0;

            ClearGrid();
            foreach (var brickPosition in bricksControllersPositions)
            {
                var brickController = brickControllersManager.GetBrickController();
                brickController.ChangeBrickType(brickTypeRandomizer.RollBrick(difficultyLevel));

                var brickControllerTransform = brickController.transform;
                brickControllerTransform.localScale = new Vector2(brickWidth, brickHeight);
                brickControllerTransform.parent = gameObject.transform;

                var brickPos = new Vector2(brickPosition.x * (brickWidth + spacing.x),
                    brickPosition.y * (brickHeight + spacing.y));
                brickControllerTransform.localPosition = brickPos;

                brickController.RestoreBrick();
                if (!brickController.Brick.IsObstacle) brickCount++;
            }

            return brickCount;
        }

        public void ClearGrid()
        {
            foreach (var brickController in brickControllersManager.AllBricks) brickController.DisableBricks();
            brickControllersManager.AllBricks.Clear();
        }

        [ContextMenu("Wipe")]
        public void DestroyBricks()
        {
            var children = new Transform[transform.childCount];
            var i = 0;

            foreach (Transform child in transform)
            {
                children[i] = child;
                i++;
            }

            for (var k = 0; k < children.Length; k++) DestroyImmediate(children[k].gameObject);
        }
    }
}