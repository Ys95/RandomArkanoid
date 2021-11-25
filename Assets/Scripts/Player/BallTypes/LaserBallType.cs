using UnityEngine;

namespace Player.BallTypes
{
    [CreateAssetMenu(fileName = "Laser_BallType", menuName = "BallType/LaserBallType")]
    public class LaserBallType : BallType
    {
        public override void HandleOnTriggerEnter(Collider2D collider)
        {
            base.HandleOnTriggerEnter(collider);
            if (collider.CompareTag(Tags.Brick)) HandleBrickCollision(collider);
        }
    }
}