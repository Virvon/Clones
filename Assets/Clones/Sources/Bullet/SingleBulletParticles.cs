using UnityEngine;

public class SingleBulletParticles : BulletParticles
{
    protected GameObject Projectile { get; private set; }

    protected override void OnShooted()
    {
        Bullet.Hitted += OnHitted;

        CreateMuzzle();
        CreateProjectile();
    }

    protected override void CreateProjectile() => Projectile = Instantiate(Bullet.BulletData.BulletProjectile, transform.position, transform.rotation, transform);

    protected override void CreateMuzzle() => Instantiate(Bullet.BulletData.BulletMuzzle, transform.position, transform.rotation);

    protected override void OnHitted()
    {
        Destroy(Projectile);

        Instantiate(Bullet.BulletData.BulletHitEffect, transform.position, Quaternion.identity);

        Bullet.Hitted -= OnHitted;
    }
}
