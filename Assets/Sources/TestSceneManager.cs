using UnityEngine.SceneManagement;
using UnityEngine;

public class TestSceneManager : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
