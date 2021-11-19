using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public class DurableBrickType : BrickType
{
    [SerializeField] TextMeshPro durabilityText;
    [SerializeField] GameObject destructibleLayer;
    [SerializeField] ParticleSystem onHitParticle;
    [SerializeField] AudioSource audioSource;
    [SerializeField] SoundEffect onHitSoundEffect;

    [Space]
    [SerializeField] int minDurability;
    [SerializeField] int maxDurability;

    int _rolledDurability;
    int _currentDurability;
    
    
    int RollDurability => Random.Range(minDurability, maxDurability + 1);
    protected override int CalculateScore => _rolledDurability + Score;
    protected override void OnBrickEnabled()
    {
        base.OnBrickEnabled();
        _rolledDurability = RollDurability;
        _currentDurability = _rolledDurability;
        
        durabilityText.text = _currentDurability.ToString();
        destructibleLayer.SetActive(true);
    }
    
    
    void ReduceDurability()
    {
        _currentDurability--;

        if (_currentDurability == 0)
        {
            destructibleLayer.SetActive(false);
        }
        else if (_currentDurability < 0)
        {
            DestroyBrick();
        }
        else
        {
            durabilityText.text = _currentDurability.ToString();
            onHitParticle.Play();
            onHitSoundEffect.Play(audioSource);
        }
    }

    public override void HandleOnCollisionEnter(Collider2D other) => ReduceDurability();
}
