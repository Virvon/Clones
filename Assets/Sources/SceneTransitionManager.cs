using UnityEngine;
using Agava.YandexGames;
using Clones.SceneLoadUtility;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private string _nextScene;
    [SerializeField] private LoadingPanel _loadingPanel;
    [SerializeField] private bool _showAdd;

    private bool _addIsOvered;
    private bool _sceneIsLoaded;
    private bool _TransitionIsAllowed = true;

    public void TryTransit()
    {
        if (_TransitionIsAllowed == false)
            return;

        _addIsOvered = !_showAdd;

        _sceneIsLoaded = false;

        StartCoroutine(TransitWaiter());

        _loadingPanel.Open();

        SceneLoader.Load(_nextScene, () =>
        {
            _sceneIsLoaded = true;
        });

        if (_showAdd)
        {
#if !UNITY_EDITOR && UNITY_WEBGL
        InterstitialAd.Show(onCloseCallback: (addIsClosed) =>
        {
            _addIsClosed = addIsClosed;
        });
#else
            _addIsOvered = true;
#endif
        }
    }

    private IEnumerator TransitWaiter()
    {
        yield return _addIsOvered && _sceneIsLoaded;

        SceneLoader.Switch();
    }
}
