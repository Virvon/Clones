using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AirBullet))]
public class AirBulletParticles : MonoBehaviour
{
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private GameObject HitEffectPrefab;
    [SerializeField] private GameObject MuzzlePrefab;

    private AirBullet _bullet;

    private void Start()
    {
        _bullet = GetComponent<AirBullet>();

        CreateMuzzle();
        CreateProjectile();
        CreateHit();
    }

    private void CreateMuzzle()
    {
        Instantiate(MuzzlePrefab, _bullet.Muzzle.transform.position, Quaternion.identity, _bullet.Muzzle);
    }

    private void CreateProjectile()
    {
        Instantiate(ProjectilePrefab, _bullet.TargetPosition + _bullet.Offset, ProjectilePrefab.transform.rotation, transform);
    }

    private void CreateHit()
    {
        Instantiate(HitEffectPrefab, _bullet.TargetPosition, Quaternion.identity);
    }
}
