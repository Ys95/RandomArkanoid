using UnityEngine;

public class BallModel : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Collider2D ballCollider;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] AudioSource audioSource;

    [Header("Particles")]
    [SerializeField] ParticleSystem onBrickHitParticle;
    [SerializeField] ParticleSystem onBounceParticle;

    [Header("Sounds")]
    [SerializeField] SoundEffect onBounceSound;
    [SerializeField] SoundEffect onBrickHitSound;

    public Collider2D BallCollider => ballCollider;
    public SpriteRenderer Sprite => sprite;

    void Play(SoundEffect effect)
    {
        if (effect == null) return;
        effect.Play(audioSource);
    }

    void Play(ParticleSystem particle)
    {
        if (particle == null) return;
        particle.Play();
    }

    public void PlayOnBrickHitSound()
    {
        Play(onBrickHitSound);
    }

    public void PlayOnBounceSound()
    {
        Play(onBounceSound);
    }

    public void PlayOnBounceParticle()
    {
        Play(onBounceParticle);
    }

    public void PlayOnBrickHitParticle()
    {
        Play(onBrickHitParticle);
    }
}