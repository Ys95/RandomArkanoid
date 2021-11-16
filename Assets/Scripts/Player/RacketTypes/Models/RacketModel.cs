using UnityEngine;

public class RacketModel : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Collider2D modelCollider;
    [SerializeField] Collider2D ballBounceArea;
    [SerializeField] SpriteRenderer sprite;
    
    public Collider2D ModelCollider => modelCollider;

    public Collider2D BallBounceArea => ballBounceArea;

    public SpriteRenderer Sprite => sprite;
}
