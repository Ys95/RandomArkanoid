using System;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    static Action _onItemPickup;
    
    [SerializeField] GameObject visual;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(Tags.Racket)) return;
        _onItemPickup?.Invoke();
        InvokeEvent();
        Destroy(gameObject);
    }

    protected abstract void InvokeEvent();
}