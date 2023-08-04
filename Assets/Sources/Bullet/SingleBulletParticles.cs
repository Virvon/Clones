using UnityEngine;

public class SingleBulletParticles : BulletParticles
{
    private GameObject _projectile;

    protected override void OnShooted()
    {
        Bullet.Hitted += OnHitted;

        CreateMuzzle();
        CreateProjectile();
    }

    protected override void CreateProjectile() => _projectile = Instantiate(Bullet.BulletData.BulletProjectile, transform.position, transform.rotation, transform);

    protected override void CreateMuzzle() => Instantiate(Bullet.BulletData.BulletMuzzle, transform.position, transform.rotation);

    protected override void OnHitted()
    {
        Destroy(_projectile);

        Instantiate(Bullet.BulletData.BulletHitEffect, transform.position, Quaternion.identity);

        Bullet.Hitted -= OnHitted;
    }
}
