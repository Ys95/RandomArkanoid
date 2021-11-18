using UnityEngine;

[CreateAssetMenu(fileName = "Fire_BallType", menuName = "BallType/FireBallType")]
public class FireBallType : BallType
{
    [SerializeField] float destroyRadius;
    [SerializeField] LayerMask bricksLayer;

    protected override void HandleBrickCollision(Collider2D collider)
    {
        base.HandleBrickCollision(collider);
        DestroyBricksAround();
    }

    void DestroyBricksAround()
    {
        Collider2D[] bricks = Physics2D.OverlapCircleAll(Ball.Transform.position, destroyRadius);
        foreach (Collider2D brick in bricks)
        {
            if (!brick.CompareTag(Tags.Brick)) continue;
            brick.attachedRigidbody.GetComponent<BrickController>().BrickHit(Ball.Model.BallCollider);
        }
    }
}