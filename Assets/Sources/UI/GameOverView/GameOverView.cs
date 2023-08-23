using Clones.Animation;
using Clones.SceneLoadUtility;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GameOverView : MonoBehaviour
{
    [SerializeField] private GameObject _view;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();   
        _view.SetActive(false);
    }

    public void Open() => _animator.SetBool(Animations.UI.Bools.IsOpened, true);

    public void Close() => _animator.SetBool(Animations.UI.Bools.IsOpened, false);

    public void Test()
    {
        SceneLoader.Load("TestMainScene", ()=>
        {
            Debug.Log("callbacked");
            SceneLoader.Switch();
        });
    }
}
