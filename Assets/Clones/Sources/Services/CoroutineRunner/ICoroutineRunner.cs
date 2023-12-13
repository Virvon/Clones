using System.Collections;
using UnityEngine;

namespace Clones.Services
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}
