using UnityEngine;

public class PlayerRebornEffect : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;

    private GameObject _rebornEffect;
    private Vector3 _effectOffset;

    public void Init(GameObject rebornEffect, Vector3 effectOffset)
    {
        _rebornEffect = rebornEffect;
        _effectOffset = effectOffset;

        _playerHealth.Reborned += OnReborned;
    }

    private void OnDestroy() =>
        _playerHealth.Reborned -= OnReborned;

    private void OnReborned() => 
        Instantiate(_rebornEffect, transform.position + _effectOffset, Quaternion.identity, transform);
}
