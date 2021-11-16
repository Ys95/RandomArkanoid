using UnityEngine;

public class BrickCollisionEvent : MonoBehaviour
{
    [SerializeField] Collider2D brickCollider;

    public delegate void BrickDestroyed();
    public event BrickDestroyed OnBrickDestroy;

    void OnCollisionEnter2D(Collision2D collision)
    {
        brickCollider.attachedRigidbody.SendMessage("OnCollisionEnter2D", collision);
        if (!collision.collider.CompareTag(Tags.Ball)) return;

        OnBrickDestroy?.Invoke();
    }
}
