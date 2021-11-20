using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBrickType : BrickType
{
    [Space]
    [SerializeField] Vector2 explosionRadius;
    [SerializeField] int maxExplosionTargets;
    
    [Space]
    [SerializeField] LayerMask explosionTargetsLayer;
    [SerializeField] AudioSource audioSource;
    [SerializeField] SoundEffect explosionSoundEffect;

    bool _exploded;
    Collider2D[] _hitByExplosion;

    void Awake()
    {
        _hitByExplosion = new Collider2D[maxExplosionTargets];
    }

    protected override void OnBrickEnabled()
    {
        base.OnBrickEnabled();
        _exploded = false;
    }

    void Explode()
    {
        _exploded = true;

        int targetsHit = Physics2D.OverlapBoxNonAlloc(BrickCollider.bounds.center, explosionRadius, 0f, _hitByExplosion, explosionTargetsLayer);
        Mathf.Clamp(targetsHit, 0, maxExplosionTargets);
        Debug.Log("Explosive brick targets hit: " + targetsHit);
       
        for (int i = 0; i < targetsHit; i++)
        {
            if (_hitByExplosion[i] == null) continue;
                
            BrickController controller = _hitByExplosion[i].attachedRigidbody.GetComponent<BrickController>();
            if(controller==null) continue;
            
            controller.BrickHit(BrickCollider);
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
        Gizmos.DrawWireCube(BrickCollider.bounds.center, explosionRadius);
    }
}
