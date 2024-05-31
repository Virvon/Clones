using Clones.Animation;
using UnityEngine;

namespace Clones.Environment
{
    [RequireComponent(typeof(Animator))]
    public class SlimeAnimation : MonoBehaviour
    {
        private const float MaxSpeed = 1.6f;
        private const float MinSpeed = 0.6f;

        private void Start() =>
            GetComponent<Animator>().SetFloat(AnimationPath.Environment.Bool.IdleSpeed, Random.Range(MinSpeed, MaxSpeed));
    }
}