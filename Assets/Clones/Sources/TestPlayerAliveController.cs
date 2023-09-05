using Clones.UI;
using UnityEngine;

public class TestPlayerAliveController : MonoBehaviour
{
    [SerializeField] private TestSceneManager _testSceneManager;
    [SerializeField] private Player _player;
    [SerializeField] private GameOverView _gameOverView;

    private void OnEnable()
    {
        _player.Died += Ondied;
    }

    private void OnDisable()
    {
        _player.Died -= Ondied;
    }

    private void Ondied(IDamageable obj)
    {
        //_testSceneManager.LoadScene();
        _gameOverView.Open();
    }
}
