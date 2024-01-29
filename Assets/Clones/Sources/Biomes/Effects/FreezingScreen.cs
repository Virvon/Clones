using Clones.GameLogic;
using System;
using UnityEngine;

public class FreezingScreen : MonoBehaviour
{
    [SerializeField] private float _minSize;
    [SerializeField] private float _maxSize;
        
    private CameraShader _cameraShader;

    public void Init(CameraShader cameraShader)
    {
        _cameraShader = cameraShader;
        _cameraShader.enabled = false;
    }

    public void SetFreezPercent(float percent)
    {
        if (percent == 0)
        {
            _cameraShader.enabled = false;

            return;
        }
        else if (_cameraShader.enabled == false)
        {
            _cameraShader.enabled = true;
        }

        float size = Mathf.Lerp(_minSize, _maxSize, percent / 100f);

        _cameraShader.ShaderTexture.SetFloat("_Size", size);
    }
}