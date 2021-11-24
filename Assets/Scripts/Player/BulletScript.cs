using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rb;

    [Header("Effects")]
    [SerializeField] TrailRenderer trail;
    [SerializeField] SoundEffect onHitSound;

    public Rigidbody2D Rb => rb;

    void OnEnable()
    {
        trail.Clear();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        onHitSound.PlayDetached(transform.position);
        trail.Clear();
        gameObject.SetActive(false);
    }
}