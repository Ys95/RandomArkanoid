using Effects;
using UnityEngine;

namespace Bricks
{
    public class IndestructibleBrickType : BrickType
    {
        [Space]
        [Header("Indestructible brick")]
        [SerializeField] AudioSource audioSource;
        [SerializeField] SoundEffect onHitEffect;

        public override void HandleOnCollisionEnter(Collider2D other)
        {
            onHitEffect.Play(audioSource);
        }
    }
}