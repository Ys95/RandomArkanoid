using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketModel : MonoBehaviour
{
    [SerializeField] Collider2D _collider;
    [SerializeField] Collider2D _ballBounceArea;
    [SerializeField] SpriteRenderer _sprite;
    
    public Collider2D Collider => _collider;

    public Collider2D BallBounceArea => _ballBounceArea;

    public SpriteRenderer Sprite => _sprite;
}
