using Clones.Services;
using System;
using System.Collections;
using UnityEngine;

namespace Clones.EducationLogic
{
    public class Waiter
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public Waiter(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Wait(float secconds, Action callback) => 
            _coroutineRunner.StartCoroutine(Timer(secconds, callback));

        private IEnumerator Timer(float seconds, Action callback)
        {
            yield return new WaitForSeconds(seconds);
            callback?.Invoke();
        }
    }
}
