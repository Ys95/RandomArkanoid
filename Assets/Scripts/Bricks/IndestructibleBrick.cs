using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndestructibleBrick : BrickType
{
    [Space]
    [Header("Indestructible brick")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] SoundEffect onHitEffect;


    
    public override void HandleOnCollisionEnter(Collider2D other)
    {
        onHitEffect.Play(audioSource);
    }
}
