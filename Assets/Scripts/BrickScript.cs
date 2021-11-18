using System;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public delegate void BrickDestroyed(Vector2 pos);
    public static event BrickDestroyed OnBrickDestroyed;

    [SerializeField] GameObject brickVisuals;
    [SerializeField] ParticleSystem destroyParticle;

    bool _isDestroyed;
    
    public void OnEnable()
    {
        CancelInvoke(nameof(DisableThis));
        brickVisuals.SetActive(true);
        _isDestroyed = false;
    }
    
    public void DestroyBrick()
    {
        if(_isDestroyed) return;
        
        _isDestroyed = true;
        brickVisuals.SetActive(false);
        OnBrickDestroyed?.Invoke(transform.position);
        DisableThis();
        if (destroyParticle == null)
        {
            DisableThis();
            return;
        }
        
        destroyParticle.Play();
        DisableThis();
       Invoke(nameof(DisableThis), destroyParticle.main.duration+0.01f);
    }

    void DisableThis() => gameObject.SetActive(false);

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag(Tags.Ball)) return;
        DestroyBrick();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(Tags.Ball)) return;
        DestroyBrick();
    }

    void OnDisable()
    {
        CancelInvoke(nameof(DisableThis));
    }
}
