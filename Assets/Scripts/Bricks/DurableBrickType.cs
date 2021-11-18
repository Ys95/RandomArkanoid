using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public class DurableBrickType : BrickType
{
    [SerializeField] TextMeshPro durabilityText;
    [SerializeField] GameObject layer;

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
        layer.SetActive(true);
    }
    
    
    void ReduceDurability()
    {
        _currentDurability--;

        if (_currentDurability == 0)
        {
            layer.SetActive(false);
        }
        else if (_currentDurability < 0) DestroyBrick();
        else durabilityText.text = _currentDurability.ToString();
    }
    
    public override void HandleOnCollisionEnter(Collider2D other)
    {
        ReduceDurability();
    }
}
