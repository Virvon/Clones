using UnityEngine;

public class EnemyHealthbar : Healthbar
{
    protected override IHealthble Healthble => _healthble;

    private IHealthble _healthble;
    private Camera _camera;

    protected override void Start()
    {
        _camera = Camera.main;
        base.Start();
    }

    public void Init(Enemy enemy) => _healthble = enemy;

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
        transform.Rotate(0, 180, 0);
    }
}
