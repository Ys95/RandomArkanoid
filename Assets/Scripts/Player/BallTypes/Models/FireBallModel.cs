using UnityEngine;

public class FireBallModel : BallModel
{
    [SerializeField] ParticleSystem onCollisionParticle;

    void OnCollisionEnter2D(Collision2D other)
    {
        onCollisionParticle.Play();
    }
}
