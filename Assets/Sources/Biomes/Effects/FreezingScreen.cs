﻿using System;
using UnityEngine;

public class FreezingScreen : MonoBehaviour
{
    [SerializeField] private CameraShader _cameraShader;
    [SerializeField] private Freezing _freezing;

    [SerializeField] private float _minSize;
    [SerializeField] private float _maxSize;

    private void OnEnable()
    {
        _cameraShader.enabled = false;

        _freezing.FreezingPrecentChanged += OnFrezeengPrecentChanged;
    }

    private void OnDisable() => _freezing.FreezingPrecentChanged -= OnFrezeengPrecentChanged;

    private void OnFrezeengPrecentChanged()
    {
        if (_freezing.FreezPrecent == 0)
        {
            _cameraShader.enabled = false;

            return;
        }    
        else if(_cameraShader.enabled == false)
        {
            _cameraShader.enabled = true;
        }

        float size = Mathf.Lerp(_minSize, _maxSize, _freezing.FreezPrecent);

        size = (float)Math.Round(size, 2);

        _cameraShader.ShaderTexture.SetFloat("_Size", size);
    }
}
