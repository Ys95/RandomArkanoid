using System;
using UnityEngine;

[Serializable]
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

    public bool IsDestroyed
    {
        get => _isDestroyed;
        protected set => _isDestroyed = value;
    }

    protected virtual int CalculateScore => score;

    void OnEnable()
    {
        OnBrickEnabled();
    }

    void OnValidate()
    {
        if (transform.parent == null) return;

        var getController = transform.parent.GetComponent<BrickController>();
        if (getController != null) controller = getController;
    }

    protected virtual void OnBrickEnabled()
    {
        _isDestroyed = false;
        if (onDestroyParticle != null)
        {
            var particleTransform = onDestroyParticle.transform;
            particleTransform.parent = brickTransform;
            particleTransform.position = brickTransform.position;
        }
    }

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

        if (onDestroySoundEffect != null) onDestroySoundEffect.PlayDetached(brickTransform.position);
        gameObject.SetActive(false);
    }

    public abstract void HandleOnCollisionEnter(Collider2D other);

    #region ReadOnlyProperties

    public Collider2D BrickCollider => brickCollider;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public Transform BrickTransform => brickTransform;
    public BrickNames BrickName => brickName;
    public bool IsObstacle => isObstacle;
    protected int Score => score;

    #endregion
}