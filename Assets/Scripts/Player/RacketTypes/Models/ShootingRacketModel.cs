using Effects;
using UnityEngine;

namespace Player.RacketTypes.Models
{
    public class ShootingRacketModel : RacketModel
    {
        [Space]
        [SerializeField] Transform gunBarrel1;
        [SerializeField] Transform gunBarrel2;
        [SerializeField] AudioSource audioSource;

        [Space]
        [SerializeField] ParticleSystem[] shootEffects;

        [Space]
        [SerializeField] SoundEffect shootSound;
        public Transform GunBarrel1 => gunBarrel1;
        public Transform GunBarrel2 => gunBarrel2;

        public void PlayOnShootEffect()
        {
            foreach (var effect in shootEffects)
            {
                effect.Play();
                shootSound.Play(audioSource);
            }
        }
    }
}