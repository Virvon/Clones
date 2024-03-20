using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Clones.Infrastructure
{
    public class SceneLoader
    {
        private AsyncOperation _waitNextScene;

        public void Load(string scene, bool allowSceneActivation = true, Action callback = null)
        { 
            if(SceneManager.GetActiveScene().name == scene)
            {
                callback?.Invoke();

                return;
            }

            _waitNextScene = SceneManager.LoadSceneAsync(scene);
            _waitNextScene.allowSceneActivation = allowSceneActivation;
            _waitNextScene.completed += _ => callback?.Invoke();
        }

        public void AllowSceneActivation()
        {
            if (_waitNextScene != null || _waitNextScene.allowSceneActivation == false)
                _waitNextScene.allowSceneActivation = true;
        }
    }
}