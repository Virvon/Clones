using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Clones.SceneLoadUtility
{
    public class SceneLoader : MonoBehaviour
    {
        private static SceneLoader _instance;

        private AsyncOperation _loadingScene;
        private Coroutine _callbackWaiter;

        private void Start() => _instance = this;

        public static void Load(string sceneName, Action callback)
        {
            _instance._loadingScene = SceneManager.LoadSceneAsync(sceneName);
            _instance._loadingScene.allowSceneActivation = false;


            if (_instance._callbackWaiter != null)
                _instance.StopCoroutine(_instance._callbackWaiter);

            _instance._callbackWaiter = _instance.StartCoroutine(_instance.CallbackWaiter(callback));
        }

        public static void Switch() => _instance._loadingScene.allowSceneActivation = true;

        private IEnumerator CallbackWaiter(Action callback)
        {
            yield return _instance._loadingScene.isDone;

            callback.Invoke();
        }
    }
}
