using Cinemachine;
using System.Collections;
using UnityEngine;

namespace Clones.CameraShake
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraShake : MonoBehaviour
    {
        private CinemachineVirtualCamera _virtualCamera;

        private CinemachineBasicMultiChannelPerlin _virtualCameraPerlin;
        private Coroutine _shaker;

        private void Start()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _virtualCameraPerlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            SetShake(0, 0);
        }

        public void Shake(float amplitudeGain, float frequencyGain)
        {
            if (_shaker != null)
                StopCoroutine(_shaker);

            SetShake(amplitudeGain, frequencyGain);
        }

        public void Shake(float amplitudeGain, float frequencyGain, float delay)
        {
            if (_shaker != null)
                StopCoroutine(_shaker);

            _shaker = StartCoroutine(Shaker(amplitudeGain, frequencyGain, delay));
        }

        public void StopShake()
        {
            if (_shaker != null)
                StopCoroutine(_shaker);

            SetShake(0, 0);
        }

        private void SetShake(float amplitudeGain, float frequencyGain)
        {
            _virtualCameraPerlin.m_AmplitudeGain = amplitudeGain;
            _virtualCameraPerlin.m_FrequencyGain = frequencyGain;
        }

        private IEnumerator Shaker(float amplitudeGain, float frequencyGain, float delay)
        {
            SetShake(amplitudeGain, frequencyGain);

            yield return new WaitForSeconds(delay);

            SetShake(0, 0);
        }
    }

}