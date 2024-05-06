using UnityEngine;

public class SplashBulletParticles : BulletParticles
{
    private GameObject _projectile;

    protected override void OnShooted()
    {
        ((HittableBullet)Bullet).Hitted += OnHitted;

        CreateMuzzle();
        CreateProjectile();
    }

    protected override void CreateProjectile() => 
        _projectile = Instantiate(Bullet.BulletData.BulletProjectile, transform.position, transform.rotation, transform);

    protected override void CreateMuzzle() => 
        Instantiate(Bullet.BulletData.BulletMuzzle, transform.position, transform.rotation);

    protected override void OnHitted()
    {
        Destroy(_projectile);

        Instantiate(Bullet.BulletData.BulletHitEffect, transform.position, Quaternion.identity);

        ((HittableBullet)Bullet).Hitted -= OnHitted;
    }
}
