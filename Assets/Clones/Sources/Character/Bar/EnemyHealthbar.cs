using UnityEngine;

public class EnemyHealthbar : Healthbar
{
    private Camera _camera;

    public void Init(IHealthChanger healthble) =>
        TakeHealthble(healthble);

    private void Awake() => 
        _camera = Camera.main;

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
        transform.Rotate(0, 180, 0);
    }
}
