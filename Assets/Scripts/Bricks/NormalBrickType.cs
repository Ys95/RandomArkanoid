using UnityEngine;

namespace Bricks
{
    public class NormalBrickType : BrickType
    {
        public override void HandleOnCollisionEnter(Collider2D other)
        {
            DestroyBrick();
        }
    }
}