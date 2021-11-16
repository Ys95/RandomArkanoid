using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] GameObject visuals;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] AudioSource audioSource;

    [SerializeField] SoundEffect onHitSound;
    public Rigidbody2D Rb => rb;

    void OnEnable()
    {
        visuals.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        onHitSound.Play(audioSource);
        visuals.SetActive(false);
        Invoke(nameof(Disable), onHitSound.Length);
    }

    void Disable() => gameObject.SetActive(false);
}
