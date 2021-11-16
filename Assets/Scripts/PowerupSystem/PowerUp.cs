using System;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] GameObject visual;

    public static Action OnPowerupPickup;
    
    protected  abstract void InvokeEvent();
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(Tags.Racket)) return;
        OnPowerupPickup?.Invoke();
        InvokeEvent();
        Destroy(gameObject);
    }
}