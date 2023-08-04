using UnityEngine;

[RequireComponent(typeof(SplashBullet))]
public class SplashBulletParticles : MonoBehaviour
{
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private GameObject HitEffectPrefab;
    [SerializeField] private GameObject MuzzlePrefab;

    private Bullet _bullet;
    private GameObject _projectile;

    private void OnEnable()
    {
        _bullet = GetComponent<Bullet>();

        _bullet.Hitted += OnHitted;
    }

    private void Start()
    {
        CreateMuzzle();
        CreateProjectile();
    }

    private void OnDisable() => _bullet.Hitted -= OnHitted;

    private void CreateMuzzle()
    {
        Instantiate(MuzzlePrefab, transform.position, transform.rotation);
    }

    private void CreateProjectile()
    {
        _projectile = Instantiate(ProjectilePrefab, transform.position, transform.rotation, transform);
    }

    private void OnHitted()
    {
        Destroy(_projectile);

        Instantiate(HitEffectPrefab, transform.position, Quaternion.identity);
    }
}
