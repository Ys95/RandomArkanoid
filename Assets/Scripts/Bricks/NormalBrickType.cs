using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBrickType : BrickType
{
    public override void HandleOnCollisionEnter(Collider2D other)
    {
        DestroyBrick();
    }
}