using UnityEngine;

[RequireComponent(typeof(Bullet))]
public abstract class BulletParticles : MonoBehaviour
{
    protected Bullet Bullet { get; private set; }

    private void OnEnable()
    {
        Bullet = GetComponent<Bullet>();

        Bullet.Shooted += OnShooted;
    }

    private void OnDisable() =>
        Bullet.Shooted -= OnShooted;

    protected abstract void OnShooted();
    protected abstract void CreateProjectile();
    protected abstract void CreateMuzzle();
    protected abstract void OnHitted();
}
