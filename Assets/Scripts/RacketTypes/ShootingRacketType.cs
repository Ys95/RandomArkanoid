using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
            if (!bullet.gameObject.activeInHierarchy) return bullet;
        }

        GameObject newBullet = Instantiate(bulletPrefab, Racket.Player);
        BulletScript bulletScript = newBullet.GetComponent<BulletScript>();
        _bulletPool.Add(bulletScript);
        return bulletScript;
    }
    
    public override void HandleFireAction(InputAction.CallbackContext context)
    {
        if(!context.started) return;
        
        ShootingRacketModel shootingRacketModel = (ShootingRacketModel) Model;
        
        BulletScript bullet1 = GetPooledBullet();
        bullet1.gameObject.SetActive(true);
        bullet1.transform.position = shootingRacketModel.GunBarrel1.position;
        bullet1.Rigidbody.velocity = Vector2.up *bulletSpeed;
        
        BulletScript bullet2 = GetPooledBullet();
        bullet2.gameObject.SetActive(true);
        bullet2.transform.position = shootingRacketModel.GunBarrel2.position;
        bullet2.Rigidbody.velocity = Vector2.up *bulletSpeed;
        
        shootingRacketModel.PlayOnShootEffect();
    }
}
