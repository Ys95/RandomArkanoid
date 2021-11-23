using UnityEngine;

public class NormalBrickType : BrickType
{
    public override void HandleOnCollisionEnter(Collider2D other)
    {
        DestroyBrick();
    }
}