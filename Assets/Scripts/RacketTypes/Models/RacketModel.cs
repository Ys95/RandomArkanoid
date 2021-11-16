using UnityEngine;

public class RacketModel : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Collider2D collider;
    [SerializeField] Collider2D ballBounceArea;
    [SerializeField] SpriteRenderer sprite;
    
    public Collider2D Collider => collider;

    public Collider2D BallBounceArea => ballBounceArea;

    public SpriteRenderer Sprite => sprite;
}
