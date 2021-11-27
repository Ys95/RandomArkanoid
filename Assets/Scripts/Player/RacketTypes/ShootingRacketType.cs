using System.Collections.Generic;
using Player.RacketTypes.Models;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.RacketTypes
{
    [CreateAssetMenu(fileName = "Shooting_RacketType", menuName = "RacketType/ShootingRacketType")]
    public class ShootingRacketType : RacketType
    {
        [SerializeField] GameObject bulletPrefab;

        [Space]
        [SerializeField] float bulletSpeed;

        readonly List<BulletScript> _bulletPool = new();

        BulletScript GetPooledBullet()
        {
            foreach (BulletScript bullet in _bulletPool)
            {
                if (!bullet.gameObject.activeInHierarchy)
                    return bullet;
            }

            GameObject newBullet = Instantiate(bulletPrefab, Racket.Player);
            newBullet.SetActive(false);
            BulletScript bulletScript = newBullet.GetComponent<BulletScript>();
            _bulletPool.Add(bulletScript);
            return bulletScript;
        }

        void DisableAllBullets()
        {
            foreach (BulletScript bullet in _bulletPool)
            {
                if (bullet.gameObject.activeInHierarchy)
                    bullet.gameObject.SetActive(false);
            }
        }

        public override void HandleFireAction(InputAction.CallbackContext context)
        {
            if (!context.started) return;

            ShootingRacketModel shootingRacketModel = (ShootingRacketModel) Model;

            BulletScript bullet1 = GetPooledBullet();
            bullet1.transform.position = shootingRacketModel.GunBarrel1.position;
            bullet1.gameObject.SetActive(true);
            bullet1.Rb.velocity = Vector2.up * bulletSpeed;

            BulletScript bullet2 = GetPooledBullet();
            bullet2.transform.position = shootingRacketModel.GunBarrel2.position;
            bullet2.gameObject.SetActive(true);
            bullet2.Rb.velocity = Vector2.up * bulletSpeed;

            shootingRacketModel.PlayOnShootEffect();
        }

        public override void OnModeExit()
        {
            base.OnModeExit();
            DisableAllBullets();
        }
    }
}