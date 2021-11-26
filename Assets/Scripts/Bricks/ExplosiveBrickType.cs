using Effects;
using UnityEngine;

namespace Bricks
{
    public class ExplosiveBrickType : BrickType
    {
        [Space]
        [Header("Explosive brick")]
        [SerializeField] Vector2 explosionRadius;
        [SerializeField] int maxExplosionTargets;
        [SerializeField] SoundEffect explosionSoundEffect;

        [Space]
        [SerializeField] LayerMask explosionTargetsLayer;

        bool _exploded;
        Collider2D[] _hitByExplosion;

        void Awake()
        {
            _hitByExplosion = new Collider2D[maxExplosionTargets];
        }

        void OnDrawGizmos()
        {
            if (BrickCollider == null) return;
            Gizmos.DrawWireCube(BrickCollider.bounds.center, explosionRadius);
        }

        protected override void OnBrickEnabled()
        {
            base.OnBrickEnabled();
            _exploded = false;
        }

        void Explode()
        {
            _exploded = true;

            var targetsHit = Physics2D.OverlapBoxNonAlloc(BrickCollider.bounds.center, explosionRadius, 0f,
                _hitByExplosion,
                explosionTargetsLayer);
            Mathf.Clamp(targetsHit, 0, maxExplosionTargets);
            Debug.Log("Explosive brick targets hit: " + targetsHit);

            for (var i = 0; i < targetsHit; i++)
            {
                if (_hitByExplosion[i] == null) continue;

                var controller = _hitByExplosion[i].attachedRigidbody.GetComponent<BrickController>();
                if (controller == null) continue;

                controller.BrickHit(BrickCollider);
            }

            explosionSoundEffect.PlayDetached(transform.position);
            DestroyBrick();
        }

        public override void HandleOnCollisionEnter(Collider2D other)
        {
            if (_exploded) return;

            Explode();
        }
    }
}