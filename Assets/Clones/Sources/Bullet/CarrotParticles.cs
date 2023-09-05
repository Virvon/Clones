using UnityEngine;

public class CarrotParticles : SingleBulletParticles
{
    protected override void OnHitted()
    {
        Transform targetTransform = ((MonoBehaviour)((SingleBullet)Bullet).HitTarget).transform;
        var hitEffect = Instantiate(Bullet.BulletData.BulletHitEffect, transform.position, Projectile.transform.rotation, targetTransform);

        //hitEffect.transform.rotation = Quaternion.FromToRotation(hitEffect.transform.forward, Projectile.transform.up);

        Destroy(Projectile);
        Bullet.Hitted -= OnHitted;
    }
}
