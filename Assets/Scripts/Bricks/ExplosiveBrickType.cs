using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBrickType : BrickType
{
    [Space]
    [SerializeField] ExplosionRaycastPoints explosionRaycastPoints;
    [SerializeField] float explosionRadius;
    
    [Space]
    [SerializeField] LayerMask explosionTargetsMask;
    [SerializeField] AudioSource audioSource;
    [SerializeField] SoundEffect explosionSoundEffect;

    bool _exploded;

    [Serializable]
    struct ExplosionRaycastPoints
    {
        [SerializeField] Transform up;
        [SerializeField] Transform down;
        [SerializeField] Transform left;
        [SerializeField] Transform right;
        
        public Vector2 Up => up.position;
        public Vector2 Down => down.position;
        public Vector2 Left => left.position;
        public Vector2 Right => right.position;
    }
    
    protected override void OnBrickEnabled()
    {
        base.OnBrickEnabled();
        _exploded = false;
    }

    void Explode()
    {
        _exploded = true;
        
        RaycastHit2D[] raycasts = new RaycastHit2D[4];
        
        raycasts[0] = Physics2D.Raycast(explosionRaycastPoints.Up, Vector2.up, explosionRadius, explosionTargetsMask);
        raycasts[1] = Physics2D.Raycast(explosionRaycastPoints.Down, Vector2.down, explosionRadius, explosionTargetsMask);
        raycasts[2] = Physics2D.Raycast(explosionRaycastPoints.Left, Vector2.left, explosionRadius, explosionTargetsMask);
        raycasts[3] = Physics2D.Raycast(explosionRaycastPoints.Right, Vector2.right, explosionRadius, explosionTargetsMask);

        foreach (RaycastHit2D rayHit in raycasts)
        {
            var brick = rayHit.collider;
            if(brick == null ) continue;
            
            BrickController brickController = brick.attachedRigidbody.GetComponent<BrickController>();
            if(brickController==null) continue;
            
            brickController.BrickHit(BrickCollider);
        }
        explosionSoundEffect.PlayDetached(transform.position);
        DestroyBrick();
    }
    
    public override void HandleOnCollisionEnter(Collider2D other)
    {
        if (_exploded) return;

        Explode();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(explosionRaycastPoints.Up, Vector2.up*explosionRadius);
        Gizmos.DrawLine(explosionRaycastPoints.Down, Vector2.down*explosionRadius);
        Gizmos.DrawLine(explosionRaycastPoints.Left, Vector2.left*explosionRadius);
        Gizmos.DrawLine(explosionRaycastPoints.Right, Vector2.right*explosionRadius);
    }
}
