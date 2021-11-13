using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fire_BallMode", menuName = "BallModes/FireBallMode")]
public class FireBallMode : BallMode
{
    [SerializeField] Vector2 _destroyRadius;
    [SerializeField] LayerMask _bricksLayer;
    
    protected override void HandleBrickCollision(Collision2D collision2D)
    {
        DestroyBricksAround();
    }

    void DestroyBricksAround()
    {
        RaycastHit2D[] raycastHits = new RaycastHit2D[4];

        var position = Ball.Transform.position;
        
        raycastHits[0] = Physics2D.Raycast(position, Vector2.down, _destroyRadius.y, _bricksLayer);
        raycastHits[1] = Physics2D.Raycast(position, Vector2.up, _destroyRadius.y, _bricksLayer);
        raycastHits[2] = Physics2D.Raycast(position, Vector2.right, _destroyRadius.x, _bricksLayer);
        raycastHits[3] = Physics2D.Raycast(position, Vector2.left, _destroyRadius.x, _bricksLayer);

        foreach (RaycastHit2D raycastHit in raycastHits)
        {
            if (raycastHit.collider == null) continue;

            Debug.Log("hit collider: " + raycastHit.collider.name);
            if (raycastHit.collider.CompareTag(Tags.BRICK))
            {
                raycastHit.collider.attachedRigidbody.GetComponent<BrickScript>().DestroyBrick();
            }
        }
    }
}