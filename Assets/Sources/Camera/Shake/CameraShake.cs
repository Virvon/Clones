using Cinemachine;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private static CinemachineBasicMultiChannelPerlin _virtualCameraPerlin;


    private void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _virtualCameraPerlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        SetVirtualCameraPerlin(0, 0);
    }

    public static void Shake(float amplitudeGain, float frequencyGain) => SetVirtualCameraPerlin(amplitudeGain, frequencyGain);

    public IEnumerator Shake(float amplitudeGain, float frequencyGain, float delay = float.MaxValue)
    {
        SetVirtualCameraPerlin(amplitudeGain, frequencyGain);

        yield return new WaitForSeconds(delay);

        SetVirtualCameraPerlin(0, 0);
    }

    public static void Stop() => SetVirtualCameraPerlin(0, 0);

    private static void SetVirtualCameraPerlin(float amplitudeGain, float frequencyGain)
    {
        _virtualCameraPerlin.m_AmplitudeGain = amplitudeGain;
        _virtualCameraPerlin.m_FrequencyGain = frequencyGain;
    }
}
