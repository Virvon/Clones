using UnityEngine;

public class EnemyHealthbar : Healthbar
{
    [SerializeField] private Vector3 _offSet;

    protected override IHealthble Healthble => _healthble;

    private IHealthble _healthble;
    private Camera _camera;

    protected override void Start()
    {
        
        _camera = Camera.main;
        base.Start();
    }

    public void Init(IHealthble healthble) => _healthble = healthble;

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
        transform.Rotate(0, 180, 0);
    }
}
