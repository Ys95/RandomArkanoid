using UnityEngine;

public class BallModel : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Collider2D ballCollider;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource idleAudioSource;

    [Header("Particles")] 
    [SerializeField] ParticleSystem onCollisionParticle;
    [SerializeField] ParticleSystem onRacketHitParticle;

    [Header("Sounds")] 
    [SerializeField] SoundEffect idleSound;
    [SerializeField] SoundEffect onCollisionSound;
    [SerializeField] SoundEffect onRacketHitSound;
    [SerializeField] SoundEffect onBorderHitSound;
    

    public Collider2D BallCollider => ballCollider;
    public SpriteRenderer Sprite => sprite;

    void Awake()
    {
        if (idleSound == null || idleAudioSource==null) return;
        idleSound.PlayLoop(idleAudioSource);
    }

    void Play(SoundEffect effect)
    {
        if(effect==null) return;
        effect.Play(audioSource);
    }

    void Play(ParticleSystem particle)
    {
        if(particle==null) return;
        particle.Play();
    }

    public void PlayOnCollisionSound() => Play(onCollisionSound);
    public void PlayOnRacketHitSound() => Play(onRacketHitSound);
    public void PlayOnBorderHitSound() => Play(onBorderHitSound);
    
    public void PlayOnCollisionParticle() => Play(onCollisionParticle);
    public void PlayOnRacketHitParticle() => Play(onRacketHitParticle);
}
