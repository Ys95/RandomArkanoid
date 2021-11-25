using Bricks;
using UnityEngine;

namespace Player.BallTypes
{
    [CreateAssetMenu(fileName = "Fire_BallType", menuName = "BallType/FireBallType")]
    public class FireBallType : BallType
    {
        [SerializeField] float destroyRadius;
        [SerializeField] LayerMask bricksLayer;

        readonly Collider2D[] _targetsHit = new Collider2D[12];

        protected override void HandleBrickCollision(Collider2D collider)
        {
            base.HandleBrickCollision(collider);
            DestroyBricksAround();
        }

        void DestroyBricksAround()
        {
            var targetsAmount = Physics2D.OverlapCircleNonAlloc(Ball.Model.BallCollider.bounds.center, destroyRadius,
                _targetsHit, bricksLayer);

            for (var i = 0; i < targetsAmount; i++)
            {
                if (!_targetsHit[i].CompareTag(Tags.Brick)) continue;
                _targetsHit[i].attachedRigidbody.GetComponent<BrickController>().BrickHit(Ball.Model.BallCollider);
            }
        }
    }
}