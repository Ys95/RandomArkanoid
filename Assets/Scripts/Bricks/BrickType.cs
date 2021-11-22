using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public abstract class BrickType : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Collider2D brickCollider;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Transform brickTransform;
    
    [Header("Settings")]
    [SerializeField] BrickController controller;
    [SerializeField] BrickNames brickName;
    [SerializeField] int score;
    [SerializeField] bool isObstacle;
    
    [Header("Effects")]
    [SerializeField] SoundEffect onDestroySoundEffect;
    [SerializeField] ParticleSystem onDestroyParticle;
  
    bool _isDestroyed;

    #region ReadOnlyProperties
    
    public Collider2D BrickCollider => brickCollider;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public Transform BrickTransform => brickTransform;
    public BrickNames BrickName => brickName;
    public bool IsObstacle => isObstacle;
    protected int Score => score;
    
    #endregion
    
    public bool IsDestroyed
    {
        get => _isDestroyed;
        protected set => _isDestroyed = value;
    }

    void OnEnable() => OnBrickEnabled();

    void OnValidate()
    {
        if(transform.parent==null) return;
        
        BrickController getController = transform.parent.GetComponent<BrickController>();
        if (getController != null) controller = getController;
    }

    protected virtual void OnBrickEnabled() 
    {
        _isDestroyed = false;
        if (onDestroyParticle != null)
        {
            Transform particleTransform = onDestroyParticle.transform;
            particleTransform.parent = brickTransform;
            particleTransform.position = brickTransform.position;
        }
    }

    protected virtual int CalculateScore => score;
    public void DestroyBrick()
    {
        if (_isDestroyed) return;
        _isDestroyed = true;
        controller.InvokeBrickDestroyedEvent(brickTransform.position, CalculateScore);
        
        if (onDestroyParticle != null)
        {
            onDestroyParticle.transform.parent = gameObject.transform.parent;
            onDestroyParticle.Play();
        }
        if(onDestroySoundEffect!=null) onDestroySoundEffect.PlayDetached(brickTransform.position);
        gameObject.SetActive(false);
    }
    
    public abstract void HandleOnCollisionEnter(Collider2D other);
}

