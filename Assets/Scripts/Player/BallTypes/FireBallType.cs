using UnityEngine;

[CreateAssetMenu(fileName = "Fire_BallType", menuName = "BallType/FireBallType")]
public class FireBallType : BallType
{
    [SerializeField] float destroyRadius;
    [SerializeField] LayerMask bricksLayer;
    [SerializeField] int maxTargets;

    Collider2D[] _targetsHit;

    public override void OnModeEnter(Ball ball)
    {
        base.OnModeEnter(ball);
        if (_targetsHit == null) _targetsHit = new Collider2D[maxTargets];
    }

    protected override void HandleBrickCollision(Collider2D collider)
    {
        base.HandleBrickCollision(collider);
        DestroyBricksAround();
    }

    void DestroyBricksAround()
    {
        var targetsAmount = Physics2D.OverlapCircleNonAlloc(Ball.Transform.position, destroyRadius, _targetsHit);

        for (int i = 0; i < targetsAmount; i++)
        {
            if (!_targetsHit[i].CompareTag(Tags.Brick)) continue;
            _targetsHit[i].attachedRigidbody.GetComponent<BrickController>().BrickHit(Ball.Model.BallCollider);
        }
    }
}