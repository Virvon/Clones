using UnityEngine;

public class TopDownBulletParticles : BulletParticles
{
    protected override void OnShooted()
    {
        CreateMuzzle();
        CreateProjectile();
        OnHitted();
    }

    protected override void CreateProjectile() => Instantiate(Bullet.BulletData.BulletProjectile, new Vector3(((TopDownBullet)Bullet).TargetPosition.x, ((TopDownBullet)Bullet).TargetPosition.y + ((TopDownBullet)Bullet).UpOffset, ((TopDownBullet)Bullet).TargetPosition.z), Bullet.BulletData.BulletProjectile.transform.rotation, transform);

    protected override void CreateMuzzle() => Instantiate(Bullet.BulletData.BulletMuzzle, ((TopDownBullet)Bullet).Muzzle.transform.position, Quaternion.identity, ((TopDownBullet)Bullet).Muzzle);

    protected override void OnHitted() => Instantiate(Bullet.BulletData.BulletHitEffect, ((TopDownBullet)Bullet).TargetPosition, Quaternion.identity);
}
