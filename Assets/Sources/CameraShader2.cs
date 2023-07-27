using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShader2 : MonoBehaviour
{
    [SerializeField] private Shader _shader;

    private void Start()
    {
        Camera.main.SetReplacementShader(_shader, "shader");
    }
}
