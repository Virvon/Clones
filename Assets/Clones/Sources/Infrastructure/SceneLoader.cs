using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Clones.Infrastructure
{
    public class SceneLoader
    {
        AsyncOperation _waitNextScene;

        public void Load(string scene, Action callback = null)
        {
            if(SceneManager.GetActiveScene().name == scene)
            {
                callback?.Invoke();

                return;
            }

            _waitNextScene = SceneManager.LoadSceneAsync(scene);

            _waitNextScene.completed += _ => callback?.Invoke();
        }
    }
}