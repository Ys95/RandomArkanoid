using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBrickType : BrickType
{
    [SerializeField] LayerMask explosionTargetsMask;
    [SerializeField] float explosionRadius;
    [SerializeField] ParticleSystem explosionParticle;
    
    protected override void OnBrickEnabled()
    {
        base.OnBrickEnabled();
        
        Transform explosionParticleTransform = explosionParticle.transform;
        explosionParticleTransform.parent = BrickTransform;
        explosionParticleTransform.position = BrickTransform.position;
    }
    
    void Explode()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(BrickCollider.bounds.center, explosionRadius, explosionTargetsMask);

        explosionParticle.transform.parent = gameObject.transform.parent;
        explosionParticle.Play();
        
        foreach (Collider2D target in targets)
        {
            BrickController brickController = target.GetComponent<BrickController>();
            if(brickController==null) return;
            brickController.BrickHit(BrickCollider);
        }
    }
    
    public override void HandleOnCollisionEnter(Collider2D other)
    {   
        Explode();
        DestroyBrick();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(BrickCollider.bounds.center, explosionRadius);
    }
}
