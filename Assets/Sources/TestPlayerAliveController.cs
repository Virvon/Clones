using UnityEngine;

public class TestPlayerAliveController : MonoBehaviour
{
    [SerializeField] private TestSceneManager _testSceneManager;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.Died += Ondied;
    }

    private void OnDisable()
    {
        _player.Died -= Ondied;
    }

    private void Ondied(IDamageble obj)
    {
        _testSceneManager.LoadScene();
    }
}
