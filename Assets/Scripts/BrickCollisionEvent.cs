using UnityEngine;

public class BrickCollisionEvent : MonoBehaviour
{
    [SerializeField] Collider2D _brickCollider;

    public delegate void BrickDestroyed();
    public event BrickDestroyed OnBrickDestroy;

    void OnCollisionEnter2D(Collision2D collision)
    {
        _brickCollider.attachedRigidbody.SendMessage("OnCollisionEnter2D", collision);
        if (collision.collider.tag != Tags.BALL) return;

        OnBrickDestroy?.Invoke();
    }
}
