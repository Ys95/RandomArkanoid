using UnityEngine;
using System.Collections;
using System;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] GameObject _visual;

    protected  abstract void InvokeEvent();
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(Tags.RACKET)) return;
        InvokeEvent();
        Destroy(gameObject);
    }
}