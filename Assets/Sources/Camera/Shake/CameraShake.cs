using Cinemachine;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;

    private static CinemachineBasicMultiChannelPerlin _virtualCameraPerlin;
    private Coroutine _shaker;

    private void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _virtualCameraPerlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        SetShake(0, 0);
    }

    public static void SetShake(float amplitudeGain, float frequencyGain)
    {
        _virtualCameraPerlin.m_AmplitudeGain = amplitudeGain;
        _virtualCameraPerlin.m_FrequencyGain = frequencyGain;
    }

    private static IEnumerator Shaker(float amplitudeGain, float frequencyGain, float delay)
    {
        SetShake(amplitudeGain, frequencyGain);

        yield return new WaitForSeconds(delay);

        SetShake(0, 0);
    }
}
